using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

class Downloader
{
    private string fileURL;
    private string fileName;
    private string fullFileName;
    private string fileEXT;
    private string fileFilter;
    private string downloadOutcome;
    private string downloadOutcomeAddText;
    private ProgressBar downloadProgress;
    private SaveFileDialog saveFileDialog1;

    public Downloader(string url, string name, string ext, string filter, ProgressBar progress)
    {
        fileName = name;
        fileEXT = ext;
        fileURL = url;
        fileFilter = filter;
        fullFileName = fileName + fileEXT;
        downloadProgress = progress;
    }

    public void setDownloadOutcome(string text)
    {
        downloadOutcome = text;
    }

    public string getDownloadOutcome()
    {
        return downloadOutcome;
    }

    public void InitDownload(string additionalText = "")
    {
        downloadOutcomeAddText = additionalText;

        saveFileDialog1 = new SaveFileDialog()
        {
            FileName = fullFileName,
            //"Compressed zip files (*.zip)|*.zip|All files (*.*)|*.*"
            Filter = fileFilter,
            Title = "Save " + fullFileName
        };

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                MessageBox.Show(saveFileDialog1.FileName);

                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(new Uri(fileURL), saveFileDialog1.FileName);
                }

                downloadOutcome = "File " + fullFileName + " downloaded!" + downloadOutcomeAddText;
            }
            catch (Exception ex)
            {
                downloadOutcome = "Error when downloading file: " + ex.Message;
            }
        }
    }

    void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        downloadProgress.Value = e.ProgressPercentage;
    }
}
