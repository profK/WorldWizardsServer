using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldWizardsServer.Pages;
using Ionic.Zip;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldWizardsServer.api
{
    [Route("api/tilesets/bundle")]
    [ApiController]
    public class TiilesetsBundleController : ControllerBase
    {
        // GET api/<TilesetsListController>/<path to bundle>
        [HttpGet("manifest/{platform}/{tileset}/{path}")]
        public Stream Get(string platform, string tileset, string path)
        {
            string manifestpath = platform + "/" + path+
                                  Path.GetFileName(path);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (FileStream zipStream = System.IO.File.OpenRead(tileset+".zip"))
            {
                ZipArchive zip = new ZipArchive(zipStream);
                ZipArchiveEntry entry = zip.GetEntry(manifestpath);
                return entry.Open();
            }
        }

        


        // POST api/<TiilesetsDownloadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TiilesetsDownloadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TiilesetsDownloadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
