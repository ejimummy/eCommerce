using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Controllers
{
    //annotations to indicate the controller will produce back a JSON
    //route is a way to tell it to come to this controller
    [Produces("application/json")]
    [Route("api/Pic")]
    public class PicController : Controller
    {
        //All we need are actions (methods) in this controller

        //this is how to get the environment creating an environment variable
        //that is readonly (is set, no one can change it, can be changed in 
        //constructor.
        private readonly IHostingEnvironment _env;

        //constructor to know where the environment is being held (wwwroot)
        public PicController(IHostingEnvironment env)
        {
            _env = env;
        }

        //the default method in this controller is the GetImage method
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetImage(int id)
        {
            //every web application as a webroot folder
            //asking environment to get to the webroot
            var webRoot = _env.WebRootPath;
            var path = Path.Combine(webRoot + "/pics/", "shoes-" + id + ".png");

            //cannot just return the path back, need to grab the binary file for the picture
            //send the bytes back and they can be rendered in the browser as a picture
            var buffer = System.IO.File.ReadAllBytes(path);

            //send it back to the ui, overriding the return of a json by specifying image
            return File(buffer, "image/png");
        }
    }
}