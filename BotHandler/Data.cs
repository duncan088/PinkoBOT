using FluentValidation;

namespace BotHandler
{
    public class Data
    {
        public string wallet { get; set; }
        public string privateKey { get; set; }
        public string bscNode { get; set; }
        public string server { get; set; }
        public bool useServer { get; set; } = false;
        public string token { get; set; }
        public int delay { get; set; } = 0;
        public decimal profit { get; set; } = 0;
        public int gasPrice { get; set; } = 25;
        public long gasLimit { get; set; } = 500000;
        public decimal buyAmount { get; set; } = 0;
        public decimal sellAmount { get; set; } = 0;
        public bool IsObserver { get; set; } = false;
        public bool audit { get; set; } = false;

    }

    public class DataValidator : AbstractValidator<Data>
    {
        public DataValidator()
        {
            RuleFor(x => x.bscNode).NotEmpty();
            RuleFor(z => z.wallet).NotEmpty().Length(42);
            RuleFor(x => x.privateKey).NotEmpty().Length(66);
            RuleFor(x => x.token).NotEmpty().Length(42);
            RuleFor(x => x.delay).GreaterThanOrEqualTo(1).Equal(0);
            RuleFor(x => x.profit).GreaterThanOrEqualTo(1).Equal(0);
            RuleFor(x => x.gasLimit).GreaterThanOrEqualTo(50000);
            RuleFor(x => x.gasPrice).GreaterThanOrEqualTo(5);
            RuleFor(x => x.buyAmount).GreaterThan(0);
            RuleFor(x => x.sellAmount).GreaterThan(0);
        }
    }
}
