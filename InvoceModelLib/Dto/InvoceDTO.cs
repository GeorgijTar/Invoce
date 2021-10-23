using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    public class InvoceDTO : BaseEntityDTO
    {
        public InvoceDTO(int id,
                         int statusId,
                         string number,
                         DateTime dateInvoce,
                         int regNumber,
                         DateTime regDateInvoce,
                         int counterpartyId,
                         decimal amount,
                         int ndsPercent,
                         decimal ndsAmount,
                         string description,                         
                         DateTime timeStamp): base (id)
        {

            StatusId = statusId;
            Number = number;
            DateInvoce = dateInvoce;
            RegNumber = regNumber;
            RegDateInvoce = regDateInvoce;
            CounterpartyId = counterpartyId;
            Amount = amount;
            NdsPercent = ndsPercent;
            NdsAmount = ndsAmount;
            Description = description;
            TimeStamp = timeStamp;
        }

        public int StatusId { get; }      
        public string Number { get; }      
        public DateTime DateInvoce { get; }
        public int RegNumber { get; }
        public DateTime RegDateInvoce { get; }        
        public int CounterpartyId { get; }      
        public decimal Amount { get; }
        public int NdsPercent { get; set; }
        public decimal NdsAmount { get; }       
        public string Description { get; }
        public DateTime TimeStamp { get; }

    }
}
