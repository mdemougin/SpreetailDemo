using Microsoft.Extensions.DependencyInjection;
using SpreetailDemo.Implementations;
using SpreetailDemo.Services.Interfaces;

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpreetailDemo
{
    public class Program
    {
        /// <summary>
        /// Main program thread
        /// </summary>
        /// <param name="args">No arguments currently being read</param>
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDictionaryService, DictionaryService>()
                .BuildServiceProvider();

            var _dictionaryService = serviceProvider.GetService<IDictionaryService>();

            string command = string.Empty;
            Console.WriteLine(_dictionaryService.List());

            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                ParseCommand(command, _dictionaryService);
            } while (command != null && command.ToLower() != "quit" && command.ToLower() != "exit");
        }

        /// <summary>
        /// Parses text read from command line into individual commands and runs them
        /// </summary>
        /// <param name="commandLineText">Text of command entered by user</param>
        /// <param name="directoryService">Injected dictionary service</param>
        public static void ParseCommand(string commandLineText, IDictionaryService directoryService)
        {
            try
            {
                string[] commandParts = commandLineText.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToArray();

                switch (commandParts[0].ToLower())
                {
                    case "keys":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Keys());
                        }    

                        break;
                    case "members":
                        if (commandParts.Length != 2)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Members(commandParts[1]));
                        }

                        break;
                    case "add":
                        if (commandParts.Length != 3)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Add(commandParts[1], commandParts[2]));
                        }

                        break;
                    case "remove":
                        if (commandParts.Length != 3)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Remove(commandParts[1], commandParts[2]));
                        }
                        
                        break;
                    case "removeall":
                        if (commandParts.Length != 2)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.RemoveAll(commandParts[1]));
                        }

                        break;
                    case "clear":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Clear());
                        }

                        break;
                    case "keyexists":
                        if (commandParts.Length != 2)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.KeyExists(commandParts[1]));
                        }

                        break;
                    case "memberexists":
                        if (commandParts.Length != 3)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.MemberExists(commandParts[1], commandParts[2]));
                        }

                        break;
                    case "allmembers":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.AllMembers());
                        }

                        break;
                    case "items":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Items());
                        }

                        break;
                    case "list":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.List());
                        }

                        break;
                    case "help":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Help());
                        }

                        break;
                    case "quit":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Quit());
                        }

                        break;
                    case "exit":
                        if (commandParts.Length != 1)
                        {
                            Console.WriteLine(directoryService.Error());
                        }
                        else
                        {
                            Console.WriteLine(directoryService.Exit());
                        }

                        break;
                    default:
                        Console.WriteLine(directoryService.Error());
                        break;
                }
            } 
            catch (Exception)
            {
                Console.WriteLine(directoryService.Error());
            }
        }
    }
}
