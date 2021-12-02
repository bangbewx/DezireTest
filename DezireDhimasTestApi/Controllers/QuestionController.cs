using DezireDhimasTestApi.Model;
using DezireDhimasTestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace DezireDhimasTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {

        private ILogger _logger;
        private IQueService _service;
        public QuestionController(ILogger<QuestionController> logger, IQueService service)
        {
            _logger = logger;
            _service = service;

        }

        [HttpGet("/backend/question")]
        public string GetTest()
        {
            return "Dhimas Suharja";
        }

        [HttpGet("/backend/question/one")]
        public ActionResult<MQueOne> GetOne()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri("https://screening.moduit.id/backend/question/one");

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                var deserialized = JsonConvert.DeserializeObject<OneResp>(responseString);
                _service.GetOne().id = deserialized.id;
                _service.GetOne().category = deserialized.category;
                _service.GetOne().description = deserialized.description;
                _service.GetOne().footer = deserialized.footer;
                _service.GetOne().createdAt = deserialized.createdAt;
            }

            return _service.GetOne();
        }

        [HttpGet("/backend/question/two")]
        public ActionResult<List<MQueTwo>> GetTwo()
        {
            List<MQueTwo> li = new List<MQueTwo>();
            using (var client = new HttpClient())
            {
                var uri = new Uri("https://screening.moduit.id/backend/question/two");

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                var deserialized = JsonConvert.DeserializeObject<List<TwoResp>>(responseString);
                var liFilDesc = deserialized.Where(o => o.description.IndexOf("Ergonomic") >= 0).ToList();
                var liFilTitle = deserialized.Where(o => o.title.IndexOf("Ergonomic") >= 0).ToList();

                var liFilJoin = liFilDesc.Union(liFilTitle).ToList();

                var liFilTag = liFilJoin.Where(o => o.tags != null && o.tags.Contains("Sports")).ToList();

                var liOrder = liFilTag.OrderByDescending(o => o.id).Take(3).ToList();

                for (int i = 0; i < liOrder.Count; i++)
                {
                    MQueTwo dt = new MQueTwo();
                    dt.id = liOrder[i].id;
                    dt.category = liOrder[i].category;
                    dt.description = liOrder[i].description;
                    dt.footer = liOrder[i].footer;
                    dt.createdAt = liOrder[i].createdAt;
                    li.Add(dt);
                }
            }
            _service.GetTwo().AddRange(li);

            return _service.GetTwo();
        }

        [HttpGet("/backend/question/three")]
        public ActionResult<List<MQueThree>> PostThree(InputThree data)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri("https://screening.moduit.id/backend/question/three");

                List<MQueThree> li = new List<MQueThree>();

                var param = from p in data.GetType().GetProperties()
                            where p.GetValue(data, null) != null
                            select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(data, null).ToString());

                string uriString = uri + "?" + String.Join("&", param.ToArray());
                var response = client.GetAsync(uriString).Result;


                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;


                var deserialized = JsonConvert.DeserializeObject<List<ThreeResp>>(responseString);

                for (int i = 0; i < deserialized.Count; i++)
                {
                    MQueThree dt = new MQueThree();
                    dt.id = deserialized[i].id;
                    dt.category = deserialized[i].category;
                    if (deserialized[i].items != null)
                        for (int u = 0; u < deserialized[i].items.Count; u++)
                        {
                            dt.title = deserialized[i].items[u].title;
                            dt.description = deserialized[i].items[u].description;
                            dt.footer = deserialized[i].items[u].footer;
                        }
                    dt.createdAt = deserialized[i].createdAt;
                    li.Add(dt);
                }
                _service.PostThree().AddRange(li);
            }
            return _service.PostThree();
        }
    }
}
