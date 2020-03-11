using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Helper
{
    public class Environment
    {
        /// <summary>
        /// Checks bin/ folder for env file
        /// </summary>
        /// <returns></returns>
        public static bool LoadEnvFile()
        {
            bool result = false;
            string env_name = ".env";
            string env_path = "";
            string workingDirectory = System.Environment.CurrentDirectory;
            string searchDirectory = Directory.GetParent(workingDirectory).Parent.FullName; ;

            // Iterates backwards through folders to find the ".env"-file
            for (int i = 0; i < 5; i++)
            {
                List<string> foundFiles = System.IO.Directory.GetFiles($@"{searchDirectory}").ToList();
                int j = foundFiles.Count(x => Path.GetFileName(x) == env_name);
                if (j < 1)
                {
                    searchDirectory = Directory.GetParent(searchDirectory).FullName;
                }
                else
                {
                    env_path = foundFiles.Find(x => Path.GetFileName(x) == env_name);
                    break;
                }
            }
            if (File.Exists(env_path))
            {
                var lines = File.ReadAllLines(env_path);
                foreach (var line in lines)
                {
                    try
                    {
                        var pair = GetKeyPair(line);
                        var key = pair[0];
                        var value = pair[1];
                        Helper.Globals.Set(key, value);
                        //res.Add(key_pair[0], key_pair[1]);
                    }
                    catch (FormatException)
                    {
                        System.Diagnostics.Debug.Write("ErrorException reading line \n");
                        throw;
                    }
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public static bool HasEnvFile()
        {
            return false;
        }
        private static string[] GetKeyPair(string line)
        {
            var regex = new Regex("( *: *)|( *= *)|( +)", RegexOptions.IgnoreCase);
            if (!regex.IsMatch(line))
            {
                throw new FormatException("Messed up");
            }
            var parts = regex.Split(line, 2);
            return new string[] { parts.First().Trim(), parts.Last().Trim() };
        }
    }
}
