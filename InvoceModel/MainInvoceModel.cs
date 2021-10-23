using InvoceDb;
using InvoceDb.Entitys;
using InvoceModelLib;
using InvoceModelLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InvoceModel.Mapping;

namespace InvoceModel
{
    public class MainInvoceModel : IMainModel
    {

        public IEnumerable<CounterpartyDTO> GetCounterparties()
        {
            Counterparty[] counterparties;
            using (InvoceDbContext db = new InvoceDbContext())
            {
                counterparties = db.Counterparty.ToArray();
            }
            return Array.AsReadOnly(counterparties.Select(Create).ToArray());

            //using InvoceDbContext db = new InvoceDbContext();

            //return Array.AsReadOnly(db.Counterparty.ToArray().Select(Create).ToArray());
        }



        public CounterpartyDTO GetCounterparty(int id)
        {
            Counterparty counterparty;
            using (InvoceDbContext db = new InvoceDbContext())
            {
                counterparty = db.Counterparty.Find(id);
            }
            if (counterparty == null) return null;
            return Create(counterparty);
        }

        public CounterpartyDTO GetCounterparty(InvoceDTO invoce)
        {
            throw new NotImplementedException();
        }

        public InvoceDTO GetInvoce(int id)
        {
            Invoce invoce;
            using (InvoceDbContext db = new InvoceDbContext())
            {
                invoce = db.Invoce.Find(id);
            }
            if (invoce == null) return null;
            return Create(invoce);
        }

        public IEnumerable<InvoceDTO> GetInvoces()
        {
            Invoce [] invoces;
            using (InvoceDbContext db = new InvoceDbContext())
            {
                invoces = db.Invoce.ToArray();
            }
            return Array.AsReadOnly(invoces.Select(Create).ToArray());
        }

        public IEnumerable<PayDetailDTO> GetPayDetails(CounterpartyDTO counterparty)
        {
            PayDetail[] payDetails;
            using (InvoceDbContext db = new InvoceDbContext())
            {
                payDetails = db.PayDetail.Where(d=>d.CounterpartyId==counterparty.Id).ToArray();
            }
            return Array.AsReadOnly(payDetails.Select(Create).ToArray());
        }

        public IEnumerable<PayDetailDTO> GetPayDetails()
        {
            PayDetail[] payDetails;
            using (InvoceDbContext db = new InvoceDbContext())
            {
                payDetails = db.PayDetail.ToArray();
            }
            return Array.AsReadOnly(payDetails.Select(Create).ToArray());
        }

        public StatusDTO GetStatus(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StatusDTO> GetStatuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
