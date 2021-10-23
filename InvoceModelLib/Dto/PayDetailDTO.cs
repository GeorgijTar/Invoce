using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    /// <summary>
    /// Класс Платежных реквизитов Контрагентов
    /// </summary>
    public class PayDetailDTO: BaseEntityDTO
    {
        public StatusDTO Status { get; set; }
        public string Title { get; set; }

        [Required (ErrorMessage ="Поле расчетный счет обязательно для заполнения"), MaxLength(20, ErrorMessage ="Не верный формат расчетного счета")]
        public string PaymentAccount { get; set; }

        public string NameBank { get; set; }

        public string Bik { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

    }
}
