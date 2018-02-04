using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLPrognoza.Data
{
    public class FileDownloader
    {

        public static void DownloadWeatherData(string year, ProgressBar progress)
        {
            Directory.CreateDirectory(year);
            //int max = 20;
            string[] files = GetFileList(year);
            progress.Minimum = 0;
            progress.Maximum = files.Length;
            progress.Step = 1;

            for (int i = 0; i < files.Length; ++i)
            {
                Download(year, files[i]);
                progress.PerformStep();
                progress.Update();
                //if (i == max)
                //    break;
                System.Threading.Thread.Sleep(2000);
            }
        }

        public static string[] GetFileList(string year)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(@"ftp://ftp.ncdc.noaa.gov/pub/data/noaa/" + year + "/"));
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line + "\n");
                    line = reader.ReadLine();
                }
                
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch (Exception)
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
                downloadFiles = null;
                return downloadFiles;
            }
        }

        private static void Download(string year, string file)
        {
            try
            {
                string uri = "ftp://ftp.ncdc.noaa.gov/pub/data/noaa/" + year + "/" + file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                    return;

                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri("ftp://ftp.ncdc.noaa.gov/pub/data/noaa/" + year + "/" + file));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(year + @"\" + file, FileMode.Create);

                int Length = 2048;
                byte[] buffer = new byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
            }
            catch (WebException wEx)
            {
                MessageBox.Show(wEx.Message, "Download Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }

    }
}
