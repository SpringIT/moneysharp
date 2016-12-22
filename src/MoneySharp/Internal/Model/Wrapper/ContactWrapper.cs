namespace MoneySharp.Internal.Model.Wrapper
{
    public class ContactWrapper
    {
        public ContactWrapper()
        {
            
        }

        public ContactWrapper(Contact contact)
        {
            this.contact = contact;
        }

        public Contact contact { get; set; }
    }
}
