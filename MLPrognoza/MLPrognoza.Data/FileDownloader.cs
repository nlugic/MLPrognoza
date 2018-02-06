using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLPrognoza.Data
{
    public class FileDownloader
    {

        public static ICollection<string> DownloadWeatherData(int yearStart, int yearEnd, string station, ProgressBar progress)
        {
            ICollection<string> fileNames = new List<string>();

            try
            {
                progress.Minimum = 0;
                progress.Maximum = yearEnd - yearStart + 1;
                progress.Step = 1;

                for (int year = yearStart; year <= yearEnd; ++year)
                {
                    string fileName = year + "/" + station + "-" + year + ".gz";
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                    FtpWebRequest reqFileList = (FtpWebRequest)WebRequest.Create(
                        new Uri(@"ftp://ftp.ncdc.noaa.gov/pub/data/noaa/" + fileName)
                    );
                    reqFileList.Method = WebRequestMethods.Ftp.DownloadFile;

                    WebResponse response = reqFileList.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    FileStream downloadedFile = new FileStream(fileName, FileMode.Create);

                    int length = 65536, bytesRead = 0;
                    byte[] buf = new byte[length];
                    while ((bytesRead = responseStream.Read(buf, 0, length)) > 0)
                        downloadedFile.Write(buf, 0, bytesRead);

                    downloadedFile.Close();

                    string newFileName = fileName.Substring(0, fileName.Length - 3);
                    fileNames.Add(newFileName);

                    downloadedFile = new FileStream(fileName, FileMode.Open);

                    FileStream decompressedFile = File.Create(newFileName);
                    GZipStream decompression = new GZipStream(downloadedFile, CompressionMode.Decompress);
                    decompression.CopyTo(decompressedFile);

                    decompression.Close();
                    decompressedFile.Close();
                    downloadedFile.Close();
                    responseStream.Close();
                    response.Close();

                    progress.PerformStep();
                }
            }
            catch (Exception) { }
            return fileNames;
        }

    }
}
