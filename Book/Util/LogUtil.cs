using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Book.Util
{
    /// <summary>
    /// 日志记录的工具类
    /// </summary>
    class LogUtil
    {
        /// <summary>
        /// 日志保存路径
        /// </summary>
        private readonly static string filePath = @"D:\equipment\log";
        /// <summary>
        /// 日志名
        /// </summary>
        private static string fileName;

        /// <summary>
        /// 写入日志：path可null
        /// </summary>
        /// <param name="path"> 路径为空时候，写入默认路径</param>
        /// <param name="logInfo"> 日志信息</param>
        /// <param name="doStr"> 执行动作名称</param>
        public static void WriteOrCreateLog(string path, string logInfo, string doStr)
        {
            try
            {
                DateTime dt = DateTime.Now;
                fileName = dt.ToString("yyyyMMdd") + ".txt";
                string filePathEnd = path ?? filePath;  //path为空时返回filePath , 不为空返回本身
                if (!Directory.Exists(filePathEnd))
                {
                    Directory.CreateDirectory(filePathEnd);
                }
                String log = filePathEnd + "\\" + fileName;
                StreamWriter textWriter = new StreamWriter(log, true);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendLine("************" + doStr + "************");
                sb.AppendLine(logInfo);
                sb.AppendLine("-----------------------------------------------");
                textWriter.WriteLine(sb.ToString());
                textWriter.Close();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }

        }

        /// <summary>
        /// 一行日志
        /// </summary>
        /// <param name="path"></param>
        /// <param name="logInfo"></param>
        public static void WriteOrCreateLog(string path, string logInfo)
        {
            try
            {
                //获取使用者方法名与类名
                MethodBase method = new StackFrame(true).GetMethod();
               // string name = this.GetType().Name;

                //获取调用者方法名与类名
                MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
                string className = methodBase.ReflectedType.FullName;



                DateTime dt = DateTime.Now;
                fileName = dt.ToString("yyyyMMdd") + ".txt";
                string filePathEnd = path ?? filePath;  //path为空时返回filePath , 不为空返回本身
                if (!Directory.Exists(filePathEnd))
                {
                    Directory.CreateDirectory(filePathEnd);
                }
                String log = filePathEnd + "\\" + fileName;
                StreamWriter textWriter = new StreamWriter(log, true);
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + logInfo);
                textWriter.WriteLine(sb.ToString());
                textWriter.Close();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }

        }

    }

}
