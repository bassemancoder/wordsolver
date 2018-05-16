using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kifasoft.DctionarySolver
{
  public class Case
  {
    static public char AndChar { get { return '*'; } }
    static public char OrChar { get { return '|'; } }
    public enum CaseType
    {
      OneLetter,
      TwoLetters,
      TwoOptionalLetters
    }
    public CaseType Type { get; set; }
    public string Value { get; set; }
    public Case[] ConnectedCases { get; set; }
    public int Position { get; set; }

    public bool BeingVisited { get; set; }

    public int X
    {
      get
      {
        int x = Position - Y * 4;
        return x;
      }
    }

    public int Y
    {
      get
      {
        int y = Position / 4;
        return y;
      }
    }

    public string ValueToDisplay
    {
      get
      {
        if (Type == CaseType.TwoOptionalLetters)
          return Value[0].ToString() + "/" + Value[1].ToString();
        return Value;
      }
    }

    public string ValueToParse
    {
      get
      {
        if (Type == CaseType.TwoOptionalLetters)
          return OrChar.ToString() + Value;
        else if (Type == CaseType.TwoLetters)
          return AndChar.ToString() + Value;
        return Value;
      }
    }

    public override string ToString()
    {
      if (Type == CaseType.TwoLetters)
        return Position.ToString() + " - " + Value;
      else if (Type == CaseType.TwoOptionalLetters)
        return Position.ToString() + " - " + Value[0] + "/" + Value[1];
      return Position.ToString() + " - " + Value;
    }
  }

  public class Grid
  {
    public Case[] Cases { get; set; }

    public static Case[] parseInput(string tableInput)
    {
      Case[] wRetValue = new Case[16];
      int position = 0;
      for (int i = 0; i < tableInput.Length; i++)
      {
        wRetValue[position] = new Case();
        wRetValue[position].Position = position;
        wRetValue[position].Value = tableInput[i].ToString();

        if (tableInput[i] == Case.AndChar)
        {
          wRetValue[position].Type = Case.CaseType.TwoLetters;
        }
        else if (tableInput[i] == Case.OrChar)
        {
          wRetValue[position].Type = Case.CaseType.TwoOptionalLetters;
        }
        if (wRetValue[position].Type != Case.CaseType.OneLetter)
        {
          wRetValue[position].Value = tableInput[i + 1].ToString() + tableInput[i + 2].ToString();
          i += 2;
        }
        position++;
      }

      return wRetValue;
    }
    public string Content { get; set; }
    public Grid(string content)
    {
      if (content.Length < 16)
        throw new Exception("Content not the proper size, need to be 16");
      Content = content.ToLower();
      Cases = parseInput(content);
      for (int i = 0; i < Cases.Length; i++)
      {
        int y = i / 4;
        int x = i - y * 4;

        List<Case> neighbors = new List<Case>();

        for (int lookxd = -1; lookxd < 2; lookxd++)
        {
          int lookx = x + lookxd;
          if (lookx >= 0 && lookx < 4)
          {
            for (int lookyd = -1; lookyd < 2; lookyd++)
            {
              int looky = y + lookyd;
              if (looky >= 0 && looky < 4)
              {
                if (looky != y || lookx != x)
                {
                  neighbors.Add(get(lookx, looky));
                }
              }
            }
          }
        }

        Cases[i].ConnectedCases = neighbors.ToArray();
      }
    }

    public Case get(int x, int y)
    {
      int index = y * 4 + x;
      return Cases[index];
    }
  }
}
