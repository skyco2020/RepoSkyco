using Newtonsoft.Json;
using RestSharp;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace StreamingVideo.Movies
{
    public  class MovieService
    {       
        public  ModelObj GetMovie()
        {

            string apiKey = "1832462d";
            string baseUri = $"http://www.omdbapi.com/?apikey={apiKey}";

            string name = "maniac";
            string type = "series";

            var sb = new StringBuilder(baseUri);
            sb.Append($"&s={name}");
            sb.Append($"&type={type}");

            var request = WebRequest.Create(sb.ToString());
            request.Timeout = 1000;
            request.Method = "GET";
            request.ContentType = "application/json";

            string result = string.Empty;
            ModelObj mv = new ModelObj ();
            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                mv = JsonConvert.DeserializeObject<ModelObj>(result);
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
            return mv;

        }

      
    }
}
