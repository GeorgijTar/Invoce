using System.ComponentModel.DataAnnotations;


namespace InvoceModelLib.Dto
{/// <summary>
 /// Слас реализации модели Статусов
 /// </summary>
    public class StatusDTO : BaseEntityDTO
    {
        public StatusDTO(int id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
