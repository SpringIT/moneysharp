using System.Collections.Generic;
using MoneySharp.Internal.Model;

namespace MoneySharp.Internal
{
    public interface IContactConnector<TGetObject, in TPostObject> : IDefaultConnector<TGetObject, TPostObject>
        where TGetObject : class, new() where TPostObject : class, new()
    {
        IList<Contact> GetBySearch(string search);
    }
}