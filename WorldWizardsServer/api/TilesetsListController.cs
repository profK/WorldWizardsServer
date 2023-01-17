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

        [HttpGet("{bundle}")]
        public Stream Get(string bundlepath)
        {
            string pathRoot = GetRootDir(bundlepath);
            string platform = GetRootDir(bundlepath.Substring(pathRoot.Length + 1));
            string entryPath = bundlepath.Substring((pathRoot.Length + platform.Length + 2))
                .Replace('\\','/');
            string entryName = entryPath.Replace('/', '.').ToLower();
            string zipPath = Path.Combine(Tilesets.TILESETDIR,pathRoot + ".zip");
            FileStream zipStream =
                System.IO.File.OpenRead(zipPath);
            ZipArchive zip = new ZipArchive(zipStream);
            //string entryPath = bundlepath.Substring(pathRoot.Length + 1);
           
            ZipArchiveEntry entry = zip.GetEntry(
                platform+"/"+entryPath+"/"+entryName+".assets");
            return entry.Open();

          
        }

        public static string GetRootDir( string path)
        {
            var root = Path.GetPathRoot(path);
            while (true)
            {
                var temp = Path.GetDirectoryName(path);
                if (temp != null && temp.Equals(root))
                    break;
                path = temp;
            }

            return path;
        }
        
        [HttpGet("hashcode")]
        public string GetHash(string bundlepath)
        {
            // this is a quick kludge
           
            string pathRoot = GetRootDir(bundlepath);
            string platform = GetRootDir(bundlepath.Substring(pathRoot.Length + 1));
            string entryPath = bundlepath.Substring((pathRoot.Length + platform.Length + 2))
                .Replace('\\','/');
            string entryName = entryPath.Replace('/', '.').ToLower();
            string zipPath = Path.Combine(Tilesets.TILESETDIR,pathRoot + ".zip");
            FileStream zipStream =
                System.IO.File.OpenRead(zipPath);
            ZipArchive zip = new ZipArchive(zipStream);
            //string entryPath = bundlepath.Substring(pathRoot.Length + 1);
           
            ZipArchiveEntry entry = zip.GetEntry(
                platform+"/"+entryPath+"/"+entryName+".assets.manifest");
            using (Stream zipInputStream = entry.Open())
            {
                StreamReader rdr = new StreamReader(zipInputStream);
                while (!rdr.EndOfStream)
                {
                    string line = rdr.ReadLine().Trim();
                    if (line.StartsWith("CRC:"))
                    {
                        int start = line.IndexOf("CRC:") + 4;
                        string numstr = line.Substring(start+1);
                        ulong hash = ulong.Parse(numstr);
                        return JsonConvert.SerializeObject(new BundleHashCode(hash));
                    }
                }
            }

            return JsonConvert.SerializeObject(new BundleHashCode()); // return invalid hashcode 
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
