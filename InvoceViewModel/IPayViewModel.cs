using InvoceDb.Entitys;
using Simplified;
using System.Collections.ObjectModel;

namespace InvoceViewModel
{
    public interface IPayViewModel
    {
        ObservableCollection<PayDetailDTO> PayDetails { get; }
        PayDetailDTO SelectPayDetailDTO { get; }
        RelayCommand<PayDetailDTO> AddPayDetailsCommand { get; }
        RelayCommand<PayDetailDTO> EdetePayDetailsCommand { get; }
        RelayCommand<PayDetailDTO> DeletePayDetailsCommand { get; }
    }



}
