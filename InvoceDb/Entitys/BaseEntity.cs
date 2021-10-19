using System.ComponentModel.DataAnnotations;

namespace InvoceDb.Entitys
{/// <summary>
 /// Базовый класс модели
 /// </summary>
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
