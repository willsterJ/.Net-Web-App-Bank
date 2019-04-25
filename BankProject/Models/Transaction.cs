using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankProject.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string DateStringFormat { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }
        public string AmountStringFormat { get; set; }

        public double Balance { get; set; }

        public bool Status { get; set; }

        [Required]
        public CheckOrDepositEnum CheckOrDeposit { get; set; }

        public int BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }

    public enum CheckOrDepositEnum { Check, Deposit}

}