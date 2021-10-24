using InvoceDb.Entitys;
using InvoceModelLib.Dto;

namespace InvoceModel
{
    public static class Mapping
    {
        public static CounterpartyDTO Create(Counterparty counterparty)
            => new CounterpartyDTO(counterparty.Id,
                               counterparty.Status.Id,
                               counterparty.Name,
                               counterparty.PayName,
                               counterparty.INN,
                               counterparty.KPP,
                               counterparty.OGRN,
                               counterparty.OKPO,
                               counterparty.PhoneNumber,
                               counterparty.Email,
                               counterparty.RegistrationDate,
                               counterparty.Address,
                               counterparty.TimeSpan);

        public static InvoceDTO Create(Invoce invoce)
            => new InvoceDTO(invoce.Id,
                         invoce.Status.Id,
                         invoce.Number,
                         invoce.DateInvoce,
                         invoce.RegNumber,
                         invoce.RegDateInvoce,
                         invoce.Counterparty.Id,
                         invoce.Amount,
                         invoce.NdsPercent,
                         invoce.NdsAmount,
                         invoce.Description,
                         invoce.TimeStamp);

        public static PayDetailDTO Create(PayDetail payDetail)
           => new PayDetailDTO(payDetail.Id, 
               payDetail.Status.Id, 
               payDetail.Title, 
               payDetail.PaymentAccount,
               payDetail.NameBank,
               payDetail.Bik,
               payDetail.CorrAccount,
               payDetail.TimeStamp,
               payDetail.CounterpartyId);


        public static StatusDTO Create(Status status)
          => new StatusDTO(status.Id,status.Name);


    }
}
