using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;
using System.Xml;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Mvc;
using WorldWizardsServer.Pages;
using Newtonsoft.Json;
using WorldWizardsSharedObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldWizardsServer.api
{
    [Route("api/tilesets")]
    [ApiController]
    public class TilesetsListController : ControllerBase
    {
        private Dictionary<Guid, string> tilesetIDs = new Dictionary<Guid, string>();
        // GET: api/<TilesetsListController>
     
      
        

        [HttpGet]
        public string Get()
        {
            // list files
            // currently lists all tiles and such on the server
            // in the futue will apply ownership
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string[] tilesetnames = Directory.GetFiles(Tilesets.TILESETDIR,"*.zip");
            List<TilesetDirIndex> indexList = new List<TilesetDirIndex>();
            foreach(string tname in tilesetnames)
            {
                string tnameFixed = tname.Replace("\\","/");
                using (FileStream zipStream = System.IO.File.OpenRead(tnameFixed))
                {
                    ZipArchive zip = new ZipArchive(zipStream);
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        if (entry.Name.EndsWith(".idx"))
                        {
                            string idxStr = new StreamReader(entry.Open()).ReadToEnd();
                            TilesetDirIndex idx = JsonConvert.DeserializeObject<TilesetDirIndex>(idxStr);
                            indexList.Add(idx);
                        }
                    }
                }
            }
            
            return JsonConvert.SerializeObject(indexList);
        }

        // GET api/<TilesetsListController>/<path to bundle>
        [HttpGet("manifest/{platform}/{tileset}/{path}")]
        public Stream Get(string platform, string tileset, string path)
        {
            string manifestpath = platform + "/" + path+
                                  Path.GetFileName(path)+".manifest";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (FileStream zipStream = System.IO.File.OpenRead(tileset+".zip"))
            {
                ZipArchive zip = new ZipArchive(zipStream);
                ZipArchiveEntry entry = zip.GetEntry(manifestpath);
                return entry.Open();
            }
        }

        // POST api/<TilesetsListController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TilesetsListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TilesetsListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
