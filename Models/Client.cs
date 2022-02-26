using ServitalleresWeb.Models.Enums;
using ServitallersWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServitalleresWeb.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [EnumDataType(typeof(Enums.ClientType), ErrorMessage = "¡El valor ingresado es inválido!")]
        public ClientType ClientType { get; set; }
        [Required]
        [EnumDataType(typeof(Enums.TypeOfIdentification), ErrorMessage = "¡El valor ingresado es inválido!")]
        public TypeOfIdentification TypeOfIdentification { get; set; }
        [Required]
        [Range(999999, int.MaxValue, ErrorMessage = "Something Wrong!")]
        public int NumberOfIdentification { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido!")]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido!")]
        public string FirstLastName { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido!")]
        public string SecondLastName { get; set; }
        public Int64? TelephoneNumber { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido!")]
        public Int64 MobileNumber { get; set; }

        [Required(ErrorMessage = "¡Este campo es requerido!")]
        [EmailAddress]
        public string Email { get; set; }
        public string Website { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido!")]
        public string Address { get; set; }
        [Required]
        [Range(0, 138, ErrorMessage = "¡El valor ingresado es inválido!")]
        public Cities CityId { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido!")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "¡Este campo es requerido!")]
        public Int64 ContactPhone { get; set; }
        public Int64? ContactPhone2 { get; set; }
        [Required]
        public bool Credit { get; set; }
        public Int64 CreditAmount { get; set; }
        public int DaysCredit { get; set; }


        public bool Declarant { get; set; }
        public int ReteFuente { get; set; }
        public int ReteIva { get; set; }
    }
}
