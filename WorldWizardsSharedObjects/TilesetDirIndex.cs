using System;
using System.Collections.Generic;


namespace WorldWizardsSharedObjects
{
    [Serializable]
    public class TilesetDirIndex 
    {
        public string name;
        public List<TilesetDirIndex> subDirectories;
        public List<string> filenames;

        public TilesetDirIndex(string name)
        {
            this.name = name;
            subDirectories = new List<TilesetDirIndex>();
            filenames = new List<string>();
        }

        

    }
}
