namespace MoneySharp.Internal.Mapping
{
    public interface IMapper<TContract, in TApiGet, out TApiPost>
    {
        TContract MapToContract(TApiGet apiGet);
        TApiPost MapToApi(TContract contract, TApiGet apiGet);
    }
}