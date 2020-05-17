using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface ISkyco_AccountServices
    {
        Skyco_AccountBE GetLogin(String username, String userpass );
        Skyco_AccountBE GetById(Int32 Id);
        Boolean Update(Skyco_AccountBE Be);
        Boolean ModifyPassword(String userpass, Int64 iduser);
    }
}
