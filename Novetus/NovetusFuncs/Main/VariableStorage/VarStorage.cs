#region Usings
using System.Drawing;
#endregion

#region Variable Storage
public class VarStorage
{
    #region Asset Cache Definition
    public class AssetCacheDef
    {
        public AssetCacheDef(string clas, string[] id, string[] ext,
            string[] dir, string[] gamedir)
        {
            Class = clas;
            Id = id;
            Ext = ext;
            Dir = dir;
            GameDir = gamedir;
        }

        public string Class { get; set; }
        public string[] Id { get; set; }
        public string[] Ext { get; set; }
        public string[] Dir { get; set; }
        public string[] GameDir { get; set; }
    }
    #endregion

    #region Part Colors
    public class PartColors
    {
        public int ColorID { get; set; }
        public Color ButtonColor { get; set; }
    }

    #endregion
}
#endregion
