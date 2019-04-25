using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankProject.Models
{
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength (7,MinimumLength = 7)]
        [RegularExpression("^[0-9]+$", ErrorMessage= "Account number must be 7 digit numbers")]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public bool Status { get; set; }


        /**
         * Relations 
         **/
        public string UserId { get; set; }
        [ForeignKeyAttribute("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}