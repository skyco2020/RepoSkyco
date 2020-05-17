using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface ICityServices
    {
        CityBE GetById(Int64 Id);
        List<CityBE> GetAll(Int32 state, Int32 page, Int32 pageSize, String orderBy, String ascending, ref Int32 count);
        Int64 Create(CityBE Be);
        Boolean Update(Int64 Id, CityBE Be);
        Boolean Delete(Int64 Id);
    }
}
