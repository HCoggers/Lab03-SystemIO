using System;
using Xunit;
using Lab03_SystemIO;
using System.IO;

namespace Lab03_Tests
{
    public class UnitTest1
    {
        // ensures the app creates a file if none exists
        [Fact]
        public void CanCreateANewFile()
        {
            File.Delete("../../../hobbieList.txt");
            Program.CreateFileIfDoesntExist("../../../hobbieList.txt", "../../../errorLog.txt");
            Assert.True(File.Exists("../../../hobbieList.txt"));
        }

        // ensures the app can write new hobbies to the list
        [Fact]
        public void CanWriteHobbie()
        {
            File.WriteAllText("../../../hobbieList.txt", "not testString.\n");
            Program.WriteHobbie("testString");
            Assert.Equal("testString", File.ReadAllLines("../../../hobbieList.txt")[^1]);
        }

        // ensures the app prevents the user from adding a duplicate hobby
        [Fact]
        public void DoesntAddDuplicate()
        {
            File.WriteAllText("../../../hobbieList.txt", "testString\n");
            int length = File.ReadAllLines("../../../hobbieList.txt").Length;
            Program.AddItem("testString", "../../../hobbieList.txt", "../../../errorLog.txt");
            Assert.Equal(length, File.ReadAllLines("../../../hobbieList.txt").Length);
        }

        // ensures the app can delete a hobby from the list
        [Fact]
        public void CanDeleteHobbie()
        {
            Program.WriteHobbie("testString2");
            Program.RemoveItem("testString2", "../../../hobbieList.txt", "../../../errorLog.txt");
            Assert.NotEqual("testString2", File.ReadAllLines("../../../hobbieList.txt")[^1]);
        }

        // ensures the app doesn't break if it tries to delete a hobby that doesn't exist
        [Fact]
        public void DoesntDeleteNonexistant()
        {
            File.WriteAllText("../../../hobbieList.txt", "not testString \ntestString3");
            int length = File.ReadAllLines("../../../hobbieList.txt").Length;
            Program.RemoveItem("testString2", "../../../hobbieList.txt", "../../../errorLog.txt");
            Assert.Equal(length, File.ReadAllLines("../../../hobbieList.txt").Length);
        }
    }
}
