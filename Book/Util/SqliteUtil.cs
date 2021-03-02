/*
 * 1.下载并安装System.Data.SQLite  （ http://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki）
 * 2.添加System.Data.SQLite引用 C:\Program Files\System.Data.SQLite ---
 * 3. using System.Data.SQLite;
 * */

namespace Book.Util
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// sqlite工具类
    /// </summary>
    public class SqliteUtil
    {

        /// <summary>
        /// 创建数据库文件
        /// </summary>
        /// <returns></returns>
        public bool CreateDB(string path)
        {
            var fileName = "D:/testDB.db";

            SQLiteConnection.CreateFile(fileName);
            return true;
        }
        
    }
}
