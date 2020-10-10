using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reisbureau.Models
{
    public class Klant
    {
        [Required(ErrorMessage = "Gelieve {0} in te vullen!")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Gelieve {0} in te vullen!")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Gelieve {0} in te vullen!")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Gelieve {0} in te vullen!")]
        public int? Postcode { get; set; }

        [Required(ErrorMessage = "Gelieve {0} in te vullen!")]
        public string Gemeente { get; set; }

        [Required(ErrorMessage = "Gelieve {0} in te vullen!")]
        [DataType(DataType.EmailAddress)]
        public string EmailAdres { get; set; }

        [Required(ErrorMessage = "Gelieve {0} in te vullen!")]
        public long? TelefoonNummer{ get; set; }
    }
}
