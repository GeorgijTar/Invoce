using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InvoceDb.Entitys
{
    /// <summary>
    /// Класс Контрагент
    /// </summary>
    public class Counterparty : BaseEntity
    {
        public virtual Status Status { get; set; }
        [Required(ErrorMessage = "Наименование обязательное поле")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Платежное наименование обязательное поле")]
        public string PayName { get; set; }
        [Required(ErrorMessage = "ИНН обязательное поле"), StringLength(12, MinimumLength = 10, ErrorMessage = "Недопустимый ИНН")]
        public string INN { get; set; }
        [StringLength(9, MinimumLength = 0, ErrorMessage = "Недопустимый КПП")]
        public string KPP { get; set; }
        [StringLength(15, MinimumLength = 13, ErrorMessage = @"Недопустимый ОГРН/ОГРНИП")]
        public string OGRN { get; set; }
        [StringLength(10, MinimumLength = 8, ErrorMessage = "Недопустимый ОКПО")]
        public string OKPO { get; set; }
        [Phone(ErrorMessage = "Не верный формат телефона")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Не верный формат адреса электронной почты")]
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; } //Дата регистрации организации
        public string Address { get; set; } //Адрес для документов
        public DateTime TimeSpan { get; set; }// Дата добавления в бд

        public List<PayDetail> PayDetail { get; set; }
    }
}
