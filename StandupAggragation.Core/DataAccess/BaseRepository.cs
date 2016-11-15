using System.Collections.Generic;

namespace StandupAggragation.Core.DataAccess
{
    public class BaseRepository<TObject> : IRepository<TObject, IList<TObject>>
    {
        private readonly IContext _context;

        public BaseRepository(IContext context)
        {
            _context = context;
        }

        public RepositoryResult<IList<TObject>> GetItems(object filter)
        {
            return GetFromContext<IList<TObject>>("GetList", filter);
        }

        public RepositoryResult<TObject> GetItem(object filter)
        {
            return GetFromContext<TObject>("Get", filter);
        }

        private RepositoryResult<T> GetFromContext<T>(string command, object filter)
        {
            using (_context)
            {
                var rawResult = _context.Fetch(command, filter);
                var result = new RepositoryResult<T>();
                if (rawResult == null)
                {
                    result.Succeed = false;
                    result.Result = default(T);
                    result.Id = null;
                }
                else
                {
                    result.Succeed = true;
                    result.Result = (T) rawResult;
                    //result.Id 
                    //TODO: Implement result.ID in Context level
                }
                return result;
            }
        }
    }
}