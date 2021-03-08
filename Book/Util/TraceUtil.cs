/*
 * 1.右键项目添加应用程序配置文件
 * 2.添加节点 system.diagnostics
 * */

/// <summary>
/// 日志类库
/// </summary>
namespace Book.Util
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Text;

    public class TraceUtil
    {
        private static TraceUtil _traceHelper;

        private TraceUtil()
        {

        }

        public static TraceUtil GetInstance()
        {
            if (_traceHelper == null)
                _traceHelper = new TraceUtil();

            return _traceHelper;
        }

        public void Error(string message)
        {
            Log(message, MessageType.Error);
        }

        public void Error(Exception ex)
        {
            Log(ex.StackTrace, MessageType.Error);
        }

        public void Warning(string message)
        {
            Log(message, MessageType.Warning);
        }

        public void Info(string message)
        {
            Log(message, MessageType.Information);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="type">类型</param>
        private void Log(string message, MessageType type)
        {
            //获取使用者方法名与类名
            //MethodBase method = new StackFrame(true).GetMethod();
            //string name = this.GetType().Name;

            ////获取调用者方法名与类名
            //MethodBase methodBase = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            //string className = methodBase.ReflectedType.FullName;

            StackTrace stack = new StackTrace();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < stack.FrameCount; i++)
            {
                MethodBase meth = stack.GetFrame(i).GetMethod();
                sb.Append(meth.ReflectedType.FullName + "=" + meth.Name + "--");
            }


            Trace.WriteLine(string.Format("{0},{1},{2},{3}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                type.ToString(),
                sb.ToString(),
                message));
        }
    }

    public enum MessageType
    {
       Information = 0,
       Warning = 1,
       Error = 2
    }
}
