using System;
using System.Collections.Generic;
using System.Text;

namespace Samola.Algorithms.Sequences.App
{
    class Menu
    {
        public List<IConsoleExcutable> Executables { get; set; }
        public int? ForceRunIndex { get; set; }

        public Menu()
        {
            this.Executables = new List<IConsoleExcutable>();
        }

        public void ReadAndExecute(bool clearScreen = true)
        {
            int index = -1;

            if (this.ForceRunIndex.HasValue)
            {
                index = this.ForceRunIndex.Value;
            }
            else
            {
                DrawMenu(clearScreen);
                index = ReadMenuSelection();
            }

            if (index > -1)
                RunExecutable(index);
        }

        public void DrawMenu(bool clearScreen)
        {
            if (clearScreen)
                Console.Clear();

            int count = this.Executables.Count;

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"[{i + 1}] {this.Executables[i].ExecutableName}");
            }

            Console.WriteLine($"[X] Exit");
        }

        public int ReadMenuSelection()
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (input.ToLower().Trim() == "x")
            {
                return 0;
            }
            else
            {
                return Int32.Parse(input) - 1;
            }
        }

        public void RunExecutable(int index)
        {
            // TODO: make async
            Console.Clear();
            var executable = this.Executables[index];

            Console.WriteLine(executable.ExecutableName);
            executable.Run();
        }
    }


    public interface IConsoleExcutable
    {
        void Run();
        string ExecutableName { get; }
    }
}
