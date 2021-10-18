using System.ComponentModel.DataAnnotations;


namespace InvoceModel
{/// <summary>
 /// Слас реализации модели Статусов
 /// </summary>
    public class Status: BaseModel
    {
        [Required (ErrorMessage ="Наименование татуса обязательно для заполнения")]
        public string Name { get; set; }
    }
}
