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
        public CounterpartyDTO(int id,
                               int statusId,
                               string name,
                               string payName,
                               string iNN,
                               string kPP,
                               string oGRN,
                               string oKPO,
                               string phoneNumber,
                               string email,
                               DateTime registrationDate,
                               string address,
                               DateTime timeSpan) : base(id)
        {
            StatusId = statusId;
            Name = name;
            PayName = payName;
            INN = iNN;
            KPP = kPP;
            OGRN = oGRN;
            OKPO = oKPO;
            PhoneNumber = phoneNumber;
            Email = email;
            RegistrationDate = registrationDate;
            Address = address;
            TimeSpan = timeSpan;
        }

        public int StatusId { get; }
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
        public DateTime TimeSpan { get; }
    }
}
