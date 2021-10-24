using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    /// <summary>
    /// Класс Платежных реквизитов Контрагентов
    /// </summary>
    public class PayDetailDTO : BaseEntityDTO
    {
        public PayDetailDTO(int id,
                            int statusId,
                            string title,
                            string paymentAccount,
                            string nameBank,
                            string bik,
                            string corrAccount,
        DateTime timeStamp,
                            int counterpartyId) : base(id)
        {
            StatusId = statusId;
            Title = title;
            PaymentAccount = paymentAccount;
            NameBank = nameBank;
            Bik = bik;
            CorrAccount = corrAccount;
            TimeStamp = timeStamp;
            CounterpartyId = counterpartyId;
        }

        public int StatusId { get; }
        public string Title { get; }
        public string PaymentAccount { get; }
        public string NameBank { get; }
        public string Bik { get; }

        public string CorrAccount { get; }
        public int CounterpartyId { get; }
        public DateTime TimeStamp { get; }

    }
}
