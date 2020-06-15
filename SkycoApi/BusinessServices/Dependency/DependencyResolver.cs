﻿using BusinessServices.Interfaces;
using BusinessServices.OwinSecurity;
using BusinessServices.Services;
using Resolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Dependency
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<ICardServices, CardServices>();
            registerComponent.RegisterType<ICityServices, CityServices>();
            registerComponent.RegisterType<ICountryServices, CountryServices>();
            registerComponent.RegisterType<ILocationServices, LocationServices>();
            registerComponent.RegisterType<IPaymentServices, PaymentServices>();
            registerComponent.RegisterType<IPlanServices, PlanServices>();
            registerComponent.RegisterType<IProvinceServices, ProvinceServices>();
            registerComponent.RegisterType<ISkyco_AccountServices, Skyco_AccountServices>();
            registerComponent.RegisterType<ISkyco_AccountTypeServices, Skyco_AccountTypeServices>();
            registerComponent.RegisterType<ISkyco_AddressServices, Skyco_AddressServices>();
            registerComponent.RegisterType<ISkyco_PhoneServices, Skyco_PhoneServices>();
            registerComponent.RegisterType<ISkyco_UsersServices, Skyco_UsersServices>();
            registerComponent.RegisterType<ITokenServices, TokenServices>();
            registerComponent.RegisterType<IOwinSecurityService, OwinSecurityServer>();
        }
    }
}
