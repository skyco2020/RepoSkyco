using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Enumerations
{
    public class RolGeneric
    {
        #region Constructor
        private static RolGeneric _factory;
        public static RolGeneric GetInstance()
        {
            if (_factory == null)
                _factory = new RolGeneric();
            return _factory;
        }
        #endregion

        public String GetRolString(Int32 id)
        {
            switch (id)
            {
                case 1:
                    return Rol.Full_Administrator.ToString();
                case 2:
                    return Rol.Administrator.ToString();
                case 3:
                    return Rol.Employee.ToString();
                default:
                    return Rol.User.ToString();
            }
        }

        public Int32 getRolId(String Rol)
        {
            switch (Rol)
            {
                case "Full_Administrator":
                    return (Int32)(Resolver.Enumerations.Rol.Full_Administrator);
                case "Administrator":
                    return (Int32)(Resolver.Enumerations.Rol.Administrator);
                case "Employee":
                    return (Int32)(Resolver.Enumerations.Rol.Employee);
                default:
                    return (Int32)(Resolver.Enumerations.Rol.User);
            }
        }       
    }
}
