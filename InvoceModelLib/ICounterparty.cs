using InvoceModelLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoceModelLib
{
   public interface ICounterparty
    {
        IEnumerable<CounterpartyDTO> GetCounterparties();
        CounterpartyDTO GetCounterparty(int Id);
        IEnumerable<StatusDTO> GetStatuses();
        StatusDTO GetStatus(int Id);




    }
}
