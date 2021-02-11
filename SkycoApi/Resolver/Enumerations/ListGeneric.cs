using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Enumerations
{
    public class ListGeneric
    {
        #region Constructor
        private static ListGeneric _factory;
        public static ListGeneric GetInstance()
        {
            if (_factory == null)
                _factory = new ListGeneric();
            return _factory;
        }
        #endregion
        
        public String GetGenderString(Int32 id)
        {
            switch (id)
            {
                case 1:
                    return Gender.Male.ToString();
                case 2:
                    return Gender.Female.ToString();
                default:
                    return Gender.Other.ToString();
            }
        }

        public Int32 getGender(String gender)
        {
            switch (gender)
            {
                case "Gender":
                    return (Int32)(Resolver.Enumerations.Gender.Male);
                case "Female":
                    return (Int32)(Resolver.Enumerations.Gender.Female);
                default:
                    return (Int32)(Resolver.Enumerations.Gender.Other);
            }
        }

        public Int32 GetScreen(String screen)
        {
            switch (screen.ToLower())
            {
                case "Plan one Screen":
                    return 1;
                case "plan two screens":
                    return 2;
                case "plan four screens":
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
