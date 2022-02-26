using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServitalleresWeb.Models
{
    public class VehicleBrand
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "¡Este campo no puede quedar vacío!")]
        public string Make { get; set; }
        [Required(ErrorMessage = "¡Este campo no puede quedar vacío!")]
        public string Model { get; set; }
    }
}
