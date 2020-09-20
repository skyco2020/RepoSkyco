using BusinessEntities.BE;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IMovieServices
    {
        SearchBE GetById(String Id);
        List<SearchBE> GetAll(Int32 state);
        List<MovieBE> GetListMovie();
    }
}
