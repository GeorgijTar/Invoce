using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace InvoceDb.Entitys
{
  public class PayDetail: BaseEntity
    {
        public Status Status { get; set; }
        public string Title { get; set; }

        [Required (ErrorMessage ="Поле расчетный счет обязательно для заполнения"), MaxLength(20, ErrorMessage ="Не верный формат расчетного счета")]
        public string PaymentAccount { get; set; }

        public string NameBank { get; set; }

        public string Bik { get; set; }

        public Counterparty Counterparty { get; set; }

    }
}
