using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    public class InvoceDTO : BaseEntityDTO
    {
        [Required]
        public StatusDTO Status { get; set; }
        [Required(ErrorMessage ="Поле Номер является обязательным")]
        public string Number { get; set; }
        [Required]
        public DateTime DateInvoce { get; set; }

        public int RegNumber { get; set; }

        public DateTime RegDateInvoce { get; set; }
        [Required(ErrorMessage = "Поле Контрагент является обязательным")]
        public CounterpartyDTO Counterparty { get; set; }
        [Required(ErrorMessage = "Поле Сумма является обязательным")]
        public decimal Amount { get; set; }

        public int NdsPercent { get; set; }

        public decimal NdsAmount { get; set; }
        [Required(ErrorMessage = "Поле Описание является обязательным")]
        public string Description { get; set; }

        public byte[] File { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

    }
}
