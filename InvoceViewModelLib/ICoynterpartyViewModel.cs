using InvoceModelLib.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InvoceViewModelLib
{
    public interface ICoynterpartyViewModel
    {
        string Title { get; }

        public int StatusId { get; set; }
        public string Name { get; set; }
        public string PayName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string OGRN { get; set; }
        public string OKPO { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public DateTime TimeSpan { get; set; }

        public CounterpartyDTO EditingCounterparty { get; }

        public IEnumerable<StatusDTO> Statuses { get; }

        ObservableCollection<PayDetailDTO> EditingPayDetail { get; }
    }

}
