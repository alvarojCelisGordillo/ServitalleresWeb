using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServitalleresWeb.Models.Enums
{
    public enum TypeOfIdentification
    {
        [Display(Name = "Cédula de Ciudadanía")]
        CedulaDeCiudadania = 344,
        Pasaporte = 932
    }
}
