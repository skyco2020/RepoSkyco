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

             String URL = "https://pixabay.com/api/videos/?key=19225679-589718ac01031a964104548e7";

            string apiKey = "1832462d";
            string baseUri = $"http://www.omdbapi.com/?apikey={apiKey}";

            string name = "maniac";
            string type = "series";

            var sb = new StringBuilder(baseUri);
            sb.Append($"&s={name}");
            sb.Append($"&type={type}");
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

        public Movie GetAllMovie()
        {           

            String URL = "https://pixabay.com/api/videos/?key=19225679-589718ac01031a964104548e7";

            string apiKey = "1832462d";
            string baseUri = $"http://www.omdbapi.com/?apikey={apiKey}";

            string name = "maniac";
            string type = "series";

            var sb = new StringBuilder(URL);
            var request = WebRequest.Create(sb.ToString());
            request.Method = "GET";
            request.ContentType = "application/json";

            string result = string.Empty;
            Movie mv = new Movie();
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
                mv = JsonConvert.DeserializeObject<Movie>(result);
                return mv;
            }
            catch (WebException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

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
