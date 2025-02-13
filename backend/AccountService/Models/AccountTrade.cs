using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class AccountTrade
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public required string SecurityCode { get; set; } //3 characters only
        public required string Timestamp { get; set; }
        public required string Amount { get; set; }

        public required string TradeType { get; set; } //Buy or Sell
        public required string Status { get; set; } //Placed, Executed, Expired
    }
}