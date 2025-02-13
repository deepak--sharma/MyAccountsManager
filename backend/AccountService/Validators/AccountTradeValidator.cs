using AccountService.Models;
using FluentValidation;

namespace AccountService.Validators
{
    public class AccountTradeValidator : AbstractValidator<AccountTrade>
    {
        public AccountTradeValidator()
        {
            var tradeTypes = new[] { "Buy", "Sell" };
            var statuses = new[] { "Placed", "Executed", "Expired" };

            RuleFor(x => x.SecurityCode)
                .NotEmpty().WithMessage("Security code is required.")
                .Length(3).WithMessage("Security code must be 3 characters.");

            RuleFor(x => x.TradeType)
                .NotEmpty().WithMessage("Trade type is required.")
                .Must(tradeType => tradeTypes.Contains(tradeType, StringComparer.InvariantCultureIgnoreCase))
                .WithMessage("Trade type must be either 'Buy' or 'Sell'.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => statuses.Contains(status, StringComparer.InvariantCultureIgnoreCase))
                .WithMessage("Status must be 'Placed', 'Executed', or 'Expired'.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .Must(x => decimal.TryParse(x, out decimal n) && n > 0).WithMessage("Amount must be a valid number and greater than zero.");

            RuleFor(x => x.Timestamp)
                .NotEmpty().WithMessage("Timestamp is required.")
                .Must(x => DateTime.TryParse(x, out _)).WithMessage("Timestamp must be a valid date.");
        }
    }
}