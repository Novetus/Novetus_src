/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 12/19/2018
 * Time: 8:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using NovetusShared;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of ClientScriptDocumentation.
	/// </summary>
	public partial class ClientScriptDocumentation : Form
	{
		public ClientScriptDocumentation()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ClientScriptDocumentationLoad(object sender, EventArgs e)
		{
			richTextBox1.Text = File.ReadAllText(GlobalVars.BasePath + "\\documentation.txt");
		}
	}
}
