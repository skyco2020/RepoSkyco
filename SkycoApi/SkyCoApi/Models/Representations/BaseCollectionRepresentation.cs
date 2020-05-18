using Newtonsoft.Json;
using SkyCoApi.Models.Hypermedia.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Representations
{
    public abstract class BaseCollectionRepresentation<T> : SimpleListRepresentation<T> where T : BaseRepresentation
    {
        public int Pagenumber = 0;
        public int Count = 0;
        public int Top = 0;
        public string Filters;
        [JsonIgnore]
        public BaseTemplate _mytemplate;
        [JsonIgnore]
        public abstract BaseTemplate Mytemplate { get; set; }

        public BaseCollectionRepresentation(IList<T> list) : base(list)
        {
        }

        protected override void CreateHypermedia()
        {
            var link = RegisterMyPage();
            Links.Add(new Link { Href = link.Href, Rel = "self" });

            if (Pagenumber > 0)
            {
                decimal lastpage = Math.Ceiling(Decimal.Parse(Count.ToString()) / Decimal.Parse(Top.ToString()));
                if (Pagenumber < lastpage)
                {
                    Links.Add(new Link { Href = link.Href + Filters + "&page=" + (Pagenumber + 1), Rel = "next" });
                    Links.Add(new Link { Href = link.Href + Filters + "&page=" + lastpage, Rel = "last" });
                }
                if (Pagenumber > 1)
                {
                    Links.Add(new Link { Href = link.Href + Filters + "&page=1", Rel = "first" });
                    Links.Add(new Link { Href = link.Href + Filters + "&page=" + (Pagenumber - 1), Rel = "prev" });
                }
            }
            Links.Add(new Link { Href = link.Href + Filters, Rel = "find" });

            this.RegisterMyPage();
        }
        [JsonIgnore]
        public Link MyOwnPage { get; set; }
        public virtual Link RegisterMyPage()
        {
            if (MyOwnPage != null)
                return MyOwnPage;
            return Mytemplate.GetMyCollectionReference().CreateLink();
        }
        public BaseCollectionRepresentation(IList<T> list, string filters, int pagenumber, int count, int top) : base(list)
        {
            //this.CreateOwnerItems(list);
            Filters = filters;
            Pagenumber = pagenumber;
            Count = count;
            Top = top;
            //this.CreateHypermedia();
        }
    }
}