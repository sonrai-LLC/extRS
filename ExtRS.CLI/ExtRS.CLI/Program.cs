using Sonrai.ExtRS;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ExtRS.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            if(now > 5 && now < 12)
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


            Test();

            Console.ReadLine();
        }

        public static void Test()
        {
            //btnConvertToAscii.Enabled = false;
            //Load the Image from the specified path
            Bitmap image = new Bitmap(@"..\..\..\my_friend_benn.jpg", true);
            //Resize the image...
            //I've used a trackBar to emulate Zoom In / Zoom Out feature
            //This value sets the WIDTH, number of characters, of the text image
            image = FormattingService.GetResizedImage(image, 200);
            //Convert the resized image into ASCII
            string content = FormattingService.ConvertToAscii(image);
            //Enclose the final string between <pre> tags to preserve its formatting
            //and load it in the browser control
            Console.WriteLine(content);
            Console.ReadLine();
        }
    }
}
