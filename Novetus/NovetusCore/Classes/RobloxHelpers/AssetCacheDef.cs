using System;
using System.Collections.Generic;
using System.Text;

namespace Novetus.Core
{
    #region Asset Cache Definition
    public class AssetCacheDefBasic
    {
        public AssetCacheDefBasic(string clas, string[] id)
        {
            Class = clas;
            Id = id;
        }

        public string Class { get; set; }
        public string[] Id { get; set; }
    }

    public class AssetCacheDef : AssetCacheDefBasic
    {
        public AssetCacheDef(string clas, string[] id, string[] ext,
            string[] dir, string[] gamedir) : base(clas, id)
        {
            Ext = ext;
            Dir = dir;
            GameDir = gamedir;
        }

        public string[] Ext { get; set; }
        public string[] Dir { get; set; }
        public string[] GameDir { get; set; }
    }
    #endregion
}
