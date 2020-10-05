using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using RestSharp;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        String cnn = ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString;
        public  ModelObj GetMovie()
        {

            //var client1 = new RestClient("https://imdb8.p.rapidapi.com/title/auto-complete?q=game%20of%20thr");
            //var request1 = new RestRequest(Method.GET);
            //request1.AddHeader("x-rapidapi-host", "imdb8.p.rapidapi.com");
            //request1.AddHeader("x-rapidapi-key", "1101c7f371msh30f8a15044739c5p1f0a58jsn19d567032559");
            //IRestResponse response1 = client1.Execute(request1);

            var client1 = new RestClient("https://imdb8.p.rapidapi.com/actors/get-all-filmography?nconst=nm0001667");
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("x-rapidapi-host", "imdb8.p.rapidapi.com");
            request1.AddHeader("x-rapidapi-key", "1101c7f371msh30f8a15044739c5p1f0a58jsn19d567032559");
            IRestResponse response1 = client1.Execute(request1);

            var client2 = new RestClient("https://imdb8.p.rapidapi.com/actors/get-all-videos?region=US&nconst=nm0001667");
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("x-rapidapi-host", "imdb8.p.rapidapi.com");
            request2.AddHeader("x-rapidapi-key", "1101c7f371msh30f8a15044739c5p1f0a58jsn19d567032559");
            IRestResponse response2 = client2.Execute(request2);

            var client3 = new RestClient("https://bing-video-search1.p.rapidapi.com/videos/search?q=%3Crequired%3E");
            var request3 = new RestRequest(Method.GET);
            request3.AddHeader("x-rapidapi-host", "bing-video-search1.p.rapidapi.com");
            request3.AddHeader("x-rapidapi-key", "1101c7f371msh30f8a15044739c5p1f0a58jsn19d567032559");
            IRestResponse response3 = client3.Execute(request3);

            var client4 = new RestClient("https://getvideo.p.rapidapi.com/?url=https%253A%252F%252Fwww.youtube.com%252Fwatch%253Fv%253DnfWlot6h_JM");
            var request4 = new RestRequest(Method.GET);
            request4.AddHeader("x-rapidapi-host", "getvideo.p.rapidapi.com");
            request4.AddHeader("x-rapidapi-key", "1101c7f371msh30f8a15044739c5p1f0a58jsn19d567032559");
            IRestResponse response4 = client4.Execute(request4);

            var client5 = new RestClient("https://getvideo.p.rapidapi.com/supported-sites/");
            var request5 = new RestRequest(Method.GET);
            request5.AddHeader("x-rapidapi-host", "getvideo.p.rapidapi.com");
            request5.AddHeader("x-rapidapi-key", "1101c7f371msh30f8a15044739c5p1f0a58jsn19d567032559");
            IRestResponse response5 = client5.Execute(request5);

            string apiKey = "1832462d";
            string baseUri = $"http://www.omdbapi.com/?apikey={apiKey}";

            string name = "maniac";
            string type = "series";

            var sb = new StringBuilder(baseUri);
            sb.Append($"&s={name}");
            sb.Append($"&type={type}");
            //AzureMovieServices nf = new AzureMovieServices();
            //nf.GetMovie();
            var request = WebRequest.Create(sb.ToString());
            //request.Timeout = 1000;
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

        public List<Movie> GetListMovie()
        {
            try
            {
                String url = GetUriToken("simplemovie");
                CloudBlobContainer container = new CloudBlobContainer(new Uri(url));
                //var prin = container.GetBlockBlobReference("principal");
                List<string> blobs = new List<string>();
                List<IListBlobItem> list = container.ListBlobs().ToList();
                List<Movie> mov = new List<Movie>();
                foreach (IListBlobItem item in list)
                {
                    mov.Add(new Movie()
                    {
                        urlmovie = item.StorageUri.PrimaryUri.ToString()
                    });
                }
                return mov;
            }
            catch (Exception ex)
            {
                return new List<Movie>();
            }
        }

        private CloudBlobContainer GetContainer(String containername)
        {
            //Cadena de la conexion del repositorio
            CloudStorageAccount account = CloudStorageAccount.Parse(cnn);
            CloudBlobClient client = account.CreateCloudBlobClient();

            //NOTA: los nombres de los CONTENEDORES siempre tienen que ser en MINUSCULAS.
            //Lectura del container
            CloudBlobContainer container = client.GetContainerReference(containername);

            return container;
        }

        private String GetUriToken(String containername)
        {
            // acesso al repositorio
            CloudBlobContainer container = GetContainer(containername);
            SharedAccessBlobPolicy permisosSas = new SharedAccessBlobPolicy();
            permisosSas.SharedAccessExpiryTime = DateTime.Now.AddMinutes(10);
            //NOTA: El simbolo "|" realiza una suma sustractiva, es decir, permiso 1 + permiso 2, etc
            permisosSas.Permissions = SharedAccessBlobPermissions.List
                | SharedAccessBlobPermissions.Read
                | SharedAccessBlobPermissions.Write;
            //| SharedAccessBlobPermissions.Delete; //NOTA: Si se quita un PERMISO SAS, la APLICACION CLIENTE NO tendrá acceso a realizar la accion que representa.
            String token = container.GetSharedAccessSignature(permisosSas);
            //PARA DAR ACCESO, EL TOKEN VA DENTRO DEL URI
            //DEL CONTENEDOR
            //NOTA: lo suyo es devolver la URI (con TOKEN) como un OBJETO, es decir, el TOKEN como una propiedad, ahora mismo esta devuelto como un STRING.
            return container.Uri + token;
        }
    }
}
