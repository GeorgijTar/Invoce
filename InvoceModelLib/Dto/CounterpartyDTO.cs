using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    /// <summary>
    /// Класс Контрагент
    /// </summary>
    public class CounterpartyDTO : BaseEntityDTO
    {
        public virtual StatusDTO Status { get; }
        public string Name { get; }
        public string PayName { get; }
        public string INN { get; }
        public string KPP { get; }
        public string OGRN { get; }
        public string OKPO { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public DateTime RegistrationDate { get; }
        public string Address { get; }
        public DateTime TimeSpan { get;  }

        public List<PayDetailDTO> PayDetail { get;  }

    }
}
