using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Logi
    {
        static int checkedStatus = 0;
        static string timeStamp = null;
        static string nameOfMachineAndUser = Environment.MachineName + "_" + Environment.UserName;
        static string workingDirectory = Environment.CurrentDirectory;
        static string path = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\Logi" + nameOfMachineAndUser.ToString() + ".txt";
        public void checkOrCreateFile() //sprawdzenie istnienia plików i ewentualny zapis do nowego lub istniejącego
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                timeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                File.AppendAllText(path, timeStamp + "; Start Application - name of computer and user: " + nameOfMachineAndUser + Environment.NewLine);
            }
            else
            {
                timeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff" );
                File.AppendAllText(path, Environment.NewLine + timeStamp + "; Start Application - name of computer and user: " + nameOfMachineAndUser + Environment.NewLine);
            }
            checkedStatus = 1;
        }
        public static void addTextToFile(string tekst, string user = "(not specified/is default/doesn't exist yet)") // można zmodyfikować na asynchroniczną metodę (ale to pozniej sie zrobi)
        {
            timeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            File.AppendAllText(path, timeStamp + "; " + tekst + " - user: " + user + " and computer name: " + Environment.MachineName + Environment.NewLine);
            
        }
    }
}
