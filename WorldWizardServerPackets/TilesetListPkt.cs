using System;
using System.Collections.Generic;
using System.Text;

namespace WorldWizardServerPackets
{
    [Serializable]
    public class TilesetInfoPkt
    {
        public Dictionary<string, string> tilesetByGUID;

        public TilesetInfoPkt(Dictionary<string, string> guidDict)
        {
            tilesetByGUID = guidDict;
        }
    }
}
