using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorldWizardsSharedObjects
{
   
    [Serializable]
    public class TilesetInfo
    {
        //public Guid Guid;
        public string Path;
        public TilesetInfo(string path)
        {
           // Guid = guid;
            Path = path;
        }
    }
}
