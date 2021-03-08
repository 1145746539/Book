namespace Book.Util
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class DirectoryFileHelper
    {
        /// <summary>
        /// 
        /// ����ļ��� û���򴴽�
        /// </summary>
        /// <param name="s_DirPath"></param>
        public static void ClearDir(string s_DirPath)
        {
            if (Directory.Exists(s_DirPath))
            {
                FileSystemInfo[] fileSystemInfos = new DirectoryInfo(s_DirPath).GetFileSystemInfos();
                foreach (FileSystemInfo info2 in fileSystemInfos)
                {
                    if (info2 is DirectoryInfo)
                    {
                        new DirectoryInfo(info2.FullName).Delete(true);
                    }
                    else
                    {
                        File.Delete(info2.FullName);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(s_DirPath);
            }
        }

        /// <summary>
        /// �����ļ���
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public static void CopyDirectory(string sourcePath, string destinationPath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);
            foreach (FileSystemInfo info2 in info.GetFileSystemInfos())
            {
                string destFileName = Path.Combine(destinationPath, info2.Name);
                if (info2 is FileInfo)
                {
                    File.Copy(info2.FullName, destFileName);
                }
                else
                {
                    Directory.CreateDirectory(destFileName);
                    CopyDirectory(info2.FullName, destFileName); //�ص�
                }
            }
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="s_SourceFilePath"></param>
        /// <param name="s_DestFilePath"></param>
        /// <param name="b_Overwrite"></param>
        public static void CopyFile(string s_SourceFilePath, string s_DestFilePath, bool b_Overwrite = true)
        {
            File.Copy(s_SourceFilePath, s_DestFilePath, b_Overwrite);
        }

        /// <summary>
        /// �����ļ���
        /// </summary>
        /// <param name="s_DirPath"></param>
        public static void CreateDir(string s_DirPath)
        {
            if (!Directory.Exists(s_DirPath))
            {
                Directory.CreateDirectory(s_DirPath);
            }
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="s_FilePath"></param>
        public static void CreateFile(string s_FilePath)
        {
            File.Create(s_FilePath).Close();
        }

        /// <summary>
        /// ɾ���ļ���
        /// </summary>
        /// <param name="s_DirPath"></param>
        public static void DeleteDir(string s_DirPath)
        {
            if (Directory.Exists(s_DirPath))
            {
                new DirectoryInfo(s_DirPath).Delete(true);
            }
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="s_FilePath"></param>
        public static void DeleteFile(string s_FilePath)
        {
            File.Delete(s_FilePath);
        }


        /// <summary>
        ///ɾ������һ��ʱ�䣨�£��ļ��A�е����ļ��A
        /// </summary>
        /// <param name="parentDirPath">���ļ��A</param>
        /// <param name="monthLimit">�·�����</param>
        /// <returns>true:�ɹ�</returns>
        public static void DeleteDirLimiting(string parentDirPath, int monthLimit)
        {
            if (Directory.Exists(parentDirPath))
            {

                DirectoryInfo parentDir = new DirectoryInfo(parentDirPath);
                DirectoryInfo[] childDirArray = parentDir.GetDirectories();
                foreach (DirectoryInfo dir in childDirArray)
                {
                    if (DateTime.Now.Month - dir.CreationTime.Month > monthLimit)
                    {
                        Directory.Delete(dir.FullName, true);
                    }
                }

            }
        }


        /// <summary>
        /// ɾ������һ�������ļ��A�е��ļ�����ɾ�ļ���)
        /// </summary>
        /// <param name="parentDirPath">���ļ��A</param>
        /// <param name="dayLimit">��������</param>
        /// <returns>true:�ɹ�</returns>
        public static void DeleteFileLimiting(string parentDirPath, int dayLimit)
        {
            if (Directory.Exists(parentDirPath))
            {

                DirectoryInfo parentDir = new DirectoryInfo(parentDirPath);
                FileInfo[] childFileArray = parentDir.GetFiles();
                TimeSpan ts = new TimeSpan();
                foreach (FileInfo file in childFileArray)
                {
                    ts = DateTime.Now - file.CreationTime;
                    if (ts.Days > dayLimit)
                    {
                        File.Delete(file.FullName);

                    }
                }


            }

        }


        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="s_SourceFilePath"></param>
        /// <param name="s_DestFilePath"></param>
        public static void MoveFile(string s_SourceFilePath, string s_DestFilePath)
        {
            File.Move(s_SourceFilePath, s_DestFilePath);
        }

        /// <summary>
        /// ��ȡTxt�ĵ�
        /// </summary>
        /// <param name="s_TxtPath"></param>
        /// <returns></returns>
        public static List<string> ReadTxt(string s_TxtPath)
        {
            List<string> list = new List<string>();
            if (File.Exists(s_TxtPath))
            {
                using (StreamReader reader = new StreamReader(s_TxtPath))
                {
                    string str = reader.ReadLine();
                    while (str != null)
                    {
                        list.Add(str);
                    }
                    reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// �滻�ļ���
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public static void ReplaceDirectory(string sourcePath, string destinationPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (Directory.Exists(destinationPath))
                {
                    Directory.Delete(destinationPath, true);
                }
                CopyDirectory(sourcePath, destinationPath);
            }
        }

        /// <summary>
        /// ���ı��ļ���׷���ı������У�������
        /// </summary>
        /// <param name="s_TxtPath"></param>
        /// <param name="s_Msg"></param>
        public static void WriteLineTxtAppand(string s_TxtPath, string s_Msg)
        {
            using (StreamWriter writer = new StreamWriter(s_TxtPath, true))
            {
                writer.WriteLine(s_Msg);
                writer.Close();
            }
        }

        /// <summary>
        /// �����ļ�׷���ı������У�
        /// </summary>
        /// <param name="s_TxtPath"></param>
        /// <param name="s_Msg"></param>
        public static void WriteLineTxtCover(string s_TxtPath, string s_Msg)
        {
            using (StreamWriter writer = new StreamWriter(s_TxtPath, false))
            {
                writer.WriteLine(s_Msg);
                writer.Close();
            }
        }

        /// <summary>
        /// ���ı��ļ���׷���ı��������У� ������
        /// </summary>
        /// <param name="s_TxtPath"></param>
        /// <param name="s_Msg"></param>
        public static void WriteTxtAppand(string s_TxtPath, string s_Msg)
        {
            using (StreamWriter writer = new StreamWriter(s_TxtPath, true))
            {
                writer.Write(s_Msg);
                writer.Close();
            }
        }

        /// <summary>
        /// �����ļ�׷���ı��������У�
        /// </summary>
        /// <param name="s_TxtPath"></param>
        /// <param name="s_Msg"></param>
        public static void WriteTxtCover(string s_TxtPath, string s_Msg)
        {
            using (StreamWriter writer = new StreamWriter(s_TxtPath, false))
            {
                writer.Write(s_Msg);
                writer.Close();
            }
        }
    }
}

