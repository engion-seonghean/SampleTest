using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FuncEvent
{
    


    public static class KUtil
    {

        public static T LoadXml<T>(string file)
        {
            using (var sr = new StreamReader(File.OpenRead(file)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(sr);
            }
        }

        public static T LoadXml<T>(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(sr);
            }
        }

        public static void SaveXml<T>(string file, T obj)
        {
            string dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            using (StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(sw, obj);
            }
        }

        public static void SaveXml<T>(Stream stream, T obj)
        {
            using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(sw, obj);
            }
        }

        public static T LoadJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
        }

        public static void SaveJson<T>(string file, T obj)
        {
            string dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            File.WriteAllText(file, JsonConvert.SerializeObject(obj, Formatting.Indented), Encoding.UTF8);
        }

        /// <summary>
        /// 내용이 없는 파일을 생성합니다. 해당 디렉토리가 없을 경우 생성합니다.
        /// </summary>
        public static void TouchFile(string file)
        {
            FileInfo fi = new FileInfo(file);
            DirectoryInfo d = fi.Directory;
            if (!d.Exists) d.Create();
            using (File.Create(fi.FullName)) { }
        }

        /// <summary>
        /// 디렉토리 or 파일이 없으면 생성하며, 기존 파일에 내용을 추가합니다.
        /// </summary>
        public static void AppendLine(string file, string value)
        {
            FileInfo fi = new FileInfo(file);
            DirectoryInfo d = fi.Directory;
            if (!d.Exists) d.Create();
            using (var writer = File.AppendText(file))
                writer.WriteLine(value);
        }

        /// <summary>
        /// 디렉토리가 없는 경우 생성합니다.
        /// </summary>
        public static void TouchDirectory(string dirName)
        {
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);
        }

        /// <summary>
        /// Directory를 복사합니다.
        /// ref : http://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp
        /// </summary>
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.EnumerateFiles())
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            //fi.CopyTo(Path.Combine(target.FullName, fi.Name));
            foreach (DirectoryInfo diSourceSubDir in source.EnumerateDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
