﻿using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class TokenDTO : BaseRepresentation
    {
        #region Constructor
        public TokenDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion
        public Int64 idtoken { get; set; }
        public Int64 id { get; set; }
        public Int64 client_ip { get; set; }
        public Int64 created { get; set; }
        public Int64 livemode { get; set; }
        public Int64 objectcart { get; set; }
        public Int64 type { get; set; }
        public Int64 used { get; set; }
        public Int32 state { get; set; }

        #region List
        public List<CardDTO> cards { get; set; }
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = TokenTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return idtoken;
        }

        protected override void CreateHypermedia()
        {
            Href = TokenTemplate.Token.CreateLink(new { id = idtoken }).Href;
        }
        #endregion
    }
}