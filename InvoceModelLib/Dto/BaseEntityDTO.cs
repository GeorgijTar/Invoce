using System.ComponentModel.DataAnnotations;

namespace InvoceModelLib.Dto
    {/// <summary>
 /// Базовый класс модели
 /// </summary>
    public class BaseEntityDTO

    {
        protected BaseEntityDTO(int id)
        {
            Id = id;
        }

        public int Id { get;}
    }
}
