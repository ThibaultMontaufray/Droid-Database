using Droid_database;
using NUnit.Framework;
using System;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestUTRuns()
        {
            Assert.IsTrue(true);
        }
        [Test]
        public void Test_mysql_execute_query()
        {
            try
            {
                MySqlAdapter.ExecuteQuery("select * from information_schema.tables");
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_mysql_execute_reader()
        {
            try
            {
                var v = MySqlAdapter.ExecuteReader("select * from information_schema.tables");
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_mysql_execute_script()
        {
            try
            {
                MySqlAdapter.ExecuteScript("select * from information_schema.tables");
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_mysql_load_data()
        {
            try
            {
                MySqlAdapter.LoadData("dump_file.csv", "mysupertable");
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_mysql_show_table()
        {
            try
            {
                var v = MySqlAdapter.ShowTable();
                Assert.IsNotNull(v);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_mysql_test_connection()
        {
            try
            {
                var v = MySqlAdapter.IsConnectionPossible();
                Assert.IsNotNull(v);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_mysql_backup()
        {
            try
            {
                var v = MySqlAdapter.Backup("here.csv");
                Assert.IsNotNull(v);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_mysql_insert_on_duplicate_key()
        {
            try
            {
                var v = MySqlAdapter.InsertOnDuplicateKey(new string[0], new string[0], new string[0]);
                Assert.IsNotNull(v);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
    }
}
