using Newtonsoft.Json;
using SkyCoApi.Models.Hypermedia.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Representations
{
    public abstract class BaseRepresentation : Representation
    {
        public abstract Int64 IDRepresentation();
        [JsonIgnore]
        public BaseTemplate _mytemplate;
        [JsonIgnore]
        public abstract BaseTemplate Mytemplate { get; set; }

        public virtual void CreatesMySelfLinks()
        {
            Link mylink = Mytemplate.GetMyOwnReference(IDRepresentation());
            Links.Add(mylink);
        }

        public virtual void CreateUpdateLink()
        {
            Link mylink = Mytemplate.GetMyUpdateLink(IDRepresentation());
            Links.Add(mylink);
        }

        public virtual void CreateDeleteLink()
        {
            Link mylink = Mytemplate.GetMyDeleteLink(IDRepresentation());
            Links.Add(mylink);
        }
    }
}