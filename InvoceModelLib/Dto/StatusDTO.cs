using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{/// <summary>
 /// Слас реализации модели Статусов
 /// </summary>
    public class StatusDTO: BaseEntityDTO
    {
        [Required (ErrorMessage ="Наименование татуса обязательно для заполнения")]
        public string Name { get; set; }
    }
}
