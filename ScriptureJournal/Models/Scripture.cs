using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptureJournal.Models
{
    public class Scripture
    {
        public int ID { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-Z0-9""'\s-.,]*$")]
        [Required]
        public string Book { get; set; }

        [Range(1, 160)]
        [Required]
        public int Chapter { get; set; }

        [Range(1,160)]
        [Required]
        public int Verse { get; set; }

        [StringLength(355, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-Z0-9""'\s-.,]*$")]
        [Required]
        public string Note { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
