using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kritzel.WebCast.Models;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.IO.Pipelines;

namespace Kritzel.WebCast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        const string ALLOWED_ID = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";

        static Dictionary<string, Cast> casts = new Dictionary<string, Cast>();

        static Stopwatch time = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            if (time == null)
            {
                time = new Stopwatch();
                time.Start();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Show(string id)
        {
            ShowViewModel model = new ShowViewModel();
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Load(string id)
        {
            if (!casts.ContainsKey(id))
                return NotFound("NOK");
            MemoryStream streambuffer = new MemoryStream();
            byte[] buffer = new byte[10240];
            int len;
            while((len = await Request.Body.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await streambuffer.WriteAsync(buffer, 0, len);
            }
            casts[id].SetData(streambuffer.GetBuffer());
            casts[id].Update(time.ElapsedMilliseconds);
            casts[id].version++;
            return Ok("OK");
        }

        public IActionResult Create(string id)
        {
            if (id.Length < 1 || id.Length > 10) return Ok("NOK: name");
            for(int i = 0; i < id.Length; i++)
            {
                if (!ALLOWED_ID.Contains(id[i])) return Ok("NOK: name");
            }
            if (casts.ContainsKey(id))
                return Ok("NOK: taken");
            casts.Add(id, new Cast(time.ElapsedMilliseconds, id));
            return Ok("OK");
        }

        public IActionResult Info(string id)
        {
            if (id == null) return Content("{}", "application/json");
            CastInfo ci = new CastInfo();
            if (casts.ContainsKey(id))
            {
                ci.version = casts[id].version;
                ci.livetimeRemaining = casts[id].lifetime - time.ElapsedMilliseconds;
            }
            else
            {
                ci.version = -1;
                ci.livetimeRemaining = -1;
            }
            return Content($"{{\"version\": {ci.version}, \"livetimeRemaining\": {ci.livetimeRemaining}}}", "application/json");
        }

        public IActionResult Image(string id)
        {
            if(!casts.ContainsKey(id) || casts[id].GetData() == null)
            {
                Stream nostream = System.IO.File.OpenRead(Path.Combine("wwwroot", "img", "nostream.bmp"));
                return File(nostream, "image/bmp");
            }
            return File(casts[id].GetData(), "image/bmp");
        }

        public IActionResult Download()
        {
            return View();
        }

        public IActionResult Update(string id)
        {
            if(casts.ContainsKey(id))
            {
                casts[id].Update(time.ElapsedMilliseconds);
                return Ok("OK");
            }
            return Ok("NOK");
        }

        public static void UpdateCasts()
        {
            if (casts == null)
                return;
            if (time == null)
                return;
            long currentTime = time.ElapsedMilliseconds;
            List<string> deletes = new List<string>();
            lock (casts)
            {
                foreach (KeyValuePair<string, Cast> kvp in casts)
                {
                    if(kvp.Value.lifetime < currentTime)
                    {
                        deletes.Add(kvp.Key);
                    }
                }
                foreach(string id in deletes)
                {
                    casts[id].Destroy();
                    casts.Remove(id);
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        struct CastInfo
        {
            public int version;
            public long livetimeRemaining;
        }
    }
}
