using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorldWizardsSharedObjects
{
    [Serializable]
    public abstract class AssetTreeNode
    {
        public string Name;

        public AssetTreeNode(string name)
        {
            Name = name;
        }

        public AssetTreeNode()
        {
            
        }
    }

    [Serializable]
    public class AssetTreeDir : AssetTreeNode
    {
        public List<AssetTreeNode> Children;

        public AssetTreeDir(string name) : base(name)
        {
            Children = new List<AssetTreeNode>();
        }

        public AssetTreeDir() : base()
        {
        }
    }

    [Serializable]
    public class AssetTreeBundle: AssetTreeNode
    {
        public AssetTreeBundle(string name) : base(name)
        {
            
        }
    }
   
}
