using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    public class UserDTO : BaseEntityDTO
    {
        public StatusDTO Status { get; }
        public string Login { get; }
        public string Password { get; }
        public DateTime TimeStamp { get; }
    }
}
