using InvoceModelLib.Dto;
using InvoceViewModelLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InvoceView
{
    public class CoynterpartyViewModelDT : ICoynterpartyViewModel
    {
        public string Title { get; } = "Контрагкент";
        public int StatusId { get; set; } = 1;
        public string Name { get; set; } = "ООО Тестовый контрагент";
        public string PayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string INN { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string KPP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OGRN { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OKPO { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PhoneNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime RegistrationDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime TimeSpan { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CounterpartyDTO EditingCounterparty => throw new NotImplementedException();

        public IEnumerable<StatusDTO> Statuses { get; } = new StatusDTO[] { new StatusDTO(1, "Черновик"), new StatusDTO(2, "Актуален")};

        public ObservableCollection<PayDetailDTO> EditingPayDetail { get; } = new ObservableCollection<PayDetailDTO>() { new PayDetailDTO(1, 2, "Название счета", "12345678910123456789", "ОТП Банка", "123456789", new DateTime(2021, 10, 23), 2) };
    }
}
