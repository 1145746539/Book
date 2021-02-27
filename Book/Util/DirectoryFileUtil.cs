namespace Book.Util
{
    using System.Collections.Generic;
    using System.IO;

    public class DirectoryFileHelper
    {
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
                    CopyDirectory(info2.FullName, destFileName);
                }
            }
        }

        public static void CopyFile(string s_SourceFilePath, string s_DestFilePath, bool b_Overwrite = true)
        {
            File.Copy(s_SourceFilePath, s_DestFilePath, b_Overwrite);
        }

        public static void CreateDir(string s_DirPath)
        {
            if (!Directory.Exists(s_DirPath))
            {
                Directory.CreateDirectory(s_DirPath);
            }
        }

        public static void CreateFile(string s_FilePath)
        {
            File.Create(s_FilePath).Close();
        }

        public static void DeleteDir(string s_DirPath)
        {
            if (Directory.Exists(s_DirPath))
            {
                new DirectoryInfo(s_DirPath).Delete(true);
            }
        }

        public static void DeleteFile(string s_FilePath)
        {
            File.Delete(s_FilePath);
        }

        public static void MoveFile(string s_SourceFilePath, string s_DestFilePath)
        {
            File.Move(s_SourceFilePath, s_DestFilePath);
        }

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

        public static void WriteLineTxtAppand(string s_TxtPath, string s_Msg)
        {
            using (StreamWriter writer = new StreamWriter(s_TxtPath, true))
            {
                writer.WriteLine(s_Msg);
                writer.Close();
            }
        }

        public static void WriteLineTxtCover(string s_TxtPath, string s_Msg)
        {
            using (StreamWriter writer = new StreamWriter(s_TxtPath, false))
            {
                writer.WriteLine(s_Msg);
                writer.Close();
            }
        }

        public static void WriteTxtAppand(string s_TxtPath, string s_Msg)
        {
            using (StreamWriter writer = new StreamWriter(s_TxtPath, true))
            {
                writer.Write(s_Msg);
                writer.Close();
            }
        }

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

