using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface ICardServices
    {
        CardBE GetById(Int64 Id);
        List<CardBE> GetAll(Int32 state, Int32 page, Int32 top, String orderBy, String ascending, ref Int32 count);
        Int64 Create(CardBE Be);
        Boolean Update(CardBE Be);
        Boolean Delete(Int64 Id, String UserName);
    }
}
