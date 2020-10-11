#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
#endregion

#region Simple HTTP Server
// Modified from https://gist.github.com/aksakalli/9191056
// MIT License - Copyright (c) 2016 Can Güney Aksakalli
// https://aksakalli.github.io/2014/02/24/simple-http-server-with-csparp.html
public class SimpleHTTPServer
{

    private readonly string[] _indexFiles = {
        "index.html",
        "index.htm",
        "index.php",
        "default.html",
        "default.htm",
        "default.php"
    };

    private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        { ".asf", "video/x-ms-asf" },
        { ".asx", "video/x-ms-asf" },
        { ".avi", "video/x-msvideo" },
        { ".bin", "application/octet-stream" },
        { ".cco", "application/x-cocoa" },
        { ".crt", "application/x-x509-ca-cert" },
        { ".css", "text/css" },
        { ".deb", "application/octet-stream" },
        { ".der", "application/x-x509-ca-cert" },
        { ".dll", "application/octet-stream" },
        { ".dmg", "application/octet-stream" },
        { ".ear", "application/java-archive" },
        { ".eot", "application/octet-stream" },
        { ".exe", "application/octet-stream" },
        { ".flv", "video/x-flv" },
        { ".gif", "image/gif" },
        { ".hqx", "application/mac-binhex40" },
        { ".htc", "text/x-component" },
        { ".htm", "text/html" },
        { ".html", "text/html" },
        { ".ico", "image/x-icon" },
        { ".img", "application/octet-stream" },
        { ".iso", "application/octet-stream" },
        { ".jar", "application/java-archive" },
        { ".jardiff", "application/x-java-archive-diff" },
        { ".jng", "image/x-jng" },
        { ".jnlp", "application/x-java-jnlp-file" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".js", "application/x-javascript" },
        { ".mml", "text/mathml" },
        { ".mng", "video/x-mng" },
        { ".mov", "video/quicktime" },
        { ".mp3", "audio/mpeg" },
        { ".mpeg", "video/mpeg" },
        { ".mpg", "video/mpeg" },
        { ".msi", "application/octet-stream" },
        { ".msm", "application/octet-stream" },
        { ".msp", "application/octet-stream" },
        { ".pdb", "application/x-pilot" },
        { ".pdf", "application/pdf" },
        { ".pem", "application/x-x509-ca-cert" },
        { ".php", "text/html" },
        { ".pl", "application/x-perl" },
        { ".pm", "application/x-perl" },
        { ".png", "image/png" },
        { ".prc", "application/x-pilot" },
        { ".ra", "audio/x-realaudio" },
        { ".rar", "application/x-rar-compressed" },
        { ".rpm", "application/x-redhat-package-manager" },
        { ".rss", "text/xml" },
        { ".run", "application/x-makeself" },
        { ".sea", "application/x-sea" },
        { ".shtml", "text/html" },
        { ".sit", "application/x-stuffit" },
        { ".swf", "application/x-shockwave-flash" },
        { ".tcl", "application/x-tcl" },
        { ".tk", "application/x-tcl" },
        { ".txt", "text/plain" },
        { ".war", "application/java-archive" },
        { ".wbmp", "image/vnd.wap.wbmp" },
        { ".wmv", "video/x-ms-wmv" },
        { ".xml", "text/xml" },
        { ".xpi", "application/x-xpinstall" },
        { ".zip", "application/zip" },
    };
    private Thread _serverThread;
    private string _rootDirectory;
    private HttpListener _listener;
    private int _port;

    public int Port
    {
        get { return _port; }
        private set { }
    }

    /// <summary>
    /// Construct server with given port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    /// <param name="port">Port of the server.</param>
    public SimpleHTTPServer(string path, int port)
    {
        this.Initialize(path, port);
    }

    /// <summary>
    /// Construct server with suitable port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    public SimpleHTTPServer(string path)
    {
        //get an empty port
        TcpListener l = new TcpListener(IPAddress.Loopback, 0);
        l.Start();
        int port = ((IPEndPoint)l.LocalEndpoint).Port;
        l.Stop();
        this.Initialize(path, port);
    }

    /// <summary>
    /// Stop server and dispose all functions.
    /// </summary>
    public void Stop()
    {
        _serverThread.Abort();
        _listener.Stop();
        GlobalVars.IsWebServerOn = false;
    }

    private void Listen()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
        _listener.Start();
        while (true)
        {
            try
            {
                HttpListenerContext context = _listener.GetContext();
                Process(context);
            }
            catch (Exception)
            {

            }
        }
    }

    private string ProcessPhpPage(string phpCompilerPath, string pageFileName, string query)
    {
        Process proc = new Process();
        proc.StartInfo.FileName = phpCompilerPath;
        var args = HttpUtility.ParseQueryString(query);
        string argString = "";
        foreach (var k in args.AllKeys)
        {
            argString += args[k] + " ";
        }
        proc.StartInfo.Arguments = "-d \"display_errors=1\" -d \"error_reporting=E_PARSE\" -d \"opcache.enable_cli=0\" -d \"opcache.enable=0\" \"" + pageFileName + "\" " + argString;
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.RedirectStandardError = true;
        proc.Start();
        string res = proc.StandardOutput.ReadToEnd();
        proc.StandardOutput.Close();
        proc.Close();
        return res;
    }

    private void Process(HttpListenerContext context)
    {
        bool HasLoaded = false;
        string filename = context.Request.Url.AbsolutePath;
        filename = filename.Substring(1);

        if (string.IsNullOrEmpty(filename))
        {
            foreach (string indexFile in _indexFiles)
            {
                if (File.Exists(Path.Combine(_rootDirectory, indexFile)))
                {
                    filename = indexFile;
                    break;
                }
            }
        }

        filename = Path.Combine(_rootDirectory, filename);

        if (File.Exists(filename))
        {
            try
            {
                var ext = new FileInfo(filename);

                if (ext.Extension == ".php")
                {
                    string output = ProcessPhpPage(GlobalPaths.ConfigDirData + "\\php\\php.exe", filename, context.Request.Url.Query);
                    byte[] input = ASCIIEncoding.UTF8.GetBytes(output);
                    //Adding permanent http response headers
                    string mime;
                    context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                    context.Response.ContentLength64 = input.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));
                    context.Response.OutputStream.Write(input, 0, input.Length);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                    HasLoaded = true;
                }
                else
                {
                    Stream input = new FileStream(filename, FileMode.Open);
                    //Adding permanent http response headers
                    string mime;
                    context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                    context.Response.ContentLength64 = input.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                    byte[] buffer = new byte[1024 * 16];
                    int nbytes;
                    while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        context.Response.OutputStream.Write(buffer, 0, nbytes);
                    input.Close();

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                    HasLoaded = true;
                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

        }

        if (HasLoaded)
        {
            context.Response.OutputStream.Close();
        }
    }

    private void Initialize(string path, int port)
    {
        this._rootDirectory = path;
        this._port = port;
        _serverThread = new Thread(this.Listen);
        _serverThread.Start();
        GlobalVars.IsWebServerOn = true;
    }
}
#endregion