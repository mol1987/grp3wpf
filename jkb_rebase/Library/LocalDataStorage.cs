using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;

namespace Library
{
    public static class LocalDataStorage
    {
        public static string WorkingPath { get; set; } = Directory.GetParent(System.Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string DataFolder { get; set; } = "/Assets/Data";
        private static List<CSVFile> CSVFiles = new List<CSVFile>();
        private class CSVFile
        {
            public string Name { get; set; }
            public string FileName { get; set; }
            public ExpandoObject Data { get; set; }
        };

        static LocalDataStorage()
        {
            
        }
       
        private static void FindFolders()
        {
            string fullPath = Path.Join(WorkingPath.AsSpan(), DataFolder.AsSpan());
            string[] files = Directory.GetFiles(fullPath, "*csv");
            foreach(string file in files)
            {
                System.IO.FileInfo fi = null;
                try
                {
                    fi = new System.IO.FileInfo(file);
                    CSVFiles.Add(new CSVFile() {
                        Name = fi.Name,
                        FileName = fi.FullName
                    });
                }
                catch (System.IO.FileNotFoundException e)
                {
                    continue;
                }
              
            }
        }
        public static void New() { }
        public static void Add() { }
        public static void Load(string name)
        {
            var res = CSVFiles.Find(a => a.Name == name);
            if(res != null) 
            {
                // Left off here--todo;
            }
        }
        public static void Update() { }
    }
}
