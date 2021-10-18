using System.ComponentModel.DataAnnotations;

namespace InvoceModel
{/// <summary>
 /// Базовый класс модели
 /// </summary>
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
