using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IPerfilServices
    {
        PerfilBE GetById(Int64 Id);
        List<PerfilBE> GetAll(Int32 state, Int32 page, Int32 pageSize, String orderBy, String ascending, Int64 AccountId, ref Int32 count);
        Int64 Create(PerfilBE Be);
        Boolean Update(PerfilBE Be);
        Boolean Delete(Int64 Id);
    }
}
