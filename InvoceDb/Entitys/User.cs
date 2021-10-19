using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceDb.Entitys
{
    public class User : BaseEntity
    {
        [Required]
        public Status Status { get; set; }

        [Required (ErrorMessage ="Поле логин обязательно для заполнения")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле пароль обязательно для заполнения")]
        public string Password { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
