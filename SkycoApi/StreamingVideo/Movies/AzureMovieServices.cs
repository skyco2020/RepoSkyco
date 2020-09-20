using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingVideo.Movies
{
    public class AzureMovieServices
    {
        String cnn = ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString;
        public List<Movie> GetListMovie()
        {
            try
            {
                String url = GetUriToken("simplemovie");               
                CloudBlobContainer container = new CloudBlobContainer(new Uri(url));
                var prin = container.GetBlockBlobReference("principal");
                List<string> blobs = new List<string>();
                List <IListBlobItem> list = container.ListBlobs().ToList();
                List<Movie> mov = new List<Movie>();
                foreach (IListBlobItem item in list)
                {
                    mov.Add( new Movie() { 
                    urlmovie = item.StorageUri.PrimaryUri.ToString()
                    });
                }
                return mov;
            }
            catch (Exception ex )
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
