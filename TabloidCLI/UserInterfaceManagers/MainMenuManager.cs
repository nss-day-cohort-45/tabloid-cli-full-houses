using System;

namespace TabloidCLI.UserInterfaceManagers
{
    public class MainMenuManager : IUserInterfaceManager
    {
        private const string CONNECTION_STRING = 
            @"Data Source=localhost\SQLEXPRESS;Database=TabloidCLI;Integrated Security=True";

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Bonjour! Ciao! Konnichiwa! Hello! Welcome to your own personal blog management system. We're so happy that you chose Tabloid!");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Main Menu");

            Console.WriteLine(" 1) My Journal Management");
            Console.WriteLine(" 2) Blog Management");
            Console.WriteLine(" 3) Author Management");
            Console.WriteLine(" 4) Post Management");
            Console.WriteLine(" 5) Tag Management");
            Console.WriteLine(" 6) Search by Tag");
            Console.WriteLine(" 7) Note Management");
            Console.WriteLine(" 8) Background Color Options");
            Console.WriteLine(" 0) Exit");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {

                case "1": return new JournalManager(this, CONNECTION_STRING);
                case "2": return new BlogManager(this, CONNECTION_STRING);
                case "3": return new AuthorManager(this, CONNECTION_STRING);
                case "4": return new PostManager(this, CONNECTION_STRING);
                case "5": return new TagManager(this, CONNECTION_STRING);
                case "6": return new SearchManager(this, CONNECTION_STRING);
                case "7": return new NoteManager(this, CONNECTION_STRING);
                case "0":
                    Console.WriteLine("Good bye");
                    return null;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }


            void BackgroundColor()
            {
                while (choice == "7")
                {

                    Console.WriteLine(@"Select a Background Color
                            1) blue
                            2) green
                            3) red");
                    int chosenColor = int.Parse(Console.ReadLine());
                    if (chosenColor == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Clear();

                    }
                    else if (chosenColor == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Clear();
                    }
                    else if (chosenColor == 3)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Clear();
                    }

                }
            }
        }
        }
    }

