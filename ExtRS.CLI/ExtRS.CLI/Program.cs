//using ExtRS.CLI.Properties;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Sonrai.ExtRS;
//using Sonrai.ExtRS.Models;
//using System.Drawing;
//using System.Text;
//using static System.Net.Mime.MediaTypeNames;
//using Spectre;

//namespace ExtRS.CLI
//{   
//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            IConfiguration _configuration;
//            var builder = new ConfigurationBuilder()
//            .AddUserSecrets<Program>();
//            _configuration = builder.Build();

//            var config = new ConfigurationBuilder()
//            .AddUserSecrets<Program>()
//            .Build();

//            Console.WriteLine(config["MySecret"]);

//            SSRSService ssrs;
//            HttpClient httpClient = new HttpClient();
//            var program = new Program();

//            SSRSConnection connection = new SSRSConnection(Resources.ReportServerName, Resources.User, AuthenticationType.ExtRSAuth);
//            ssrs = new SSRSService(connection, _configuration, null!);
//            connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(httpClient, Resources.User, Resources.Passphrase, connection.ReportServerName).Result;

//            Console.WriteLine(":::::::ExtRS Command Line Interface:::::::");
//            Console.WriteLine();
//            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
//            string boxing =
//                            "\r\n" + "\r\n" + @"
//                                ( •_•)                          (•_• )
//                                  (ง)ง                            ୧( ୧ )
//                               /︶\                             /︶\
//                            :::::::::::::::::::::::::::::::::::::::::::::::::::::::
//                            :::::::::::::::::::::::::::::::::::::::::::::::::::::::" + "\r\n" + "\r\n" + "\r\n";

//            int now = DateTime.Now.Hour;
//            if (now > 5 && now < 12)
//            {
//                Console.WriteLine("Good morning, " + username);
//            }
//            else if (now > 12 && now < 7)
//            {
//                Console.WriteLine("Good afternoon, " + username);
//            }
//            else
//            {
//                Console.WriteLine("Good evening, " + username);
//                Console.Write(boxing);
//            }

//            Console.WriteLine("Proceed to extRS? (Y/N)");
//            var answer = Console.ReadLine();
//            if (answer?.ToUpper() == "Y")
//            {
//                Console.WriteLine(@"Welcome to extRS v1.0
//                                The following commands are available:

//                                l                           - lists all reports
//                                r -reportName               - displays configuration of specific report");
//            }

//            answer = Console.ReadLine();

//            switch (answer?.Split(" ")[0])
//            {
//                case "l":
//                    {
//                        SSRSService _ssrs = new SSRSService(new SSRSConnection("localhost", "extRSAuth", AuthenticationType.ExtRSAuth), _configuration, null!);
//                        var reports = _ssrs.GetReports().Result;
//                        foreach(var report in reports)
//                        {
//                            Console.WriteLine(report.Name);
//                        }
//                    }

//                    break;
//                case "r":
//                    {


//                    }
//                    //Console.WriteLine(answer?.Split(" ")[1]);
//                    break;
//            }

//            Console.WriteLine("Press any key to continue...");
//            Console.ReadLine();
//            Spectre.Console.AnsiConsole.MarkupLine("[lime]This is a gooooooood CLI library!![/]");
//            for (int i = 0; i < 10; i++)
//            {
//                Spectre.Console.AnsiConsole.MarkupLine($"[blue]Loading extRS... {i * 20}%[/]");
//                Thread.Sleep(500);
//            }

//            var progress = Spectre.Console.AnsiConsole.Progress();
//            progress.Start(ctx =>
//            {
//                var task1 = ctx.AddTask("[lime]Loading super tools!...[/]", autoStart: true);
//                while (!task1.IsFinished)
//                {
//                    task1.Increment(20);
//                    Thread.Sleep(500);
//                }
//            });

//            for (int i = 0; i < 10; i++)
//            {
//                Spectre.Console.AnsiConsole.MarkupLine($"[red]SECURITY BREACH!!![/]");
//                Thread.Sleep(500);
//            }

//            Spectre.Console.AnsiConsole.MarkupLine("[blue](just kidding)[/]");

//            Console.WriteLine("Press any key to continue...");
//            Console.ReadLine();
//            ShowAsciiArt();
//            Console.ReadLine();
//        }

//        public static void ShowAsciiArt()
//        {

//            Bitmap image = new Bitmap(@"..\..\..\my_friend_benn.jpg", true);
//            image = FormattingService.GetResizedImage(image, 175);
//            string content = FormattingService.ConvertToAscii(image);
//            Console.WriteLine(content);
//        }
//    }
//}


// Source - https://stackoverflow.com/a/60768075
// Posted by Hayden
// Retrieved 2026-04-26, License - CC BY-SA 4.0

using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        public static List<Option> options;
        static void Main(string[] args)
        {
            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option("Thing", () => WriteTemporaryMessage("Hi")),
                new Option("Another Thing", () =>  WriteTemporaryMessage("How Are You")),
                new Option("Yet Another Thing", () =>  WriteTemporaryMessage("Today")),
                new Option("Exit", () => Environment.Exit(0)),
            };

            // Set the default index of the selected item to be the first
            int index = 0;

            // Write the menu out
            WriteMenu(options, options[index]);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        WriteMenu(options, options[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(options, options[index]);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();

        }
        // Default action of all the options. You can create more methods
        static void WriteTemporaryMessage(string message)
        {
            Console.Write("HEYYYY!!!");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine(message);
            Thread.Sleep(3000);
            WriteMenu(options, options.First());
        }


        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
        }
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }

}
