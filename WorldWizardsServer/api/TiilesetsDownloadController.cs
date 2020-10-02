using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldWizardsServer.Pages;
using Ionic.Zip;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldWizardsServer.api
{
    [Route("api/tilesets/download")]
    [ApiController]
    public class TiilesetsDownloadController : ControllerBase
    {
        // GET: api/<TiilesetsDownloadController>
        [HttpGet]
        public FileStream Get(string platform, string tileset, int version)
        {
            //NOTE: Version currently unimplemented
            var path = Tilesets.TILESETDIR + "/" + tileset + "/" + platform + "/tileset_" + tileset.ToLower();
            return new FileStream(path, FileMode.Open, FileAccess.Read);
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
