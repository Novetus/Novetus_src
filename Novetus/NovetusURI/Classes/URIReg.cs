#region Usings
using Microsoft.Win32;
using System;
using System.Windows.Forms;
#endregion

#region URI Registration
//code based off https://stackoverflow.com/questions/35626050/registering-custom-url-handler-in-c-sharp-on-windows-8

public class URIReg
{
    private static string _Protocol = "";
    private static string _ProtocolHandler = "";

    private static readonly string _launch = string.Format(
        "{0}{1}{0} {0}%1{0}", (char)34, Application.ExecutablePath);

    private static readonly Version _win8Version = new Version(6, 2, 9200, 0);

    private static readonly bool _isWin8 =
        Environment.OSVersion.Platform == PlatformID.Win32NT &&
        Environment.OSVersion.Version >= _win8Version;

    public URIReg(string protocol, string protocolhandle)
    {
        _Protocol = protocol;
        _ProtocolHandler = protocolhandle;
    }

    public void Register()
    {
        if (_isWin8) RegisterWin8();
        else RegisterWin7();
    }

    private static void RegisterWin7()
    {
        var regKey = Registry.ClassesRoot.CreateSubKey(_Protocol);

        regKey.CreateSubKey("DefaultIcon")
            .SetValue(null, string.Format("{0}{1},1{0}", (char)34,
                Application.ExecutablePath));

        regKey.SetValue(null, "URL:" + _Protocol + " Protocol");
        regKey.SetValue("URL Protocol", "");

        regKey = regKey.CreateSubKey(@"shell\open\command");
        regKey.SetValue(null, _launch);
    }

    private static void RegisterWin8()
    {
        RegisterWin7();

        var regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes")
            .CreateSubKey(_ProtocolHandler);

        regKey.SetValue(null, _Protocol);

        regKey.CreateSubKey("DefaultIcon")
             .SetValue(null, string.Format("{0}{1},1{0}", (char)34,
                 Application.ExecutablePath));

        regKey.CreateSubKey(@"shell\open\command").SetValue(null, _launch);

        Registry.LocalMachine.CreateSubKey(string.Format(
            @"SOFTWARE\{0}\{1}\Capabilities\ApplicationDescription\URLAssociations",
            Application.CompanyName, Application.ProductName))
            .SetValue(_Protocol, _ProtocolHandler);

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\RegisteredApplications")
            .SetValue(Application.ProductName, string.Format(
                @"SOFTWARE\{0}\Capabilities", Application.ProductName));
    }

    public void Unregister()
    {
        if (!_isWin8)
        {
            Registry.ClassesRoot.DeleteSubKeyTree(_Protocol, false);
            return;
        }

        // extra work required.
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes")
            .DeleteSubKeyTree(_ProtocolHandler, false);

        Registry.LocalMachine.DeleteSubKeyTree(string.Format(@"SOFTWARE\{0}\{1}",
            Application.CompanyName, Application.ProductName));

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\RegisteredApplications")
            .DeleteValue(Application.ProductName);
    }
}
#endregion