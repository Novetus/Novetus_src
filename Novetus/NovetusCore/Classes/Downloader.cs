﻿#region Usings
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
#endregion

#region Downloader

class Downloader
{
    public readonly string fileURL;
    public readonly string fileName;
    public readonly string fileFilter;
    public readonly string filePath;
    private string downloadOutcome;
    private static string downloadOutcomeException;

    public Downloader(string url, string name, string filter)
    {
        fileName = name;
        fileURL = url;
        fileFilter = filter;
    }

    public Downloader(string url, string name, string filter, string path)
    {
        fileName = name;
        fileURL = url;
        fileFilter = filter;
        filePath = path;
    }

    public Downloader(string url, string name)
    {
        fileName = name;
        fileURL = url;
        fileFilter = "";
    }

    public void setDownloadOutcome(string text)
    {
        downloadOutcome = text;
    }

    public string getDownloadOutcome()
    {
        return downloadOutcome;
    }

    public void InitDownload(string path, string fileext, string additionalText = "")
    {
        InitDownload(path, fileext, additionalText);
    }

    public void InitDownload(string path, string fileext, string additionalText, bool removeSpaces = false)
    {
        string outputfilename = "";

        if (removeSpaces == true)
        {
            outputfilename = (fileName + fileext).Replace(" ", "");
        }
        else
        {
            outputfilename = fileName + fileext;
        }
        
        string fullpath = path + "\\" + outputfilename;

        InitDownloadNoDialog(fullpath, additionalText);
    }

    public void InitDownload(string additionalText = "")
    {
        SaveFileDialog saveFileDialog1 = new SaveFileDialog
        {
            FileName = fileName,
            //"Compressed zip files (*.zip)|*.zip|All files (*.*)|*.*"
            Filter = fileFilter,
            Title = "Save " + fileName
        };

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            InitDownloadNoDialog(saveFileDialog1.FileName, additionalText);
        }
    }

    public void InitDownloadNoDialog(string name, string additionalText = "")
    {
        int read = 0;

        try
        {
            read = DownloadFile(fileURL, name);
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
            downloadOutcome = "Error when downloading file: " + ex.Message;
        }
        finally
        {
            //wait a few seconds for the download to finish
            //Thread.Sleep(2000);
            if (File.Exists(name) && read > 0)
            {
                downloadOutcome = "File " + Path.GetFileName(name) + " downloaded! " + GlobalFuncs.SizeSuffix(Convert.ToInt64(read), 2) + " written (" + read + " bytes)! " + additionalText;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(downloadOutcomeException))
                {
                    downloadOutcome = "Error: Download of file " + Path.GetFileName(name) + " failed. " + downloadOutcomeException;
                }
                else
                {
                    downloadOutcome = "Error: Download of file " + Path.GetFileName(name) + " failed. The file wasn't downloaded to the assigned directory.";
                }
            }
        }
    }

    public string GetFullDLPath()
    {
        return filePath + Path.DirectorySeparatorChar + fileName;
    }

    private static int DownloadFile(string remoteFilename, string localFilename)
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
        //thanks to https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0 for the net 4.0 compatible TLS 1.1/1.2 code!
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | (SecurityProtocolType)3072
                | (SecurityProtocolType)768
                | SecurityProtocolType.Ssl3;
            // Create a request for the specified remote file name
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(remoteFilename);
            //changing it to just "roblox" since roblox is breaking everything.
            //request.UserAgent = "Roblox/WinINet";
            request.UserAgent = "Roblox";
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            GlobalFuncs.LogExceptions(e);
#endif
            if (e is WebException && bytesProcessed == 0)
            {
                WebException ex = (WebException)e;
                HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                if (errorResponse.StatusCode == HttpStatusCode.Conflict)
                {
                    downloadOutcomeException = "Error: Unable to download item. Is it publically available?";
                }
                else
                {
                    downloadOutcomeException = "Exception: " + ex.Message;
                }
            }
            else
            {
                downloadOutcomeException = "Exception: " + e.Message;
            }
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
}
#endregion