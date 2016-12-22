using MoneySharp.Internal.Model;

namespace MoneySharp.Internal.Mapping
{
    public class CustomFieldMapper : IMapper<Contract.Model.CustomField, CustomField, CustomField>
    {
        public Contract.Model.CustomField MapToContract(CustomField input)
        {
            return new Contract.Model.CustomField() {Value = input.value, Id = input.id};
        }

        public CustomField MapToApi(Contract.Model.CustomField data, CustomField apiGet)
        {
            return new CustomField() {id = data.Id, value = data.Value};
        }
    }
}
