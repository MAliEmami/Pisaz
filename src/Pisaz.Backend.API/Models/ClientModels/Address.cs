using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
// using Pisaz.Backend.API.Models.Base;

namespace Pisaz.Backend.API.Models.ClientModels
{
    public class Address //: GeneralModel
    {
        public int Id { get; set; }
        public required Client Client { get; set; }
        public required string Province { get; set; } = "Theran";
        public required string Remainder { get; set; }
        
    }
}