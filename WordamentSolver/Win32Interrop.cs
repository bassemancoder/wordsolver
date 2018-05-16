using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Kifasoft.WordamentSolver
{
  public class Win32Interrop
  {
    //This is a replacement for Cursor.Position in WinForms
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
      public int X;
      public int Y;

      public static implicit operator Point(POINT point)
      {
        return new Point(point.X, point.Y);
      }
    }

    /// <summary>
    /// Retrieves the cursor's position, in screen coordinates.
    /// </summary>
    /// <see>See MSDN documentation for further information.</see>
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    public static Point GetCursorPosition()
    {
      POINT lpPoint;
      GetCursorPos(out lpPoint);
      //bool success = User32.GetCursorPos(out lpPoint);
      // if (!success)

      return lpPoint;
    }
    public const int MOUSEEVENTF_LEFTDOWN = 0x02;
    public const int MOUSEEVENTF_LEFTUP = 0x04;
  }
}
