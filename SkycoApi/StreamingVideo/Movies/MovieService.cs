using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StreamingVideo.Movies
{
    public class MovieService
    {
       
        private String GetApiKey()
        {
            string apiKey = " a8cf49cc7c8613eb37be05c36cad7c5d";
            return apiKey;
        }



        //public void CallAPI(string searchText, int page)
        //{
        //    int pageNo = Convert.ToInt32(page) == 0 ? 1 : Convert.ToInt32(page);

        //    /*Calling API https://developers.themoviedb.org/3/search/search-people */
        //    string apiKey = " a8cf49cc7c8613eb37be05c36cad7c5d";
        //    HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/person?api_key=" + apiKey + "&language=en-US&query=" + searchText + "&page=" + pageNo + "&include_adult=false") as HttpWebRequest;

        //    string apiResponse = "";
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
        //                    | SecurityProtocolType.Tls
        //                    | SecurityProtocolType.Tls11
        //                    | SecurityProtocolType.Tls12;
        //    using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
        //    {
        //        StreamReader reader = new StreamReader(response.GetResponseStream());
        //        apiResponse = reader.ReadToEnd();
        //    }
        //    /*End*/

        //    /*http://json2csharp.com*/
        //    ResponseSearchPeople rootObject = JsonConvert.DeserializeObject<ResponseSearchPeople>(apiResponse);

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<div class=\"resultDiv\"><p>Names</p>");
        //    //foreach (Result result in rootObject.results)
        //    //{
        //    //    string image = result.profile_path == null ? Url.Content("~/Content/Image/no-image.png") : "https://image.tmdb.org/t/p/w500/" + result.profile_path;
        //    //    string link = Url.Action("GetPerson", "TmdbApi", new { id = result.id });

        //    //    sb.Append("<div class=\"result\" resourceId=\"" + result.id + "\">" + "<a href=\"" + link + "\"><img src=\"" + image + "\" />" + "<p>" + result.name + "</a></p></div>");
        //    //}

        //    //ViewBag.Result = sb.ToString();

        //    int pageSize = 20;
        //    PagingInfo pagingInfo = new PagingInfo();
        //    pagingInfo.CurrentPage = pageNo;
        //    pagingInfo.TotalItems = rootObject.total_results;
        //    pagingInfo.ItemsPerPage = pageSize;
        //    //ViewBag.Paging = pagingInfo;
        //}
    }
}
