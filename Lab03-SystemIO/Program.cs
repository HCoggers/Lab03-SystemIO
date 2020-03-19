using System;
using System.IO;

namespace Lab03_SystemIO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME TO YOUR DOOM");
            //We will be creating a list of hobbies for logging. 
            //we will be able to add and delete the items.
            // We will be able to view the list.
            //Exit the program at the end. 
            //User interface will re-apear after each action.
            //Loging errors in a error log file. 
            //Writing individual list items to a seperate file.
            UserInterface();

        }

        static void UserInterface()
        {
            Console.WriteLine("List of hobbies");

            bool action = true;
            while (action)
            {
                // Check for existing list
                // string curFile = @"c:\temp\test.txt";
                //Console.WriteLine(File.Exists(curFile) ? "File exists." : "File does not exist.");
                if (!File.Exists("../../../hobbieList.txt")) {
                    Console.WriteLine("Let's create a new list. press any key.");
                    string ynInput = Console.ReadLine();
                    CreateList();
                }

                // Else
                Console.WriteLine("please select one of the following choices. 1. 2. 3. 4. ");
                Console.WriteLine("1. View list of hobbies");
                Console.WriteLine("2. Add hobbie");
                Console.WriteLine("3. Delete hobbie");
                Console.WriteLine("4. Exit hobbie list");

                string numInput = Console.ReadLine();

                Int32.TryParse(numInput, out int number);

                string hobbie;
                switch (number)
                {
                    case 1:
                        Console.WriteLine("Your list of hobbies are ...");
                        // Make conditional for if the list is empty
                        break;

                    case 2:
                        Console.WriteLine("You just added a hobbie");
                        break;

                    case 3:
                        Console.WriteLine("You deleted a hobbie");
                        break;

                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static void CreateList()
        {
            string path = "../../../hobbieList.txt";
            File.WriteAllText(path, "Hobbie Lit:\n");
        }
    }
}
