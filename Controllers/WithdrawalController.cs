using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bank_project.Models;
using System.Linq;

namespace bank_project.Controllers
{
    public class WithdrawalController : Controller
    {
        private YourContext _context;
 
        public WithdrawalController(YourContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("ShowAccount/{UserID}")]
        public IActionResult ShowAccount(int UserID)
        {
            int? ID = HttpContext.Session.GetInt32("UserID");
            if(UserID != ID){
                return Redirect("/");
            }
            User user = _context.Users.Include(x => x.Withdrawals).Where(x => x.UserID == UserID).SingleOrDefault();  //.Include(x => x.Withdrawals)
            if(user.Withdrawals != null){
                user.Withdrawals = user.Withdrawals.OrderByDescending(y => y.createdat).ToList();
            }
            ViewBag.info = user;
            return View("Account");
        }

        [HttpPost]
        [Route("withdraw")]
        public IActionResult Withdraw(string type, float Amount){
            int? ID = HttpContext.Session.GetInt32("UserID");
            User user = _context.Users.Where(x => x.UserID == ID).SingleOrDefault();
            if(Amount < 0 && ((Amount * -1) > user.Balance)){
                TempData["Error"] = "You too Poor!!!!!!!!!1";
            }
            else{
                Withdrawal transaction = new Withdrawal{
                    Amount = Amount,
                    User = user,
                   
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now
                };
                if(type == "deposit"){
                    user.Balance += Amount;
                }
                else if(type == "withdraw"){
                    user.Balance -= Amount;

                }
                _context.Withdrawals.Add(transaction);
                _context.SaveChanges();
            }
            return Redirect($"/ShowAccount/{ID}");

        }
    }
}
