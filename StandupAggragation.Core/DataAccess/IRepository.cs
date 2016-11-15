using System.Collections.Generic;

namespace StandupAggragation.Core.DataAccess
{
    public interface IRepository<TObject, TCollection> where TCollection : ICollection<TObject>
    {
        RepositoryResult<TCollection> GetItems(object filter);
        RepositoryResult<TObject> GetItem(object filter);
    }
}