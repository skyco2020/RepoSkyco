using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Mailing
{
    public interface IMail
    {
        void SendMail(IStateMail modelData);
    }
}
