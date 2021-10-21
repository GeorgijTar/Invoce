using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    /// <summary>
    /// Класс Платежных реквизитов Контрагентов
    /// </summary>
    public class PayDetailDto
    {
        public PayDetailDto(string status, string title, string paymentAccount, string nameBank, string bik, DateTime timeStamp)
        {
            Status = status;
            Title = title;
            PaymentAccount = paymentAccount;
            NameBank = nameBank;
            Bik = bik;
            TimeStamp = timeStamp;
        }

        public string Status { get; }
        public string Title { get; }

        public string PaymentAccount { get;}

        public string NameBank { get; }

        public string Bik { get; }

        public DateTime TimeStamp { get; }

    }
}
