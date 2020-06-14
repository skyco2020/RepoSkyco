using DataModal.DataClasses;
using DataModal.GenericRepository;
using DataModal.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Repositories.Repository
{
    public class TokenRepository : SkyCoGenericRepository<Tokens>, ITokenRepository
    {
        public TokenRepository(SkyCoDbContext context) : base(context)
        {
        }
        public override void Delete(Tokens entity, List<string> modifiedfields)
        {
            Tokens token = dbcontext.Tokens.Find(entity.idtoken);
            token.state = entity.state;
            dbcontext.Tokens.Attach(token);
            base.Delete(token, modifiedfields);
        }

        public override void Update(Tokens entity, List<string> modifiedfields)
        {
            Tokens token = dbcontext.Tokens.Find(entity.idtoken);
            token.id = entity.id;
            token.client_ip = entity.client_ip;
            token.created = entity.created;
            token.livemode = entity.livemode;
            token.objectcart = entity.objectcart;
            token.type = entity.type;
            token.used = entity.used;

            dbcontext.Tokens.Attach(token);

            base.Update(token, modifiedfields);
        }
    }
}
