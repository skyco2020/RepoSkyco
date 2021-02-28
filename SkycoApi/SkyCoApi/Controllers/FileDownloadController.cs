using Newtonsoft.Json;
using Resolver.Cryptography;
using SkyCoApi.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SkyCoApi.Controllers
{
    public class FileDownloadController : ApiController
    {
        [AllowAnonymous]
        //[MimeMultipartFilter]
        [Route("~/api/MyFiles/PublicDocument")]
        [HttpPost]
        public async Task<string> UploadPublicDocument(HttpPostedFileBase file)
        {
            //HttpFileCollection filecol = HttpContext.Current.Request.Files;
            //DocumentValidation.GetInstance().CompleteValidations(filecol);

            string uploadPath = DocumentHandling.GetInstance().HandlingUpdatePublicDirectories();
            var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);
            await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

            string _localFileName = multipartFormDataStreamProvider
                .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

            return JsonConvert.SerializeObject(new FileResult
            {
                FileName = Path.GetFileName(_localFileName),
                FileLength = new FileInfo(_localFileName).Length,
                LocalFilePath = MD5Base.GetInstance().Encypt(_localFileName),

            });
        }

    }
}
