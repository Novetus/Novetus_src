using System.Windows.Forms;

public partial class ClientinfoCreatorValidatePathForm : Form
{
    private ClientinfoEditor FormParent;

    public ClientinfoCreatorValidatePathForm(ClientinfoEditor par)
    {
        InitializeComponent();
        FormParent = par;
    }

    private void ClientinfoCreatorValidatePathForm_Load(object sender, System.EventArgs e)
    {
        CenterToScreen();
    }

    private void applyButton_Click(object sender, System.EventArgs e)
    {
        FormParent.RelativePath = TextEntry.Text;
        Close();
    }
}
