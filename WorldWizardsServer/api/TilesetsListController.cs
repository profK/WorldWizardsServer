using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ionic.Zip;
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
            string[] tilesetnames = Directory.GetFiles(Tilesets.TILESETDIR,"*.zip");
            List<TilesetInfo> tlist = new List<TilesetInfo>();
            foreach(string tname in tilesetnames)
            {
                using (ZipInputStream zip = new ZipInputStream(tname))
                {
                    ZipEntry entry = zip.GetNextEntry();
                    bool foundIdx = false;
                    while (!foundIdx){
                        if (entry.FileName.EndsWith(".idx"))
                        {
                            foundIdx = true;
                        }
                        else
                        {
                            entry = zip.GetNextEntry();
                        }
                    } 
                    // did we find an index?
                    if (foundIdx)
                    {
                        
                    }
                    
                }
            }
            foreach(KeyValuePair<Guid,String> tuple in tilesetIDs)
            {
                tlist.Add(new TilesetInfo(tuple.Value,tuple.Key));
            }
            return JsonConvert.SerializeObject(tlist);
        }

        // GET api/<TilesetsListController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
