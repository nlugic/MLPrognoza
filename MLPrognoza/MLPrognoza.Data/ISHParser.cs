using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLPrognoza.Data
{
    public class ISHParser
    {

        public static string ConvertISHFile(string fileName)
        {
            string destFileName = fileName + ".csv";
            string jarPath = "../../MLPrognoza_ISHParser.jar";
            
            Process clientProcess = new Process();
            clientProcess.StartInfo.FileName = "java";
            clientProcess.StartInfo.Arguments = @"-jar " + jarPath + " " + fileName + " " + destFileName;
            clientProcess.Start();
            clientProcess.WaitForExit();

            return destFileName;
        }

    }
}
