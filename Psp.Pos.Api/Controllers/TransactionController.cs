using Microsoft.AspNetCore.Mvc;
using Psp.Pos.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psp.Pos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private static List<Transaction> transactions = new List<Transaction>
        {
            new Transaction { Id = 1, OrderId = 101, StaffUserId = 201, PaymentType = "Credit Card", DiscountApplied = 10, Tax = 5, Tip = 2, TotalDiscount = 12 },
            new Transaction { Id = 2, OrderId = 102, StaffUserId = 202, PaymentType = "Cash", DiscountApplied = 5, Tax = 2, Tip = 1, TotalDiscount = 6 }
        };

        // GET: api/transactions
        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> GetTransactions()
        {
            return Ok(transactions);
        }

        // GET: api/transactions/{id}
        [HttpGet("{id}")]
        public ActionResult<Transaction> GetTransaction(int id)
        {
            var transaction = transactions.Find(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // POST: api/transactions
        [HttpPost]
        public ActionResult<Transaction> CreateTransaction([FromBody] Transaction newTransaction)
        {
            newTransaction.Id = transactions.Count + 1;
            transactions.Add(newTransaction);

            return CreatedAtAction(nameof(GetTransaction), new { id = newTransaction.Id }, newTransaction);
        }

        // PUT: api/transactions/{id}
        [HttpPut("{id}")]
        public ActionResult<Transaction> UpdateTransaction(int id, [FromBody] Transaction updatedTransaction)
        {
            var existingTransaction = transactions.Find(t => t.Id == id);

            if (existingTransaction == null)
            {
                return NotFound();
            }

            // Update the existing transaction with the new data
            existingTransaction.OrderId = updatedTransaction.OrderId;
            existingTransaction.StaffUserId = updatedTransaction.StaffUserId;
            existingTransaction.PaymentType = updatedTransaction.PaymentType;
            existingTransaction.DiscountApplied = updatedTransaction.DiscountApplied;
            existingTransaction.Tax = updatedTransaction.Tax;
            existingTransaction.Tip = updatedTransaction.Tip;
            existingTransaction.TotalDiscount = updatedTransaction.TotalDiscount;

            return Ok(existingTransaction);
        }

        // DELETE: api/transactions/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTransaction(int id)
        {
            var transactionToRemove = transactions.Find(t => t.Id == id);

            if (transactionToRemove == null)
            {
                return NotFound();
            }

            transactions.Remove(transactionToRemove);

            return NoContent();
        }
    }
}
