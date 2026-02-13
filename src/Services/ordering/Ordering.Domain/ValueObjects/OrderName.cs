

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        public string Value { get; }
        private const int DefaultLength = 4;
        private OrderName(Guid value) => Value = value;

        public static OrderName Of(Guid value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            return new OrderName(value);
        }
    }
}