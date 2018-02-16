using System.Collections.Generic;
using MoneySharp.Internal.Model;

namespace MoneySharp.Internal
{
    public interface IRecurringSalesInvoiceConnector<TGetObject, in TPostObject> : IDefaultConnector<TGetObject, TPostObject>
        where TGetObject : class, new() where TPostObject : class, new()
    {
        IList<RecurringSalesInvoiceGet> GetByContactId(long id);
    }
}