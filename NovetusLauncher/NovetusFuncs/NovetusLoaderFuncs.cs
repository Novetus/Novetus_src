#region Usings
using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.Net;
#endregion

#region Downloader
class Downloader
{
    private readonly string fileURL;
    private readonly string fileName;
    private readonly string fileFilter;
    private string downloadOutcome;
    private static string downloadOutcomeException;

    public Downloader(string url, string name, string filter)
    {
        fileName = name;
        fileURL = url;
        fileFilter = filter;
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
        string downloadOutcomeAddText = additionalText;

        string outputfilename = fileName + fileext;
        string fullpath = path + "\\" + outputfilename;

        try
        {
            int read = DownloadFile(fileURL, fullpath);
            downloadOutcome = "File " + outputfilename + " downloaded! " + read + " bytes written! " + downloadOutcomeAddText + downloadOutcomeException;
        }
        catch (Exception ex)
        {
            downloadOutcome = "Error when downloading file: " + ex.Message;
        }
    }

    public void InitDownload(string additionalText = "")
    {
        string downloadOutcomeAddText = additionalText;

        SaveFileDialog saveFileDialog1 = new SaveFileDialog
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
                int read = DownloadFile(fileURL, saveFileDialog1.FileName);
                downloadOutcome = "File " + Path.GetFileName(saveFileDialog1.FileName) + " downloaded! " + read + " bytes written! " + downloadOutcomeAddText + downloadOutcomeException;
            }
            catch (Exception ex)
            {
                downloadOutcome = "Error when downloading file: " + ex.Message;
            }
        }
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
            request.UserAgent = "Roblox/WinINet";
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
            downloadOutcomeException = " Exception detected: " + e.Message;
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

#region Addon Loader
public class AddonLoader
{
    private readonly OpenFileDialog openFileDialog1;
    private string installOutcome = "";
    private int fileListDisplay = 0;

    public AddonLoader()
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an addon .zip file",
            Filter = "Compressed zip files (*.zip)|*.zip",
            Title = "Open addon .zip"
        };
    }

    public void setInstallOutcome(string text)
    {
        installOutcome = text;
    }

    public string getInstallOutcome()
    {
        return installOutcome;
    }

    public void setFileListDisplay(int number)
    {
        fileListDisplay = number;
    }

    public void LoadAddon()
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                int filecount = 0;
                StringBuilder filelistbuilder = new StringBuilder();

                using (Stream str = openFileDialog1.OpenFile())
                {
                    using (var zipFile = ZipFile.Read(str))
                    {
                        ZipEntry[] entries = zipFile.Entries.ToArray();

                        foreach (ZipEntry entry in entries)
                        {
                            filelistbuilder.Append(entry.FileName + " ("+ entry.UncompressedSize +")");
                            filelistbuilder.Append(Environment.NewLine);
                        }

                        zipFile.ExtractAll(Directories.BasePath, ExtractExistingFileAction.OverwriteSilently);
                    }
                }

                string filelist = filelistbuilder.ToString();

                if (filecount > fileListDisplay)
                {
                    installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist + Environment.NewLine + "and " + (filecount - fileListDisplay) + " more files!";
                }
                else
                {
                    installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist;
                }
            }
            catch (Exception ex)
            {
                installOutcome = "Error when installing addon: " + ex.Message;
            }
        }
    }
}
#endregion

#region Icon Loader
public class IconLoader
{
    private OpenFileDialog openFileDialog1;
    private string installOutcome = "";

    public IconLoader()
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an icon .png file",
            Filter = "Portable Network Graphics image (*.png)|*.png",
            Title = "Open icon .png"
        };
    }

    public void setInstallOutcome(string text)
    {
        installOutcome = text;
    }

    public string getInstallOutcome()
    {
        return installOutcome;
    }

    public void LoadImage()
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                using (Stream str = openFileDialog1.OpenFile())
                {
                    using (Stream output = new FileStream(Directories.extradir + "\\icons\\" + GlobalVars.UserConfiguration.PlayerName + ".png", FileMode.Create))
                    {
                        byte[] buffer = new byte[32 * 1024];
                        int read;

                        while ((read = str.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, read);
                        }
                    }

                    str.Close();
                }

                installOutcome = "Icon " + openFileDialog1.SafeFileName + " installed!";
            }
            catch (Exception ex)
            {
                installOutcome = "Error when installing icon: " + ex.Message;
            }
        }
    }
}
#endregion