using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IRegisterUserServices
    {
        Skyco_UserBE GetById(Int64 Id);
        List<Skyco_UserBE> GetAll(Int32 state, Int32 page, Int32 pageSize, String orderBy, String ascending, ref Int32 count);
        Int64 Create(Skyco_UserBE Be);
        Boolean Update(Skyco_UserBE Be);
        Boolean Delete(Int64 Id, String UserName);
    }
}
