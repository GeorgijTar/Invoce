
using InvoceModelLib.Dto;
using Simplified;
using System.Collections.ObjectModel;

namespace InvoceViewModelLib
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
