using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WorldWizardsServer.Pages;
using Ionic.Zip;
using Microsoft.AspNetCore.Http.Features;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldWizardsServer.api
{
    //
    [Route("api/tilesets/manifest")]
    [ApiController]
    public class TilesetManifestController : ControllerBase
    {
        

        // GET api/tilesets/manifest?platform=" + platform + "&tileset=" + tilesetName + "&version=" + version;
        //[HttpGet("{platform},{tileset},{version}")]
        public FileStream Get(string platform,string tileset,int version)
        {
            //NOTE: Version currently unimplemented
          
            var path = Tilesets.TILESETDIR+"/"+tileset + "/" + platform + "/" + platform;
            return new FileStream(path, FileMode.Open, FileAccess.Read);
            //turn MakeStreamResponse(path);
        }

       

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
