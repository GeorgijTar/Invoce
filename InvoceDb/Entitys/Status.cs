using System.ComponentModel.DataAnnotations;


namespace InvoceDb.Entitys
{/// <summary>
 /// Слас реализации модели Статусов
 /// </summary>
    public class Status: BaseEntity
    {
        [Required (ErrorMessage ="Наименование татуса обязательно для заполнения")]
        public string Name { get; set; }
    }
}
