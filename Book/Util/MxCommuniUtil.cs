using ActUtlTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.Util
{
    class MxCommuniUtil
    {
        private ActUtlType actUtlType = null;
        /// <summary>
        /// 是否打开连接
        /// </summary>
        private bool _isOpen = false;
        private int stationNumber = 0;  //默认0
        public string message = null;

        public static MxCommuniUtil Instance()
        {
            
            return new MxCommuniUtil();
        }

        /// <summary>
        /// 第一步 打开PLC连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            lock (this)
            {
                try
                {
                    if (_isOpen == true)
                    {
                        return true;
                    }
                    if (actUtlType == null)
                    {
                        actUtlType = new ActUtlType();
                        actUtlType.ActLogicalStationNumber = stationNumber;
                    }
                    int result = actUtlType.Open();
                    if (result != 0)
                    {
                        message = "PLC连接失败,错误代码：" + (uint)result;
                        return false;
                    }
                    else
                    {
                        _isOpen = true;
                        message = "PLC连接成功";
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    _isOpen = false;
                    message = "error:\n" + ex.Message;
                    return false;
                }
            }

        }

        /// <summary>
        /// 软元件的批量读取->连续 (4 字节数据 )
        /// </summary>
        public bool ReadDeviceBlock(string blockName, int size, out int[] lData)
        {
            lock (this)
            {
                lData = new int[size];
                try
                {
                    int result = actUtlType.ReadDeviceBlock(blockName, size, out lData[0]);
                    if (0 == result)
                    {
                        message = "从PLC地址" + blockName + "开始读取" + size + "位成功";
                        return true;
                    }
                    else
                    {
                        message = "从PLC地址" + blockName + "开始读取" + size + "失败，错误代码：\n" + (uint)result;
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    message = "error:\n" + ex.Message;
                    return false;
                }
                finally
                {
                    Console.WriteLine(message);
                }
            }

        }

        /// <summary>
        /// 随机读取
        /// </summary>
        /// <param name="szDevice"></param>
        /// <param name="size"></param>
        /// <param name="lData"></param>
        /// <returns></returns>
        public bool ReadDeviceRandom(string szDevice, int size, out int[] lData)
        {

            lData = new int[size];
            try
            {
                int result = actUtlType.ReadDeviceRandom(szDevice, size, out lData[0]);
                if (0 == result)
                {
                    message = "随即读取PLC地址" + szDevice + "\n" + size + "位成功";
                    return true;
                }
                else
                {
                    message = "随即读取PLC地址" + szDevice + "\n" + size + "失败，错误代码：\n" + (uint)result;
                    return false;
                }

            }
            catch (Exception ex)
            {
                message = "error:\n" + ex.Message;
                return false;
            }
            finally
            {
                Console.WriteLine(message);
            }

        }


        /// <summary>
        /// 软元件的单个读取 (4 字节数据 )
        /// </summary>
        /// <param name="device"></param>
        /// <param name="lData"></param>
        /// <returns></returns>
        public bool GetDevice(string device, out int lData)
        {

            try
            {
                int result = actUtlType.GetDevice(device, out lData);
                if (0 == result)
                {
                    message = "PLC地址" + device + "读取成功,结果：" + lData;
                    return true;
                }
                else
                {
                    message = "PLC地址" + device + "读取失败，错误代码：" + (uint)result;
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = "error:\n" + ex.Message;
                lData = -1;
                return false;
            }
            finally
            {
                Console.WriteLine(message);
            }

        }

        /// <summary>
        /// 双字读取
        /// </summary>
        /// <param name="device"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int GetDoubleDevice(string device)
        {
            int[] data = new int[2];
            int Yint = -100000;
            try
            {
                actUtlType.ReadDeviceBlock("D118", 2, out data[0]);
                string value1 = Convert.ToString(data[0], 2);
                string value2 = Convert.ToString(data[1], 2);
                string bitString = value2 + value1;
                Yint = Convert.ToInt32(bitString, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Yint;
        }

        /// <summary>
        /// 双字写入
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetDoubleDevice(string address, int value)
        {
            Int32 n = -2;
            string nt = Convert.ToString(n, 2);
           // Int32 n1 = Convert.ToInt32(nt, 2);
            UInt32 n2 = Convert.ToUInt32(nt, 2);

            int[] dataTemp = new int[2];
            long a = int.MaxValue;
            long b = int.MinValue;
            long c = a - b;
            long d = 0;
            if (value > 0)
            {

                dataTemp[0] = (int)(value & 0x0000ffff);
                dataTemp[1] = (int)(value >> 16);
            }
            else
            {
                //  c + value( 异或)  反码 => 补码
                d = c + value + 1;
                
                dataTemp[0] = (int)(d & 0x0000ffff); //后16位 数据
                dataTemp[1] = (int)(d >> 16); //前16位 符号
            }
            int result = actUtlType.WriteDeviceBlock(address, 2, ref dataTemp[0]);
            if (0 == result)
                return true;
            else
                return false;
            
        }

        public bool ClosePLC()
        {
            lock (this)
            {
                try
                {
                    if (_isOpen == false)
                    {
                        return true;
                    }
                    int result = actUtlType.Close();
                    if (result != 0)
                    {
                        message = "PLC关闭失败,错误代码：" + (uint)result;
                        return false;
                    }
                    else
                    {
                        _isOpen = false;
                        message = "PLC关闭成功";
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    _isOpen = true;
                    message = "error:\n" + ex.Message;
                    return false;
                }
            }


        }
    }

}
