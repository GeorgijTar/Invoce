using InvoceModelLib.Dto;
using InvoceViewLib;
using InvoceViewModelLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoceView
{
    /// <summary>
    /// Логика взаимодействия для CoynterpartyWindowView.xaml
    /// </summary>
    public partial class CoynterpartyWindowView : Window
    {
        
        public CoynterpartyWindowView()
        {
            InitializeComponent();

            var viewModel = (ICoynterpartyViewModel)DataContext;
            StatusIdToNameConverter.Statuses = viewModel.Statuses.Values.ToArray(); 
        }
    }

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

        public IReadOnlyDictionary<int, StatusDTO> Statuses { get; } = new Dictionary<int, StatusDTO>() { {1, new StatusDTO(1, "Черновик") },
            { 2, new StatusDTO(2, "Актуален") } };

        public ObservableCollection<PayDetailDTO> EditingPayDetail { get; } = new ObservableCollection<PayDetailDTO>() { new PayDetailDTO(1, 2, "Название счета", "12345678910123456789", "ОТП Банка", "123456789", new DateTime(2021, 10, 23), 2) };
    }
}
