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
                            @":::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                                ( •_•)                          (•_• )
                                  (ง)ง                            ୧( ୧ )
                               /︶\                             /︶\
                            :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                            :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::";

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

            Console.ReadLine();
        }
    }
}
