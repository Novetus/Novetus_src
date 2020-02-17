using GitHubUpdate;
using System.Windows.Forms;

public class Updater
{
    private string GitHubName = "";
    private string GitHubRepo = "";

    public Updater()
    {
    }

    public void setName(string text)
    {
        GitHubName = text;
    }

    public void setRepo(string text)
    {
        GitHubRepo = text;
    }

    public async void UpdateNovetus()
    {
        var checker = new UpdateChecker(GitHubName, GitHubRepo, GlobalVars.Version);

        UpdateType update = await checker.CheckUpdate();

        if (update != UpdateType.None)
        {
            // Ask the user if he wants to update
            // You can use the prebuilt form for this if you want (it's really pretty!)
            var result = new UpdateNotifyDialog(checker).ShowDialog();
            if (result == DialogResult.Yes)
            {
                checker.DownloadAsset("Release-" + checker.getCurrentRelease().TagName + ".zip"); // opens it in the user's browser
                if (!string.IsNullOrWhiteSpace(checker.getDownloadOutcome()))
                {
                    MessageBox.Show(checker.getDownloadOutcome(), "Novetus Updater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
