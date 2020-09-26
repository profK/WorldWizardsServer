using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorldWizardsServer.Pages
{
    public class TilesetsModel : PageModel
    {
        private const string TILESETDIR = "./data/tilesets";
        public string[] TilesetNames;

        public void OnGet()
        {

            if (!Directory.Exists(TILESETDIR))
            {
                Directory.CreateDirectory(TILESETDIR);
            }
            string[] fullnames = Directory.GetDirectories(TILESETDIR);
            List<string> tilesetList = new List<string>();
            foreach (string fullname in fullnames)
            {
                tilesetList.Add(Path.GetFileName(fullname));
            }
            TilesetNames = tilesetList.ToArray();
        }
    }
}