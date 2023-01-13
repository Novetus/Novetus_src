using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

//https://stackoverflow.com/questions/23247941/c-sharp-how-to-remove-tabcontrol-border

public partial class TabControlWithoutHeader : TabControl
{
    public TabControlWithoutHeader()
    {
        if (!DesignMode) Multiline = true;
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == 0x1328 && !DesignMode)
            m.Result = new IntPtr(1);
        else
            base.WndProc(ref m);
    }
}
