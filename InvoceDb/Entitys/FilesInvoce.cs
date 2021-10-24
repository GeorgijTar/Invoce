using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoceDb.Entitys
{
    class FilesInvoce: BaseEntity
    {
        public Invoce Invoce { get; set; }
        public int InvoceId { get; set; }
        public string NameFile { get; set; }
        public byte[] BodyFile { get; set; }
        public DateTime TimeSpan { get; set; }
    }
}
