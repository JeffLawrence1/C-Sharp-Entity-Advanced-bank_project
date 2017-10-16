using System;

namespace bank_project.Models 
{
    public class Withdrawal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public float Amount { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
    }
}