using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    /// <summary>
    /// Класс Контрагент
    /// </summary>
    public class CounterpartyDto 
    {
        public string Status { get; }
        public string Name { get;  }
        public string PayName { get;  }
        public string Inn { get;  }
        public string Kpp { get; }
        public DateTime TimeStamp { get; }

        public CounterpartyDto(string status, string name, string payName, string inn, string kpp, DateTime timeStamp)
        {
            Status = status;
            Name = name;
            PayName = payName;
            Inn = inn;
            Kpp = kpp;
            TimeStamp = timeStamp;
        }
    }
}
