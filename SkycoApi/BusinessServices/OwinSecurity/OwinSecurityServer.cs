using DataModal.DataClasses;
using DataModal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.OwinSecurity
{
    public class OwinSecurityServer: IOwinSecurityService
    {
        private readonly UnitOfWork _unitOfWork;

        public OwinSecurityServer(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SkyCoDbContext Create()
        {
            return _unitOfWork.GetNewContext();
        }
    }
}
