using System;
using System.Collections.Generic;
using System.IO;

namespace Parser
{
    public class Storage
    {
        public Storage(string fileName)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }

        public string FileName { get; private set; }

        public void Save<T>(IEnumerable<T> source)
        {
            StreamWriter SW = new StreamWriter(new FileStream(FileName, FileMode.Create, FileAccess.Write));
            foreach (var s in source)
                SW.WriteLine(s.ToString());
            SW.Close();
        }

        public void SaveJson<T>(IEnumerable<T> source)
        {
            Save(source.ToJson());
        }

        public IEnumerable<string> Load()
        {
            return File.ReadLines(FileName);
        }

        public IEnumerable<T> LoadJson<T>()
        {
            return Load().FromJson<T>();
        }
    }
}
