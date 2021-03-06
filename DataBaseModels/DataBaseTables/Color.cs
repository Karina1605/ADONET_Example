using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseModels.DataBaseTables
{
    public class Color
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
