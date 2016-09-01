using MoneySharp.Internal.Model;

namespace MoneySharp.Internal.Mapping
{
    public class CustomFieldMapper : IMapper<Contract.Model.CustomField, CustomField, CustomField>
    {
        public Contract.Model.CustomField MapToContract(CustomField arg)
        {
            return new Contract.Model.CustomField() {Value = arg.value, Id = arg.id};
        }

        public CustomField MapToApi(Contract.Model.CustomField contract, CustomField apiGet)
        {
            return new CustomField() {id = contract.Id, value = contract.Value};
        }
    }
}
