using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kifasoft.DctionarySolver;
using System.Runtime.InteropServices;

namespace Kifasoft.WordamentSolver
{
  public partial class Form1 : Form
  {
    DateTime mStartBackDriveTime;
    bool mStartBackdrive = false;
    int mBackDriveStep = 0;
    Point mTopLeft = new Point(495, 257);
    Point mBottomRight = new Point(737, 499);
    Button[] mButtons = new Button[16];
    SortedSet<string> mDictionary;
    Solver mSolver;
    Solver.WordResult mResultToDisplay;
    Task mSolvingTask;
    int mAnimationPosition = 0;

    GlobalKeyboardHook mGlobalKeyboardHook;


    private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
    {
      if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkEscape)
        return;

      if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
      {
        mBackDriveStep = 0;
        mStartBackdrive = false;

        e.Handled = true;
      }
    }

    public Form1()
    {
      InitializeComponent();

      mGlobalKeyboardHook = new GlobalKeyboardHook();
      mGlobalKeyboardHook.KeyboardPressed += OnKeyPressed;

      buttonNextBest.Enabled = false;
      buttonNextShortest.Enabled = false;
      listBoxFoundWord.DisplayMember = "Word";
      listBoxProposed.DisplayMember = "Word";

      timerShowAnimation.Enabled = true;
      int index = 0;
      string startMessage = "ALEOREACMEAMBLEL";
      var inputs = Grid.parseInput(startMessage);
      for (int y = 0; y < 4; y++)
      {
        for (int x = 0; x < 4; x++)
        {
          mButtons[index] = new Button();
          mButtons[index].Text = inputs[index].ValueToParse;
          mButtons[index].Dock = DockStyle.Fill;
          mButtons[index].KeyPress += Button_KeyPress;
          this.tableLayoutPanel1.Controls.Add(mButtons[index], x, y);
          index++;
        }
      }
      var files = Directory.GetFiles("./Data/", "*.txt");
      foreach (var file in files)
      {
        listBoxLanguage.Items.Add(file);
      }
      listBoxLanguage.SelectedIndex = 0;
    }

    private void LoadDictionary()
    {
      if (mDictionary == null && listBoxLanguage.SelectedItem != null)
      {
        var content = File.ReadAllText(listBoxLanguage.SelectedItem.ToString());
        content = content.ToLower();
        string asciiStr = content.RemoveDiacritics();
        mDictionary = new SortedSet<string>(asciiStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
      }
    }
    private void Button_KeyPress(object sender, KeyPressEventArgs e)
    {
      bool wGoNext = true;
      var button = sender as Button;
      if (button != null)
      {
        if (e.KeyChar == '\b')
        {
          button.Text = "";
          return;
        }
        if (button.Text.Length > 0 && (button.Text[0] == Case.AndChar || button.Text[0] == Case.OrChar))
        {
          wGoNext = false;
          button.Text += e.KeyChar.ToString().ToUpper();
          if (e.KeyChar != Case.AndChar && e.KeyChar != Case.OrChar && button.Text.Length == 3)
            wGoNext = true;
        }
        else
          button.Text = e.KeyChar.ToString().ToUpper();

        if (e.KeyChar == Case.OrChar || e.KeyChar == Case.AndChar)
          wGoNext = false;

        if (wGoNext)
        {
          for (int i = 0; i < mButtons.Length; i++)
          {
            if (button == mButtons[i])
            {
              if ((i + 1) < mButtons.Length)
              {
                mButtons[i + 1].Focus();
              }
            }
          }
        }
      }
      e.Handled = true;
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }


    private void Solve_FoundWordEvent(object sender, Solver.WordResult e)
    {
      listBoxFoundWord.Invoke((MethodInvoker)delegate
      {
        // Running on the UI thread
        listBoxFoundWord.Items.Add(e);
      });
    }

    private void buttonSolver_Click(object sender, EventArgs e)
    {
      LoadDictionary();
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < mButtons.Length; i++)
      {
        sb.Append(mButtons[i].Text);
      }
      this.Text = "Solving " + sb.ToString();
      Grid grid = new Grid(sb.ToString());
      listBoxFoundWord.Items.Clear();
      listBoxProposed.Items.Clear();
      mSolver = new Solver(mDictionary, grid, true, true, true);
      mSolver.FoundWordEvent += Solve_FoundWordEvent;
      mSolvingTask = Task.Run(() =>
      {
        mSolver.Solve();
      });
      buttonNextShortest.Enabled = true;
      buttonNextBest.Enabled = true;
      buttonSolver.Enabled = false;
      labelResultFound.Text = "Result found : ";
    }

    private void buttonNextBest_Click(object sender, EventArgs e)
    {
      mResultToDisplay = mSolver.getNextBest();
      highlightResult();
    }

    private void buttonNextShortest_Click(object sender, EventArgs e)
    {
      mResultToDisplay = mSolver.getNextWorst();
      highlightResult();
    }

    private void highlightResult()
    {
      if (mResultToDisplay != null)
      {
        listBoxProposed.Items.Add(mResultToDisplay);
        for (int i = 0; i < mButtons.Length; i++)
        {
          mButtons[i].ForeColor = buttonNextBest.ForeColor;
          mButtons[i].BackColor = buttonNextBest.BackColor;
        }
        labelCurrentWord.Text = mResultToDisplay.Word.ToUpper();
        mAnimationPosition = 0;

        foreach (var step in mResultToDisplay.Path)
        {
          mButtons[step.Position].BackColor = Color.Green;
        }
        timerShowAnimation.Interval = (int)(1000.0f / 12.0f * (float)mResultToDisplay.Word.Length);
      }
    }

    private void timerShowAnimation_Tick(object sender, EventArgs e)
    {
      if (mSolvingTask != null && mSolver != null)
      {
        labelResultFound.Text = "Dictionary: " + mDictionary.Count() + " - Result found : " + mSolver.FoundCount + " - Combination Tried : " + mSolver.CombinationsTried;
        if (mSolvingTask.IsCompleted)
        {
          buttonSolver.Enabled = true;
          labelResultFound.Text += " - Completed - " + mSolver.TimeSpent.TotalSeconds + " sec.";
        }
      }

      if (mResultToDisplay != null)
      {
        if (mAnimationPosition < mResultToDisplay.Path.Length)
        {
          mButtons[mResultToDisplay.Path[mAnimationPosition].Position].Text = mResultToDisplay.Path[mAnimationPosition].ValueToDisplay.ToString().ToUpper();
          mButtons[mResultToDisplay.Path[mAnimationPosition].Position].ForeColor = Color.LightYellow;
          mAnimationPosition++;
        }
        else
        {
          for (int i = 0; i < mButtons.Length; i++)
          {
            mButtons[i].ForeColor = buttonNextBest.ForeColor;
          }
          mAnimationPosition = 0;
        }

        if (mStartBackdrive)
        {
          if (mBackDriveStep < mResultToDisplay.Path.Length)
          {
            var step = mResultToDisplay.Path[mBackDriveStep];
            int width = mBottomRight.X - mTopLeft.X;
            int height = mBottomRight.Y - mTopLeft.Y;
            int xpos = mTopLeft.X + width / 4 * step.X + (width / 8);
            int ypos = mTopLeft.Y + height / 4 * step.Y + (height / 8);

            Win32Interrop.POINT win32currentPos;
            Win32Interrop.GetCursorPos(out win32currentPos);
            Point currentPos = new Point(win32currentPos.X, win32currentPos.Y);

            if (mBackDriveStep == 0)
            {
              currentPos.X = xpos;
              currentPos.Y = ypos;
              Win32Interrop.SetCursorPos(xpos, ypos);
              System.Threading.Thread.Sleep(10);
              Win32Interrop.mouse_event(Win32Interrop.MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
              System.Threading.Thread.Sleep(10);
            }

            var points = getPoints(new Point(currentPos.X, currentPos.Y), new Point(xpos, ypos), 20);
            foreach (var point in points)
            {
              Win32Interrop.mouse_event(Win32Interrop.MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);
              Win32Interrop.SetCursorPos(point.X, point.Y);
              System.Threading.Thread.Sleep(1);
            }
            if (mBackDriveStep == mResultToDisplay.Path.Length - 1)
            {
              Win32Interrop.mouse_event(Win32Interrop.MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
              Win32Interrop.SetCursorPos(xpos, ypos);
              mStartBackdrive = false;
              System.Threading.Thread.Sleep(5);
              xpos += 10;
              ypos += 10;
              Win32Interrop.mouse_event(Win32Interrop.MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
              Win32Interrop.SetCursorPos(xpos, ypos);
              System.Threading.Thread.Sleep(50);
              startBackDrive();
            }
            else
            {
              mBackDriveStep++;
            }
          }
        }
      }
    }

    public Point[] getPoints(Point p1, Point p2, int quantity)
    {
      var points = new Point[quantity];
      int ydiff = p2.Y - p1.Y, xdiff = p2.X - p1.X;
      if (p2.X == p1.X)
        p2.X = p2.X - 1;
      double slope = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
      double x, y;

      --quantity;

      for (double i = 0; i < quantity; i++)
      {
        y = slope == 0 ? 0 : ydiff * (i / quantity);
        x = slope == 0 ? xdiff * (i / quantity) : y / slope;
        points[(int)i] = new Point((int)Math.Round(x) + p1.X, (int)Math.Round(y) + p1.Y);
      }

      points[quantity] = p2;
      return points;
    }

    private void buttonStopGeneration_Click(object sender, EventArgs e)
    {
      mResultToDisplay = null;
      buttonNextShortest.Enabled = false;
      buttonNextBest.Enabled = false;
      buttonSolver.Enabled = true;
      mSolver = null;

      for (int i = 0; i < mButtons.Length; i++)
      {
        mButtons[i].Text = "";
        mButtons[i].ForeColor = buttonNextBest.ForeColor;
        mButtons[i].BackColor = buttonNextBest.BackColor;
      }
      mButtons[0].Focus();
    }

    private void listBoxFoundWord_SelectedIndexChanged(object sender, EventArgs e)
    {
      var item = listBoxFoundWord.SelectedItem as Solver.WordResult;
      mResultToDisplay = item;
      highlightResult();
    }

    /// <summary>
    /// (0,0) -----
    /// -----------
    /// -----------
    /// -------(10, 10)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonRecordTopLeft_Click(object sender, EventArgs e)
    {
      Win32Interrop.POINT point;
      if (Win32Interrop.GetCursorPos(out point))
      {
        mTopLeft.X = point.X;
        mTopLeft.Y = point.Y;
        buttonRecordBR.Focus();
      }
    }

    private void buttonRecordBR_Click(object sender, EventArgs e)
    {
      Win32Interrop.POINT point;
      if (Win32Interrop.GetCursorPos(out point))
      {
        mBottomRight.X = point.X;
        mBottomRight.Y = point.Y;
        buttonStartBackDrive.Focus();
      }
    }

    private void startBackDrive()
    {
      if ((DateTime.Now - mStartBackDriveTime).TotalSeconds < 180)
      {
        buttonNextBest_Click(this, new EventArgs());
        timerShowAnimation.Interval = (int)numericUpDownSpeed.Value;
        mBackDriveStep = 0;
        mStartBackdrive = true;
      }
      else
      {
        var screenpos = this.PointToScreen(buttonStartBackDrive.Location);
        Win32Interrop.SetCursorPos(screenpos.X + 50, screenpos.Y + 10);
        buttonStartBackDrive.BackColor = Color.Red;
        this.Focus();
        buttonStartBackDrive.Focus();
        buttonStartBackDrive.Focus();
      }
    }

    private void buttonStartBackDrive_Click(object sender, EventArgs e)
    {
      buttonStartBackDrive.BackColor = buttonNextShortest.BackColor;
      labelCoordinate.Text = String.Format("({0}, {1}) ({2}, {3})", mTopLeft.X, mTopLeft.Y, mBottomRight.X, mBottomRight.Y);
      mStartBackDriveTime = DateTime.Now;
      startBackDrive();
    }

    private void listBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
      mDictionary = null;
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      mGlobalKeyboardHook?.Dispose();
    }

    private void listBoxProposed_SelectedIndexChanged(object sender, EventArgs e)
    {
      var item = listBoxProposed.SelectedItem as Solver.WordResult;
      mResultToDisplay = item;
      highlightResult();
    }
  }
}
