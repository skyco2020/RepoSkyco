using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IPlanServices
    {
        PlanBE GetById(Int64 Id);
        List<PlanBE> GetAll(Int32 state, Int32 page, Int32 top, String orderBy, String ascending, ref Int32 count);
        Int64 Create(PlanBE Be);
        Boolean Update(PlanBE Be);
        Boolean Delete(Int64 Id, String UserName);
    }
}
