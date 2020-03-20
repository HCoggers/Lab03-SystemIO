using System;
using Xunit;
using Lab03_SystemIO;
using System.IO;

namespace Lab03_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanCreateANewFile()
        {
            File.Delete("../../../hobbieList.txt");
            Program.CreateFileIfDoesntExist("../../../hobbieList.txt", "../../../errorLog.txt");
            Assert.True(File.Exists("../../../hobbieList.txt"));
        }

        [Fact]
        public void CanWriteHobbie()
        {
            File.WriteAllText("../../../hobbieList.txt", "not testString.\n");
            Program.WriteHobbie("testString");
            Assert.Equal("testString", File.ReadAllLines("../../../hobbieList.txt")[^1]);
        }

        [Fact]
        public void DoesntAddDuplicate()
        {
            File.WriteAllText("../../../hobbieList.txt", "testString\n");
            int length = File.ReadAllLines("../../../hobbieList.txt").Length;
            Program.AddItem("testString", "../../../hobbieList.txt", "../../../errorLog.txt");
            Assert.Equal(length, File.ReadAllLines("../../../hobbieList.txt").Length);
        }

        [Fact]
        public void CanDeleteHobbie()
        {
            Program.WriteHobbie("testString2");
            Program.RemoveItem("testString2", "../../../hobbieList.txt", "../../../errorLog.txt");
            Assert.NotEqual("testString2", File.ReadAllLines("../../../hobbieList.txt")[^1]);
        }

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
