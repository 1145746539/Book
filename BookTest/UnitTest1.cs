using System;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fileName = @"D:/testDB.db";

            SQLiteConnection.CreateFile(fileName);
        }
    }
}
