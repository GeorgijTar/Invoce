using InvoceModelLib;
using Simplified;

namespace InvoceViewModel
{
    class MainViewModel : ViewModelBase
    {
        // public InvoceWieMOde {get;}
        // public PayViewMOdel {get;}

        private readonly IInvoceModel model;

        public MainViewModel(IInvoceModel model)
        {
            this.model = model;
        }
    }



}
