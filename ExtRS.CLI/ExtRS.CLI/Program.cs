using ExtRS.CLI.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ExtRS.CLI
{   
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration _configuration;
            var builder = new ConfigurationBuilder()
            .AddUserSecrets<Program>();
            _configuration = builder.Build();

            SSRSService ssrs;
            HttpClient httpClient = new HttpClient();
            var program = new Program();

            SSRSConnection connection = new SSRSConnection(Resources.ReportServerName, Resources.User, AuthenticationType.ExtRSAuth);
            ssrs = new SSRSService(connection, _configuration, null);
            connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(httpClient, Resources.User, Resources.Passphrase, connection.ReportServerName).Result;

            Console.WriteLine(":::::::ExtRS Command Line Interface:::::::");
            Console.WriteLine();
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string boxing =
                            "\r\n" + "\r\n" + @"
                                ( •_•)                          (•_• )
                                  (ง)ง                            ୧( ୧ )
                               /︶\                             /︶\
                            :::::::::::::::::::::::::::::::::::::::::::::::::::::::
                            :::::::::::::::::::::::::::::::::::::::::::::::::::::::" + "\r\n" + "\r\n" + "\r\n";

            int now = DateTime.Now.Hour;
            if (now > 5 && now < 12)
            {
                Console.WriteLine("Good morning, " + username);
            }
            else if (now > 12 && now < 7)
            {
                Console.WriteLine("Good afternoon, " + username);
            }
            else
            {
                Console.WriteLine("Good evening, " + username);
                Console.Write(boxing);
            }

            Console.WriteLine("Proceed to extRS? (Y/N)");
            var answer = Console.ReadLine();
            if (answer?.ToUpper() == "Y")
            {
                Console.WriteLine(@"Welcome to extRS v1.0
                                The following commands are available:

                                l                           - lists all reports
                                r -reportName               - displays configuration of specific report");
            }

            answer = Console.ReadLine();

            switch (answer?.Split(" ")[0])
            {
                case "l":
                    {
                        SSRSService _ssrs = new SSRSService(new SSRSConnection("localhost", "extRSAuth", AuthenticationType.ExtRSAuth), _configuration, null);
                        var reports = _ssrs.GetReports().Result;
                        foreach(var report in reports)
                        {
                            Console.WriteLine(report.Name);
                        }
                    }
                   
                    break;
                case "r":
                    {


                    }
                    //Console.WriteLine(answer?.Split(" ")[1]);
                    break;
            }
        }

        public static void ShowAsciiArt()
        {

            Bitmap image = new Bitmap(@"..\..\..\my_friend_benn.jpg", true);
            image = FormattingService.GetResizedImage(image, 175);
            string content = FormattingService.ConvertToAscii(image);
            Console.WriteLine(content);
        }
    }
}
