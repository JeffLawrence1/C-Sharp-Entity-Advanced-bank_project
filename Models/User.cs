using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace bank_project.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public float Balance { get; set; }

        public List<Withdrawal> Withdrawals { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public User(){

            Withdrawals = new List<Withdrawal>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
    
}