﻿using System;
using System.Diagnostics;

/// <summary>
/// 日志类库
/// </summary>
namespace Book.Util
{

    public class TraceHelper
    {
        private static TraceHelper _traceHelper;

        private TraceHelper()
        {
        }

        public static TraceHelper GetInstance()
        {
            if (_traceHelper == null)
                _traceHelper = new TraceHelper();

            return _traceHelper;
        }

        public void Error(string message, string module)
        {
            Log(message, MessageType.Error, module);
        }

        public void Error(Exception ex, string module)
        {
            Log(ex.StackTrace, MessageType.Error, module);
        }

        public void Warning(string message, string module)
        {
            Log(message, MessageType.Warning, module);
        }

        public void Info(string message, string module)
        {
            Log(message, MessageType.Information, module);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="type">类型</param>
        /// <param name="module"></param>
        private void Log(string message, MessageType type, string module)
        {
            Trace.WriteLine(
                string.Format("{0},{1},{2},{3}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                type.ToString(),
                module,
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