using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) Add Post");
            Console.WriteLine(" 2) List Posts");
            Console.WriteLine(" 3) Remove Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Add();
                    return this;
               /* case "2":
                    Author author = Choose();
                    if (author == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new AuthorDetailManager(this, _connectionString, author.Id);
                    }
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;*/
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();


            Console.Write("Publication Date: ");
            post.PublishDateTime = DateTime.Now;
            Console.WriteLine(post.PublishDateTime);

//-------------------------------------------------------------------
           

            Console.Write("Choose an author: ");

            void ListAuthors()
            {
                List<Author> authors = _authorRepository.GetAll();
                foreach (Author author in authors)
                {
                    Console.WriteLine(author);
                }
            }

            Author Choose(string prompt = null)
            {
                if (prompt == null)
                {
                    prompt = "Please choose an Author:";
                }

                Console.WriteLine(prompt);

                List<Author> authors = _authorRepository.GetAll();

                for (int i = 0; i < authors.Count; i++)
                {
                    Author author = authors[i];
                    Console.WriteLine($" {i + 1}) {author.FullName}");
                }
                Console.Write("> ");

                string input = Console.ReadLine();
                try
                {
                    int choice = int.Parse(input);
                    return authors[choice - 1];
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Selection");
                    return null;
                }
            }

            ListAuthors();
            post.Author = Choose();
            //-------------------------------------------------------------------

            //Console.Write("Blog: ");
            // post.Blog = Console.ReadLine();

            _postRepository.Insert(post);
        }

    }
}