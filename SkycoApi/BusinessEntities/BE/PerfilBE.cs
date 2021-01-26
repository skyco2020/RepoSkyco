using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PerfilBE
    {
        public Int64 idPerfil { get; set; }
        public Int64 AccountId { get; set; }
        public String name { get; set; }
        public String passperfil { get; set; }
        public Boolean complete { get; set; }
        public Int32 typeperfil { get; set; }
        public Int32 state { get; set; }

        #region Relation
        public Skyco_AccountBE Account { get; set; }
        #endregion
    }
}
