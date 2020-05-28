﻿using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface ISkyco_UsersServices
    {
        Skyco_UserBE GetById(Int64 Id);
        List<Skyco_UserBE> GetAll(Int32 state, Int32 page, Int32 pageSize, String orderBy, String ascending, ref Int32 count);
        Skyco_UserBE GetByE_mail(String email);
        Int64 Create(Skyco_UserBE Be);
        Boolean Update(Skyco_UserBE Be);
        Boolean Delete(Int64 Id, String UserName);
    }
}
