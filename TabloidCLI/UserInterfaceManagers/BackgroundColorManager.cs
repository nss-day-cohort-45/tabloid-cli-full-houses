using System;


namespace TabloidCLI.UserInterfaceManagers
{
    public class BackgroundManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;

        public BackgroundManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute
        {
            get
            {

                Console.WriteLine(@"Select a Background Color
 1) Blue
 2) Green
 3) Red
 4) Goldenrod?
 5) Magenta
 6) Gray with Red
 7) DarkGreen with White
 8) Douso Mode (For the people)
 9) White with Black
 10) Enter the Matrix?
 11) Tatiane Mode
 12) Jordan Mode
 13) Joshua Mode
 14) Back to basics
 0) Go Back");
                Console.Write("Make a theme selection > ");
                string chosenColor = Console.ReadLine();

                switch (chosenColor)
                {
                    case "1":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return _parentUI;
                    case "2":
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return _parentUI;
                    case "3":
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return _parentUI;
                    case "4":
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return _parentUI; ;
                    case "5":
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return _parentUI;
                    case "6":
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Clear();
                        return _parentUI;
                    case "7":
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return _parentUI;
                    case "8":
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.DarkYellow ;
                        Console.Clear();
                        return _parentUI;
                    case "9":
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        return _parentUI;
                    case "10":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Clear();
                        return _parentUI;
                    case "11":
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Clear();
                        return _parentUI;
                    case "12":
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Clear();
                        return _parentUI;
                    case "13":
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Clear();
                        return _parentUI;
                    case "14":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return _parentUI;
                    case "0":
                        return _parentUI;
                    default:
                        Console.WriteLine("Invalid Selection");
                        return this;
                }
            }
        }
    }
}

