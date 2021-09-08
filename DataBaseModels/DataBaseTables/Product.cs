using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBaseModels.DataBaseTables
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public int Count { get; set; }
        public Category Category { get; set; }
        public Color Color { get; set; }
        public string PhotoPath { get; set; }
    }
}
