using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;


namespace Book.Util
{
    // 添加 System.Web.Extensions 引用

    /// <summary>
    /// 网络连接的工具类
    /// </summary>
    class HttpWebUtil
    {

       //

        private static JavaScriptSerializer JSS = new JavaScriptSerializer();
        /// <summary>
        /// 网络访问
        /// </summary>
        /// <param name="url">访问地址</param>
        /// <param name="sendWay">访问方式</param>
        /// <param name="contentType">访问资源类型</param>
        /// <param name="postDataStr">发送内容</param>
        /// <returns></returns>
        public static string HttpPostOrGet(string url, string sendWay, string contentType, string postDataStr)
        {
            string returnString = string.Empty;

            HttpWebRequest request = null;//定义一个请求
            HttpWebResponse response;//定义一个响应
            Byte[] byteData = postDataStr == null ? new byte[] { } : Encoding.UTF8.GetBytes(postDataStr); //如果发送内容不为空转换为UTF-8格式
            string method = sendWay.ToUpper();//访问方式转换为大写格式
            switch (method)//判断访问方式
            {
                case "POST"://加密提交
                    request = (HttpWebRequest)WebRequest.Create(url);//创建请求                    
                    request.Method = method;//传输方式
                    //"application/json" 资源类型：JSON字符串  "application/x-www-form-urlencoded" 表单数据(键值对)
                    request.ContentType = contentType;//资源类型
                    request.ContentLength = byteData.Length;
                    using (Stream requestStream = request.GetRequestStream())//获取请求流
                    {
                        requestStream.Write(byteData, 0, byteData.Length);//向服务器写字节流
                    }
                    break;
                case "GET"://明文提交
                    request = (HttpWebRequest)WebRequest.Create(url + (postDataStr == null ? "" : "?") + postDataStr);
                    request.Method = method;
                    break;
            }
            try
            {
                response = (HttpWebResponse)request.GetResponse();//获取服务器响应信息
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;//获取响应异常信息
            }
            HttpStatusCode code = response.StatusCode;  //ok
            string stute = code.ToString();
            int id = (int)code;


            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                returnString = reader.ReadToEnd();
            }


            return returnString;
        }


        /// <summary>
        /// value==null?实例化对象转json string：json string转实例化对象；
        /// （new对象,null） or (new对象，string)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transFormation">实例化对象</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ChangeType<T>(T transFormation, string value)
        {
            object result = null;
            try
            {
                if (value != null)
                {
                    result = JSS.Deserialize<T>(value);
                }
                else
                {
                    result = JSS.Serialize(transFormation);

                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                //WriteLogHelper.WriteOrCreateLog(null, ex.Message.ToString());
            }
            return result;
        }

        /// <summary>
        /// 将对象转JSON
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="transFormationString">实例化对象，包含所需的属性值</param>
        /// <returns></returns>
        public static string ChangeFormJson<T>(T transFormationString)
        {
            Console.WriteLine(transFormationString.GetType());

            string returnString = null;
            try
            {
                returnString = JSS.Serialize(transFormationString);
            }
            catch (Exception ex)
            {
                returnString = ex.Message;
                // WriteLogHelper.WriteOrCreateLog(null, ex.Message.ToString());
            }

            return returnString;
        }

        /// <summary>
        /// Json转对象
        /// </summary>
        /// <typeparam name="Books">对象类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns></returns>
        public static T ChangeFormObject<T>(string jsonString)
        {
            Console.WriteLine(jsonString.GetType().Name);
            T result = default(T); //引用类型呢默认null,值类型默认0
            try
            {
                result = JSS.Deserialize<T>(jsonString);
            }
            catch (Exception ex)
            {
                //WriteLogHelper.WriteOrCreateLog(null, ex.Message.ToString());
            }
            return result;

        }
    }
}
