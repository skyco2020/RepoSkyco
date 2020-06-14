using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface ITokenServices
    {
        TokenBE GetById(Int64 Id);
        List<TokenBE> GetAll(Int32 state, Int32 page, Int32 top, String orderBy, String ascending, ref Int32 count);
        Int64 Create(TokenBE Be);
        Boolean Update(TokenBE Be);
        Boolean Delete(Int64 Id, String UserName);
    }
}
