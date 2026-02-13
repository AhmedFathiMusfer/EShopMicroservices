
namespace Ordering.Domain.ValueObjects
{
    public record ProducId
    {
        public Guid Value { get; }
        private ProducId(Guid value) => Value = value;

        public static ProducId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainEception("ProducId can not be empty.");
            }

            return new ProducId(value);
        }
    }
}