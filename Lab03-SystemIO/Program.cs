using System;
using System.IO;

namespace Lab03_SystemIO
{
    public class Program
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

        /// <summary>
        /// Writes main menu to console and execs methods based on user input
        /// </summary>
        static void UserInterface()
        {
            Console.WriteLine("List of hobbies");

            // checker for menu rewriting loop
            bool action = true;
            string hobbieListPath = "../../../hobbieList.txt";
            string errorLogPath = "../../../errorLog.txt";

            // writes main menu until program is ended by user
            while (action)
            {
                // Check for existing list
                CreateFileIfDoesntExist(hobbieListPath, errorLogPath);

                // Else
                Console.WriteLine("please select one of the following choices. 1. 2. 3. 4. ");
                Console.WriteLine("1. View list of hobbies");
                Console.WriteLine("2. Add hobbie");
                Console.WriteLine("3. Delete hobbie");
                Console.WriteLine("4. Delete hobbie list");
                Console.WriteLine("5. Exit");

                string numInput = Console.ReadLine();

                Int32.TryParse(numInput, out int number);

                string hobbie;
                
                // executing method based on user input
                switch (number)
                {
                    case 1:
                        Console.WriteLine("Your list of hobbies are ...");
                        string myList = File.ReadAllText(hobbieListPath);
                        Console.WriteLine(myList);
                        break;

                    case 2:
                        Console.WriteLine("What hobbie would you like to add?");
                        hobbie = Console.ReadLine();
                        AddItem(hobbie, hobbieListPath, errorLogPath);
                        Console.WriteLine("You just added a hobbie");
                        break;

                    case 3:
                        Console.WriteLine("Which hobbie would you like to remove?");
                        hobbie = Console.ReadLine();
                        RemoveItem(hobbie, hobbieListPath, errorLogPath);
                        break;

                    case 4:
                        File.Delete(hobbieListPath);
                        Console.WriteLine("Your list was deleted");
                        break;

                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        /// <summary>
        /// Creates a new hobbies list if none exists
        /// </summary>
        /// <param name="listPath">Filepath of list to write to</param>
        /// <param name="errorPath">Filepath of error log</param>
        public static void CreateFileIfDoesntExist(string listPath, string errorPath)
        {
            if (!File.Exists("../../../hobbieList.txt"))
            {
                Console.WriteLine("Let's create a new list. press any key.");
                //Console.ReadLine();
                File.WriteAllText(listPath, "Hobbie List:\n");
                File.WriteAllText(errorPath, "Errors:\n");
            }
        }

        /// <summary>
        /// Adds a new item to an existing hobbie list
        /// </summary>
        /// <param name="hobbie">The string value of the hobbie to add</param>
        /// <param name="path">The filepath of the hobbie list</param>
        /// <param name="path2">The filepath of the error log</param>
        public static void AddItem(string hobbie, string path, string path2)
        {
            string[] list = File.ReadAllLines(path);

            // searches hobbie list for duplicates
            bool found = false;
            foreach (string item in list)
            {
                if (item == hobbie)
                {
                    Console.WriteLine("You've already added that hobbie.");
                    using (StreamWriter sw = File.AppendText(path2))
                    {
                        sw.WriteLine($"{hobbie} this was already in your file. ERROR DOES NOT EXIST.");
                    };
                    found = (true);
                    break;
                }
            }

            // adds hobbie if not a duplicate
            if (!found)
                WriteHobbie(hobbie);
        }

        /// <summary>
        /// Removes a hobbie from an existing list
        /// </summary>
        /// <param name="hobbieToRemove">String value of hobbie to be removed</param>
        /// <param name="path">Filepath of hobbie list</param>
        /// <param name="path2">Filepath of error log</param>
        public static void RemoveItem(string hobbieToRemove, string path, string path2)
        {
            int repeatInput = 0;
            string[] words = File.ReadAllLines(path);

            // creating new array to render non-deleted hobbies to
            string[] newWords = new string[words.Length - 1];
            bool found = false;

            // buids array from existing values that don't match value to be deleted
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != hobbieToRemove || repeatInput == 0)
                {
                    newWords[repeatInput] = words[i];
                    if (repeatInput != newWords.Length - 1)
                        repeatInput++;
                }
                else
                    found = true;
            }

            // write new array to list if value is deleted
            if (found)
            {
                File.WriteAllLines(path, newWords);
                Console.WriteLine("You deleted a hobbie");
            }
            else

            // write original array if no value is deleted
            {
                Console.WriteLine("That hobbie don't exist, yo.");
                using (StreamWriter sw = File.AppendText(path2))
                {
                    sw.WriteLine($"{hobbieToRemove} this was not in your file. ERROR DOES NOT EXIST.");
                };
            }
        }

        /// <summary>
        /// Writes a hobbie to the list
        /// </summary>
        /// <param name="hobbie">String value of hobbie to add</param>
        public static void WriteHobbie(string hobbie)
        {
            string path = "../../../hobbieList.txt";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(hobbie);
            };
        }
    }
}
