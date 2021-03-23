﻿using System;
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
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;


        }

        public IUserInterfaceManager Execute
        {
            get

            {
                Console.WriteLine("Post Menu");
                Console.WriteLine(" 1) List Posts");
                Console.WriteLine(" 2) Add Post");
                Console.WriteLine(" 3) Edit Post");
                Console.WriteLine(" 4) Remove Post");
                Console.WriteLine(" 5) Note Management");
                Console.WriteLine(" 6) Post Details");
                Console.WriteLine(" 0) Go Back");

                Console.Write("> ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ListPosts();
                        return this;
                    case "2":
                        Add();
                        return this;
                    case "3":
                        Edit();
                        return this;
                    case "4":
                        Remove();
                        return this;
                    case "5":

                    case "6":
                        Post post = Choose();
                        if (post == null)
                        {
                            return this;
                        }
                        else
                        {
                            return new PostDetailManager(this, _connectionString, post.Id);
                        };
                    case "0":
                        return _parentUI;
                    default:
                        Console.WriteLine("Invalid Selection");
                        return this;
                }
            }
        }


        //-------------------------LIST------------------------------------
        void ListPosts()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post.Title);
                Console.WriteLine(post.Url);
            }
        }


        //--------------------ADD-------------------------

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


            Author ChooseAuthor(string prompt = null)
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
            post.Author = ChooseAuthor();


            Blog ChooseBlog(string prompt = null)
            {
                if (prompt == null)
                {
                    prompt = "Please choose a Blog:";
                }

                Console.WriteLine(prompt);

                List<Blog> blogs = _blogRepository.GetAll();

                for (int i = 0; i < blogs.Count; i++)
                {
                    Blog blog = blogs[i];
                    Console.WriteLine($" {i + 1}) {blog.Title}");
                }
                Console.Write("> ");

                string input = Console.ReadLine();
                try
                {
                    int choice = int.Parse(input);
                    return blogs[choice - 1];
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Selection");
                    return null;
                }
            }
            post.Blog = ChooseBlog();

            _postRepository.Insert(post);
        }


        //-------------------------CHOOSE POST--------------------------------

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }



        //-------------------------EDIT POST----------------------------------

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title: ");
            string Title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Title))
            {
                postToEdit.Title = Title;
            }
            Console.Write("New URL (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }

            Author ChooseAuthor(string prompt = null)
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
            postToEdit.Author = ChooseAuthor();

            Blog ChooseBlog(string prompt = null)
            {
                if (prompt == null)
                {
                    prompt = "Please choose a Blog:";
                }

                Console.WriteLine(prompt);

                List<Blog> blogs = _blogRepository.GetAll();

                for (int i = 0; i < blogs.Count; i++)
                {
                    Blog blog = blogs[i];
                    Console.WriteLine($" {i + 1}) {blog.Title}");
                }
                Console.Write("> ");

                string input = Console.ReadLine();
                try
                {
                    int choice = int.Parse(input);
                    return blogs[choice - 1];
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Selection");
                    return null;
                }
            }
            postToEdit.Blog = ChooseBlog();







            _postRepository.Update(postToEdit);
        }


        //-------------------------REMOVE POST----------------------------------

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }








    }
}