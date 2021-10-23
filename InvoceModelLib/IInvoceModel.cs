using InvoceModelLib.Dto;
using System.Collections.Generic;

namespace InvoceModelLib
{
    public interface IInvoceModel
    {
        IEnumerable<InvoceDTO> GetInvoces();
        InvoceDTO GetInvoce(int Id);

        IEnumerable<CounterpartyDTO> GetCounterparties();
        CounterpartyDTO GetCounterparty(InvoceDTO invoce);

        IEnumerable<PayDetailDTO> GetPayDetails(CounterpartyDTO counterparty);
        IEnumerable<PayDetailDTO> GetPayDetails();
        IEnumerable<StatusDTO> GetStatuses();
        StatusDTO GetStatus(int Id);
        IEnumerable<UserDTO> GetUsers();




    }
}
