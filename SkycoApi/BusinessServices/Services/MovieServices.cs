using BusinessEntities.BE;
using BusinessServices.Interfaces;
using StreamingVideo.Class;
using StreamingVideo.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class MovieServices: IMovieServices
    {
        #region Single
        private readonly MovieService _unitOfWork;

        public MovieServices(MovieService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public List<SearchBE> GetAll(int state)
        {
            ModelObj entities = _unitOfWork.GetMovie();

            List<SearchBE> listbe = new List<SearchBE>();
            if(entities != null)
                if(entities.Search != null)
                    foreach (Search item in entities.Search)
                    {
                        listbe.Add(Patterns.Factories.FactoryMovies.GetInstance().CreateBusiness(item));
                    }
            return listbe;
        }

        public SearchBE GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<MovieBE> GetListMovie()
        {
            List<Movie> entities = _unitOfWork.GetListMovie();

            List<MovieBE> listbe = new List<MovieBE>();
            if (entities.Count > 0)
                foreach (Movie item in entities)
                {
                    listbe.Add(Patterns.Factories.FactoryFilms.GetInstance().CreateBusiness(item));
                }
            return listbe;
        }
    }
}
