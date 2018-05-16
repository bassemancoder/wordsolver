using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Kifasoft.DctionarySolver;

namespace ImageOCR
{
  class Program
  {

    static void Main(string[] args)
    {
      var content = File.ReadAllText("./Data/francais.txt");
      string asciiStr = content.RemoveDiacritics();
      var lines = asciiStr.Split('\n');
      string characters = "esaeioeltsleeeri";
      Grid grid = new Grid(characters);
      Solver solve = new Solver(new SortedSet<string>(lines), grid, false, false, false);
      solve.FoundWordEvent += Solve_FoundWordEvent;

      Task task = Task.Run(() =>
      {
        solve.Solve();
      });

      ConsoleKeyInfo key = new ConsoleKeyInfo();
      while (key.KeyChar != 'q')
      {
        if (Console.KeyAvailable)
        {
          Solver.WordResult next;
          key = Console.ReadKey(true);
          if (key.KeyChar == 'p')
          {
            next = solve.getNextWorst();
          }
          else
          {
            next = solve.getNextBest();
          }

          if (next != null)
            Console.WriteLine(next);
        }
        System.Threading.Thread.Sleep(100);
      }
      Console.Write("Press any key to continue . . . ");
      Console.ReadKey(true);
    }

    private static void Solve_FoundWordEvent(object sender, Solver.WordResult e)
    {
      //Console.WriteLine(e);
    }
  }
}
