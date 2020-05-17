using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IProvinceServices
    {
        ProvinceBE GetById(Int64 Id);
        List<ProvinceBE> GetAll(Int32 state, Int32 page, Int32 pageSize, String orderBy, String ascending, ref Int32 count, Int64 idcountry);
        Int64 Create(ProvinceBE Be);
        Boolean Update(ProvinceBE Be);
        Boolean Delete(Int64 Id);
    }
}
