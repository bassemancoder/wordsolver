using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kifasoft.DctionarySolver
{
  public class Solver
  {
    public class WordResult
    {
      public string Word { get; set; }
      public Case[] Path { get; set; }
      public int Score { get; set; }
      public override string ToString()
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Word);
        sb.AppendLine("------");
        for (int y = 0; y < 4; y++)
        {
          sb.Append("|");
          for (int x = 0; x < 4; x++)
          {
            string print = " ";
            foreach (var step in Path)
            {
              if (step.X == x && step.Y == y)
              {
                print = step.Value.ToString();
              }
            }
            sb.Append(print);
          }
          sb.Append("|");
          sb.AppendLine();
        }
        sb.AppendLine("------");

        return sb.ToString();
      }
    }
    public delegate void FoundWordEventHandler(object sender, WordResult e);
    public event FoundWordEventHandler FoundWordEvent;

    SortedSet<string> mDictionary;
    SortedList<string, WordResult> mGivenWords = new SortedList<string, WordResult>();
    SortedList<string, WordResult> mOngoingResults = new SortedList<string, WordResult>();
    TimeSpan mTimeSpentToResolve;
    bool mUseOptimisedDictionary = false;
    bool mUseMultithreading = false;
    bool mIgnoreEmptyDictionary = false;

    Grid mGrid;
    public Solver(SortedSet<string> dictionary, Grid grid, bool ignoreEmptyDictionary, bool useOptimisedDictionary, bool useMultithreading)
    {
      mIgnoreEmptyDictionary = ignoreEmptyDictionary;
      mUseMultithreading = useMultithreading;
      mUseOptimisedDictionary = useOptimisedDictionary;
      mDictionary = new SortedSet<string>(dictionary);
      mGrid = grid;
    }

    public TimeSpan TimeSpent { get { return mTimeSpentToResolve; } }

    long mCombinationsTried = 0;
    public long CombinationsTried
    {
      get { return mCombinationsTried; }
    }
    public int FoundCount
    {
      get
      {
        int wRetValue = 0;
        lock (mOngoingResults)
        {
          wRetValue = mOngoingResults.Count;
        }
        return wRetValue;
      }
    }

    public WordResult getNextBest()
    {
      return getNext(true);
    }

    public WordResult getNextBest(WordResult iReferenceWord)
    {
      return getNext(true, iReferenceWord);
    }

    public WordResult getNextWorst()
    {
      return getNext(false);
    }

    private WordResult getNext(bool best, WordResult iReferenceWord = null)
    {
      WordResult wRetValue = null;
      lock (mOngoingResults)
      {
        var t = mOngoingResults.Except(mGivenWords);
        if (best)
        {
          if (iReferenceWord != null)
          {
            //var res2 = t.OrderBy(x => LevenshteinDistance(iReferenceWord.Word, x.Value.Word)).FirstOrDefault();
            var res2 = t.OrderByDescending(x => Scoring(iReferenceWord.Word, x.Value.Word)).FirstOrDefault();
            if (res2.Value != null)
              wRetValue = res2.Value;
            var res = t.OrderByDescending(x => x.Value.Score).FirstOrDefault();
            if (res.Value != null && res.Value.Score > (wRetValue.Score + 3))
            {
              wRetValue = res.Value;
            }
          }
          else
          {
            var res = t.OrderByDescending(x => x.Value.Score).FirstOrDefault();
            if (res.Value != null)
            {
              //wRetValue.Score
              wRetValue = res.Value;
            }
          }
        }
        else
        {
          var res = t.OrderBy(x => x.Value.Score).FirstOrDefault();
          if (res.Value != null)
            wRetValue = res.Value;
        }
      }
      if (wRetValue != null)
      {
        mGivenWords.Add(wRetValue.Word, wRetValue);
      }

      return wRetValue;
    }

    /// <summary>
    /// Lower number mean more similar
    /// This algo do not take in considera the position difference
    /// so marche and tarche are very similar
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    private static int LevenshteinDistance(string s, string t)
    {
      int n = s.Length;
      int m = t.Length;
      int[,] d = new int[n + 1, m + 1];
      if (n == 0)
      {
        return m;
      }
      if (m == 0)
      {
        return n;
      }
      for (int i = 0; i <= n; d[i, 0] = i++)
        ;
      for (int j = 0; j <= m; d[0, j] = j++)
        ;
      for (int i = 1; i <= n; i++)
      {
        for (int j = 1; j <= m; j++)
        {
          int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
          d[i, j] = Math.Min(
              Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
              d[i - 1, j - 1] + cost);
        }
      }
      return d[n, m];
    }
    /// <summary>
    /// Return difference score
    /// Higher number means more similar
    /// </summary>
    /// <param name="iWord1"></param>
    /// <param name="iWord2"></param>
    /// <returns></returns>
    private static int Scoring(string iWord1, string iWord2)
    {
      int wScore = 0;
      //string shortWord, longWord;
      int maxLength = Math.Max(iWord1.Length, iWord2.Length);
      //Order(iWord1, iWord2, out shortWord, out longWord);
      for (int i = 0; i < iWord1.Length; i++)
      {
        // danse
        // dans
        // sanse
        if (i < iWord2.Length)
        {
          if (iWord1[i] == iWord2[i])
          {
            wScore += (maxLength - i) * 10;
          }
        }
      }
      return wScore;
    }

    private static void Order(string iWord1, string iWord2, out string oWordShort, out string oWordLong)
    {
      if (iWord1.Length > iWord2.Length)
      {
        oWordShort = iWord2;
        oWordLong = iWord1;
      }
      else
      {
        oWordShort = iWord1;
        oWordLong = iWord2;
      }
    }

    public void Solve()
    {
      DateTime start = DateTime.Now;
      mOngoingResults = new SortedList<string, WordResult>();

      if (mUseMultithreading)
      {
        SolveMultiThreaded();
      }
      else
      {
        foreach (var thecase in mGrid.Cases)
        {
          Stack<Case> casePath = new Stack<Case>();

          travel(thecase, casePath, "", mOngoingResults, mDictionary);
        }
      }
      DateTime end = DateTime.Now;
      mTimeSpentToResolve = end - start;
    }

    private void SolveMultiThreaded()
    {
      List<Task> wListTask = new List<Task>();

      for (int index = 0; index < 16; index++)
      {
        var grid = new Grid(mGrid.Content);
        Case thecase;
        Stack<Case> casePath = new Stack<Case>();
        thecase = grid.Cases[index];
        var task = Task.Run(() =>
        {
          travel(thecase, casePath, "", mOngoingResults, mDictionary);
        });
        wListTask.Add(task);
      }

      Task.WaitAll(wListTask.ToArray());
    }

    private void travel(Case theCase, Stack<Case> casePath, String thePath, SortedList<string, WordResult> results, SortedSet<string> dictionary, string caseLetter = null)
    {
      System.Threading.Interlocked.Increment(ref mCombinationsTried);
      if (mIgnoreEmptyDictionary && dictionary.Count == 0)
        return;
      if (thePath.Length > 16)
        return;
      if (theCase.Type == Case.CaseType.TwoOptionalLetters && caseLetter == null)
      {
        travel(theCase, casePath, thePath, results, dictionary, theCase.Value[0].ToString());
        travel(theCase, casePath, thePath, results, dictionary, theCase.Value[1].ToString());
        return;
      }

      string pathAdded;
      if (caseLetter == null)
        pathAdded = theCase.Value;
      else
        pathAdded = caseLetter;

      thePath += pathAdded;
      theCase.BeingVisited = true;
      casePath.Push(theCase);
      foreach (var theOtherCase in theCase.ConnectedCases)
      {
        if (!theOtherCase.BeingVisited)
        {
          string text = thePath.ToString().ToLower();
          if (text.Length >= 3)
          {
            if (!results.ContainsKey(text))
            {
              if (dictionary.Contains(text))
              {
                var solved = new WordResult() { Word = text, Path = casePath.Reverse().ToArray() };
                foreach (var pathElement in casePath)
                {
                  if (pathElement.Type != Case.CaseType.OneLetter)
                    solved.Score += 5;
                  else
                    solved.Score++;
                }

                lock (results)
                {
                  results.Add(text, solved);
                }
                if (FoundWordEvent != null)
                {
                  FoundWordEvent(this, solved);
                }
              }
            }
          }

          if (mUseOptimisedDictionary)
          {
            SortedSet<string> childDictionary = new SortedSet<string>(dictionary.Where(x => x.StartsWith(text)));
            travel(theOtherCase, casePath, thePath, results, childDictionary);
          }
          else
          {
            travel(theOtherCase, casePath, thePath, results, dictionary);
          }
        }
      }
      casePath.Pop();
      thePath.Remove(thePath.Length - pathAdded.Length, pathAdded.Length);
      theCase.BeingVisited = false;
    }
  }
}
