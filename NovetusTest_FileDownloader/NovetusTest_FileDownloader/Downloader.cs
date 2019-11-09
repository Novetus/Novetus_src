using System;
using System.Net;
using System.Windows.Forms;
using System.IO;

class Downloader
{
    private string fileURL;
    private string fileName;
    private string fileFilter;
    private string downloadOutcome;
    private string downloadOutcomeAddText;
    private ProgressBar downloadProgress;
    private SaveFileDialog saveFileDialog1;

    public Downloader(string url, string name, string filter, ProgressBar progress)
    {
        fileName = name;
        fileURL = url;
        fileFilter = filter;
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
            FileName = fileName,
            //"Compressed zip files (*.zip)|*.zip|All files (*.*)|*.*"
            Filter = fileFilter,
            Title = "Save " + fileName
        };

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                int read = DownloadGZipFile(fileURL, saveFileDialog1.FileName);
                downloadOutcome = "File " + Path.GetFileName(saveFileDialog1.FileName) + " downloaded! " + read + " bytes written!" + downloadOutcomeAddText;
            }
            catch (Exception ex)
            {
                downloadOutcome = "Error when downloading file: " + ex.Message;
            }
        }
    }

    public static int DownloadGZipFile(string remoteFilename, string localFilename)
    {
		//credit to Tom Archer (https://www.codeguru.com/columns/dotnettips/article.php/c7005/Downloading-Files-with-the-WebRequest-and-WebResponse-Classes.htm)
		//and Brokenglass (https://stackoverflow.com/questions/4567313/uncompressing-gzip-response-from-webclient/4567408#4567408)
		
        // Function will return the number of bytes processed
        // to the caller. Initialize to 0 here.
        int bytesProcessed = 0;

        // Assign values to these objects here so that they can
        // be referenced in the finally block
        Stream remoteStream = null;
        Stream localStream = null;
        WebResponse response = null;

        // Use a try/catch/finally block as both the WebRequest and Stream
        // classes throw exceptions upon error
        try
        {
            // Create a request for the specified remote file name
            var request = (HttpWebRequest)WebRequest.Create(remoteFilename);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            if (request != null)
            {
                // Send the request to the server and retrieve the
                // WebResponse object 
                response = request.GetResponse();
                if (response != null)
                {
                    // Once the WebResponse object has been retrieved,
                    // get the stream object associated with the response's data
                    remoteStream = response.GetResponseStream();

                    // Create the local file
                    localStream = File.Create(localFilename);

                    // Allocate a 1k buffer
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    // Simple do/while loop to read from stream until
                    // no bytes are returned
                    do
                    {
                        // Read data (up to 1k) from the stream
                        bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                        // Write the data to the local file
                        localStream.Write(buffer, 0, bytesRead);

                        // Increment total bytes processed
                        bytesProcessed += bytesRead;
                    } while (bytesRead > 0);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            // Close the response and streams objects here 
            // to make sure they're closed even if an exception
            // is thrown at some point
            if (response != null) response.Close();
            if (remoteStream != null) remoteStream.Close();
            if (localStream != null) localStream.Close();
        }

        // Return total bytes processed to caller.
        return bytesProcessed;
    }

    void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        downloadProgress.Value = e.ProgressPercentage;
    }
}
