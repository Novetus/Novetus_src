/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Net;

//made by aksakalli
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
 
	public int Port {
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
		while (true) {
			try {
				HttpListenerContext context = _listener.GetContext();
				Process(context);
			} catch (Exception) {
 
			}
		}
	}
    
	private string ProcessPhpPage(string phpCompilerPath, string pageFileName)
	{
		Process proc = new Process();
		proc.StartInfo.FileName = phpCompilerPath;
		proc.StartInfo.Arguments = "-d \"display_errors=1\" -d \"error_reporting=E_PARSE\" \"" + pageFileName + "\"";
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
		string filename = context.Request.Url.AbsolutePath;
		filename = filename.Substring(1);
 
		if (string.IsNullOrEmpty(filename)) {
			foreach (string indexFile in _indexFiles) {
				if (File.Exists(Path.Combine(_rootDirectory, indexFile))) {
					filename = indexFile;
					break;
				}
			}
		}
 
		filename = Path.Combine(_rootDirectory, filename);
 
		if (File.Exists(filename)) {
			try {
				var ext = new FileInfo(filename);
            	
				if (ext.Extension == ".php") {
					string output = ProcessPhpPage(GlobalVars.ConfigDir + "\\php\\php.exe", filename);
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
				} else {
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
				}
			} catch (Exception) {
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}
 
		} else {
			if (context.Request.HttpMethod.Equals("GET") && filename.Contains("bodycolors.rbxm", StringComparison.OrdinalIgnoreCase)) {
				string output = WebServerGenerator.GenerateBodyColorsXML();
				byte[] input = ASCIIEncoding.UTF8.GetBytes(output);
				context.Response.ContentType = "text/xml";
				context.Response.ContentLength64 = input.Length;
				context.Response.OutputStream.Write(input, 0, input.Length);
				context.Response.StatusCode = (int)HttpStatusCode.OK;
				context.Response.OutputStream.Flush();
			} else {
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			}
		}
        
		context.Response.OutputStream.Close();
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
	
public static class WebServerGenerator
{
	public static string GenerateBodyColorsXML()
	{
		string xmltemplate = GlobalVars.MultiLine(File.ReadAllLines(GlobalVars.CustomPlayerDir + "\\BodyColors.xml"));
		string xml = String.Format(xmltemplate, GlobalVars.HeadColorID, GlobalVars.LeftArmColorID, GlobalVars.LeftLegColorID, GlobalVars.RightArmColorID, GlobalVars.RightLegColorID, GlobalVars.TorsoColorID);
		return xml;
	}
}