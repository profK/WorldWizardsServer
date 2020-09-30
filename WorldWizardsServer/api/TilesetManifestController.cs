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
        public HttpResponseMessage Get(string platform,string tileset,int version)
        {
            //NOTE: Version currently unimplemented
            var path = Tilesets.TILESETDIR+"/"+tileset + "/" + platform + "/" + platform + ".manifest";
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return MakeStreamResponse(stream);
        }

        private HttpResponseMessage MakeStreamResponse(Stream stream)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
         
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            return result;
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
