using System.ComponentModel.DataAnnotations;


namespace InvoceModel
{
    /// <summary>
    /// Класс Контрагент
    /// </summary>
    public class Counterparty : BaseModel
    {
        public Status Status { get; set; }
        [Required (ErrorMessage ="Наименование обязательное поле"), MaxLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Платежное наименование обязательное поле"), MaxLength(250, ErrorMessage ="Длинна наименования не должна привышать 250 символов")]
        public string PayName { get; set; }
        [Required(ErrorMessage = "ИНН обязательное поле"), MaxLength(12, ErrorMessage = "Не верный формат ИНН")]
        public string Inn { get; set; }

        [Required(ErrorMessage = "КПП обязательное поле"), MaxLength(9, ErrorMessage = "Не верный формат КПП")]
        public string Kpp { get; set; }

    }
}
