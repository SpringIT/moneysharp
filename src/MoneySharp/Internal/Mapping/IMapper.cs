namespace MoneySharp.Internal.Mapping
{
    public interface IMapper<TContract, in TApiGet, out TApiPost>
    {
        TContract MapToContract(TApiGet input);
        TApiPost MapToApi(TContract data, TApiGet current);
    }
}