using System;
using System.ComponentModel.DataAnnotations;


namespace InvoceModel
{
    public class User : BaseModel
    {
        [Required (ErrorMessage ="Поле логин обязательно для заполнения")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле пароль обязательно для заполнения")]
        public string Password { get; set; }

        public DateTime DateCreation { get; set; }
    }
}
