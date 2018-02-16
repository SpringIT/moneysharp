using System.Collections.Generic;
using MoneySharp.Internal.Model;

namespace MoneySharp.Internal
{
    public interface IDefaultConnector<TGetObject, in TPostObject> where TGetObject : class, new() where TPostObject : class, new()
    {
        IList<TGetObject> GetList();
        TGetObject GetById(long id);
        TGetObject Create(TPostObject data);
        TGetObject Update(long id, TPostObject data);
        void Delete(long id);
    }
}