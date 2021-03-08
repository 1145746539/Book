/*
 * 1.下载并安装System.Data.SQLite  （ http://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki）
 * 2.添加System.Data.SQLite引用 C:\Program Files\System.Data.SQLite ---
 * 3. using System.Data.SQLite;
 * */

namespace Book.Util
{
    using System;
    using System.Data.SQLite;
    using System.Diagnostics;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// sqlite工具类
    /// </summary>
    public class SqliteUtil
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private SQLiteConnection dbConnection = null;

        /// <summary>
        /// 创建数据库文件 同名会覆盖
        /// </summary>
        /// <returns></returns>
        public bool CreateDB(string path)
        {
            //var fileName = @"D:/testDB.db";
            
            
            
            try
            {
                var fileName = path;
                SQLiteConnection.CreateFile(fileName);
            }
            catch (Exception e)
            {
             
                Log(e.Message);
                return false;
            }
            return true;
        }

        private void Log(string message)
        {
          
            StackTrace stack =  new StackTrace();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < stack.FrameCount; i++)
            {
                MethodBase meth =stack.GetFrame(i).GetMethod();
                sb.Append(meth.ReflectedType.FullName + "=" + meth.Name + "--");
            }


           Trace.WriteLine(string.Format("{0},{1},{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),sb.ToString(), message));

        }

        /// <summary>
        /// 创建带密码的数据库文件 同名会覆盖
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CreateDB(string path,string passWord)
        {
            //var fileName = @"D:/testDB.db";

            try
            {

                var fileName = path;
                SQLiteConnection.CreateFile(fileName);
                string connectionString = "data source = " + fileName;
                SQLiteConnection dbConnection = new SQLiteConnection(connectionString);
                dbConnection.Open();
                dbConnection.ChangePassword(passWord);
                dbConnection.Close();

            }
            catch (Exception e)
            {
                MethodBase method = new StackFrame(true).GetMethod();
                string name = this.GetType().Name;

                MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
                string className = methodBase.ReflectedType.FullName;

                Trace.WriteLine(string.Format("{0},{1},{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), className + "." + methodBase.Name + ">>" + name + "." + method.Name, e.Message));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 链接数据库
        /// </summary>
        /// <param name="path"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool ConnectDB(string path,string passWord)
        {
            dbConnection = new SQLiteConnection();

            SQLiteConnectionStringBuilder dbConnectionstr = new SQLiteConnectionStringBuilder();
            dbConnectionstr.DataSource = path;
            if (passWord != null)
                dbConnectionstr.Password = passWord;
            
            dbConnection.ConnectionString = dbConnectionstr.ToString();
            dbConnection.Open();

            CreateTable();
            return true;
        }

        /// <summary>
        /// 创建默认的一张表
        /// </summary>
        /// <returns></returns>
        public bool CreateTable()
        {

            string[] colNames = new string[] { "ID", "Name", "Age", "Email" };
            string[] colTypes = new string[] { "INTEGER", "TEXT", "INTEGER", "TEXT" };

            string tableName = "table1";

            string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];

            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            queryString += "  ) ";
            //CREATE TABLE IF NOT EXISTS table1( ID INTEGER, Name TEXT, Age INTEGER, Email TEXT  ) 
            ExecuteQuery(queryString);
            return true;
        }

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteQuery(queryString);
        }


       

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        public SQLiteDataReader ExecuteQuery(string queryString)
        {
            SQLiteDataReader dataReader = null;
            try
            {
                SQLiteCommand  dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                dataReader = dbCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }

            return dataReader;
        }

        public void CloseDB()
        {
            if (dbConnection != null)
                dbConnection.Close();
          
        }
    }
}
