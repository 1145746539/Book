using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;

//Modbus通讯工具  
namespace Book.Util
{
    /// <summary>
    /// Modbus通讯工具类
    /// </summary>
    class ModBusTools
    {
        string Slave = "FF";
        
        /// <summary>
        /// 用于保存参数名字，用于读取机台参数 保存在本地INI中 爆破OEE定制
        /// </summary>
        public static string[] value;

        
        /// <summary>
        ///  读取参数的进度，用于读取机台参数 保存在本地INI中的过程进度条的显示 爆破OEE定制
        /// </summary>
        int ReadProcessMax;
        /// <summary>
        /// 输入数据，返回当前数据的CRC码
        /// </summary>
        /// <param name="CommandCode">输入的编码(Word)</param>
        /// <returns></returns>
        public string CRC(string CommandCode)
        {
            string command = CommandResort(CommandCode);
            if (command == "ERROR")
            {
                return "ERROR";
            }
            string[] datas = command.Split(' ');
            List<byte> bytedata = new List<byte>();

            foreach (string str in datas)//将
            {
                bytedata.Add(byte.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            //以空格分隔每个byte，从读取的字符串中转化成数据处理的byte

            byte[] crcbuffer = bytedata.ToArray();



            int CRCregister = 0xffff;//默认的内置寄存器
            int Length = crcbuffer.Length;


            for (int i = 0; i < Length; i++)
            {
                CRCregister = CRCregister ^ crcbuffer[i];//异或
                for (int j = 0; j < 8; j++)
                {
                    int Temp = CRCregister & 1;
                    CRCregister = CRCregister >> 1;
                    CRCregister = CRCregister & 0x7fff;
                    if (Temp == 1)
                    {
                        CRCregister = CRCregister ^ 0xa001;
                    }
                    CRCregister = CRCregister & 0xffff;
                }
            }

            string[] output = new string[2];
            output[1] = Convert.ToString((byte)((CRCregister >> 8) & 0xff), 16).PadLeft(2,'0');
            output[0] = Convert.ToString((byte)(CRCregister & 0xff), 16).PadLeft(2,'0');
            return output[0] + output[1];
        }
        /// <summary>
        /// 在编码中，byte与byte之间会以空格隔开.
        /// </summary>
        /// <param name="command">输入的编码(Words)</param>
        /// <returns></returns>
        private string CommandResort(string command)
        {
            string data = command.Replace(" ", "");
            if (data.Length % 2 != 0)//最小单位为一个byte（两个字符），若不能满足此条件则输入错误
            {
                Console.WriteLine("CRC Resort Bytes Error And Input:" + command);
                Console.WriteLine("Length of Input :" + data.Length);
                return "ERROR!";
            }
            char[] temp = data.ToCharArray();
            string output = "";
            for (int i = 0; i < temp.Length; i++)
            {
                if (i % 2 == 1) output = output + temp[i].ToString() + " ";
                else output = output + temp[i].ToString();
            }
            return output.Trim();//末尾会带有一个空格
        }

        /// <summary>
        /// 将一个word的每个byte倒序输出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string WordSort(string input)
        {
            input = input.PadLeft(8, '0');
            return input.Substring(6, 2) + input.Substring(4, 2) + input.Substring(2, 2) + input.Substring(0, 2);
        }

        /// <summary>
        /// 将多个word以(AB CD->CD AB)转换
        /// </summary>
        /// <param name="input">输入的数据</param>
        /// <returns></returns>
        private string ManyWordSort(string input)
        {
            try
            {
                Convert.ToInt32(input, 16);//尝试对输入进行转换，报错则不输出
            }
            catch
            {
                return "input error!";
            }
            char[] code = input.ToCharArray();
            Array.Reverse(code);

            for (int i = 0; i < code.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    char temp = code[i];
                    code[i] = code[i + 1];
                    code[i + 1] = temp;
                }
            }

            string output = "";
            foreach (char item in code)
            {
                output = output + item;
            }
            return output;
        }


        /// <summary>
        /// 根据多个参数设置ATQ系统
        /// </summary>
        /// <param name="RorW">读true 或写false</param>
        /// <param name="word">读0001 或写000204</param>
        /// <param name="address">写入地址</param>
        /// <param name="info">写入信息 ， 读 null</param>
        public string GetATEQString(bool RorW, string word, string address, double info)
        {
            try//检查 softwareid address 输入是否有问题
            {
                Convert.ToInt32(address, 16);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Set ATQ input Error!");
                return "";
            }

            StringBuilder stringOut = new StringBuilder();
            string value;
            if (RorW)//Read
            {
                value = stringOut.Append(Slave + "03" + address + word).ToString();
            }
            else//Write
            {
                string Numinfo = NumbertoHexString(info);
                value = stringOut.Append(Slave + "10" + address + word + Numinfo).ToString();
            }
            string crcString = CRC(value);
            if (crcString == "ERROR")
            {
                return "";
            }
            stringOut.Append(crcString);
            Console.WriteLine("Modbus Code:" + stringOut.ToString());
            //串口连接好后 写入ATQ

            return stringOut.ToString();
        }
        /// <summary>
        /// 根据程序号输出设定程序号的编码。只用于设置参数的选择程序号
        /// </summary>
        /// <param name="ID">程序号</param>
        /// <returns></returns>
        public string GetSoftwareID(string ID)
        {
            int n;
            if (!int.TryParse(ID, out n))
            {
                return "";

            }

            n = n - 1; //下标
            ID = n.ToString().PadLeft(2, '0');
            ID = ID.PadRight(4, '0');
            string output = Slave + "10" + "6000" + "000102" + ID;
            output = output + CRC(output);
            return output;
        }
        /// <summary>
        /// 选择程序号  用于测试启动
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetSoftwareIDForStart(string ID)
        {
            int n;
            if (!int.TryParse(ID, out n))
            {
                return "";

            }
            //to do  没有正确转为16进制
            n = n - 1; //下标
            ID = n.ToString().PadLeft(2, '0');
            ID = ID.PadRight(4, '0');
            string output = Slave + "10" + "0200" + "000102" + ID;
            output = output + CRC(output);
            return output;
        }


        /// <summary>
        /// 将输入的十位数信息 转换为写入的modbus码
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string NumbertoHexString(double num)
        {
            Int32 output = (Int32)(num * 1000); //放大1000倍
            string str = Convert.ToString(output,16);//符号位的处理
            str = WordSort(str); //高低位反转
            return str;//转换成十六进制字符串输出
        }

        /// <summary>
        /// 读取数据的指令。
        /// </summary>
        /// <param name="word">字数</param>
        /// <param name="address">地址</param>
        /// <returns></returns>
        public string ReadString(string word, string address)
        {
            StringBuilder WriteCommand = new StringBuilder();
            WriteCommand.Append(Slave + "03" + address + word);
            string Response = "";//收到的数据
            return Response;
        }

        /// <summary>
        /// 解析读到的数据 返回一个数值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double Analysis(string input)
        {
            string data = input.Replace(" ", "");//去除空格
            if (input.Length >= 10 && CRC_Check(data))
            {
                string Temp = data.Remove(0, 6);
                Temp = Temp.Substring(0, Temp.Length - 4);
                Temp = ManyWordSort(Temp);
                double output = ((double)Convert.ToInt32(Temp, 16)) / 1000;
                return output;
            }
            else
            {
                Console.WriteLine("Analysis Error:" + input);
                return 0.0;
            }
        }

        /// <summary>
        /// 将带有CRC检验码的字符串进行CRC检验，判断生成的校验码是否等于字符串自带的验证码
        /// </summary>
        /// <param name="input">带有CRC检验码的字符串</param>
        /// <returns></returns>
        public bool CRC_Check(string input)
        {
            string data = input.Replace(" ", "");//去除空格
            string Temp_CRC = data.Substring(data.Length - 4, 4);//获取尾部的CRC编码
            string Temp_Info = data.Substring(0, data.Length - 4);
            string Check_Code = CRC(Temp_Info);
            if (Check_Code.ToUpper() == Temp_CRC.ToUpper()) return true;
            else return false;
        }

        /// <summary>
        /// 将输入的数据每个byte分割存储在数组中
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string[] DataDivision(string input)
        {
            string[] result;
            if (input.Length % 2 != 0)
            {
                result = new string[0] { };
                Console.WriteLine("Data Division Error.Code Position: ModBusTools-282");
                return result;
            }
            result = new string[input.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = input[2 * i].ToString() + input[2 * i + 1].ToString();
            }
            return result;
        }

        //FF 03 1A 00 00 02 00 01 00 30 00 FF FF 00 00 00 00 F8 2A 00 00 00 00 00 00 70 17 00 00 8C C8
        // 0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30
        //FF 03 1A 00 00 08 00 05 00 00 80 0A 00 87 08 00 00 F8 2A 00 00 00 00 00 00 70 17 00 00 82 3B


        //FF(从站地址) 03（读取）1A（字节数）00 00（程序号）02 00（寄存器中的结果数量） 
        //01 00（测试模式）30 00 （仪器测试状态）FF FF（测试阶段）00 00 00 00（测试压力） 
        //F8 2A 00 00（压力单位） 00 00 00 00（泄漏量结果） 70 17 00 00 （泄漏量单位） 8C C8（CRC）

        /// <summary>
        /// 将一个16位或32位的数据（未排序）转换为整形数据。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetNumber(string input)
        {

            if (input != null && input.Length % 2 == 0)
            {
                input = ManyWordSort(input);
                int RealNum = -9999;
                try
                {
                    if (input.Length == 4) RealNum = Convert.ToInt16(input, 16);
                    else if (input.Length == 8) RealNum = Convert.ToInt32(input, 16);
                }
                catch (Exception)
                {
                    Console.WriteLine("Get Number Convert Error");
                }
                return RealNum;
            }
            else
            {
                Console.WriteLine("Get Number Input Error");
                return -9999;
            }
        }


       

        /// <summary>
        /// 选择气泵,并将该指令设置到机台中  1为大量程（4000Bar）气泵  2为小量程（500Bar）气泵
        /// </summary>
        /// <param name="MaxPressuer">最大气压 （Bar）</param>
        public string ChooseBlower(int MaxPressuer)
        {
            //write address: 6066
            // reg2 : 0000
            // reg1 : 1000
            //FF 10 6000 000102 0000 8e32k
            string output = "FF106066000102";
            if (MaxPressuer > 500) output = output + "1000";
            else output = output + "0000";
            return output + CRC(output);
        }

        /// <summary>
        /// 获得重置FIFO的指令
        /// </summary>
        /// <returns></returns>
        public string ResetFifo()
        {
            string output = "FF050000FF00";
            return output + CRC(output);
        }
        /// <summary>
        /// 从设备中读取所有参数
        /// </summary>
        /// <param name="ID">设备显示的程序号</param>
        
    }


}
