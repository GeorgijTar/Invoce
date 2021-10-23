using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{
    public class UserDTO : BaseEntityDTO
    {
        public UserDTO(int id, StatusDTO status, string login, string password, DateTime timeStamp):base(id)
        {
            Status = status;
            Login = login;
            Password = password;
            TimeStamp = timeStamp;
        }

        public StatusDTO Status { get; }
        public string Login { get; }
        public string Password { get; }
        public DateTime TimeStamp { get; }
    }
}
