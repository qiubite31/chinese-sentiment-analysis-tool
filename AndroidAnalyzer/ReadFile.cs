using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AndroidAnalyzer
{
    class ReadFile
    {
        public static Dictionary<String,String> Read2ColumnCSV(String Path)
        {
            Dictionary<String,String> Collection = new Dictionary<String,String>();

            StreamReader reader = File.OpenText(Path);
            String Stream = reader.ReadLine();

            while (Stream != null)
            {
                Collection.Add(Stream.Substring(0, Stream.IndexOf(",")), Stream.Substring(Stream.IndexOf(",") + 1));

                Stream = reader.ReadLine();
            }

            return Collection;
        }
        public static String[] ReadOneRowCSV(String Path)
        {
            String[] Collection;

            StreamReader reader = File.OpenText(Path);
            String stream = reader.ReadLine();

            Collection = Regex.Split(stream, ","); //分開打成陣列

            return Collection;
        }
    }
}
