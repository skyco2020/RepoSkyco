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
    public class PerfilRepository : SkyCoGenericRepository<Perfils>, IPerfilRepository
    {
        public PerfilRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Perfils entity, List<string> modifiedfields)
        {
            Perfils perf = dbcontext.Perfils.Find(entity.idPerfil);
            perf.state = entity.state;
            dbcontext.Perfils.Attach(perf);
            base.Delete(perf, modifiedfields);
        }

        public override void Update(Perfils entity, List<string> modifiedfields)
        {
            Perfils perf = dbcontext.Perfils.Find(entity.idPerfil);
            perf.name = entity.name;
            perf.passperfil = entity.passperfil;
            perf.complete = entity.complete;
            dbcontext.Perfils.Attach(perf);
            base.Update(perf, modifiedfields);
        }
    }
}
