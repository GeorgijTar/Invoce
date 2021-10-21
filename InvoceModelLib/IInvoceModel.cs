using InvoceModelLib.Dto;
using System.Collections.Generic;

namespace InvoceModelLib
{
    public interface IInvoceModel
    {
        IEnumerable<CounterpartyDto> GetCounterparties();
        IEnumerable<PayDetailDto> GetPayDetails(CounterpartyDto counterparty);
        IEnumerable<PayDetailDto> GetPayDetails();
    }
}
