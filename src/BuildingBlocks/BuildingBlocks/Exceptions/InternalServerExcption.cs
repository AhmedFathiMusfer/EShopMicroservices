namespace BuildingBlocks.Exceptions
{
    public class InternalServerExcption : Exception
    {
        public InternalServerExcption(string message) : base(message)
        {

        }
        public InternalServerExcption(string message, string details) : base(message)
        {
            Details = details;
        }
        public string? Details { get; }
    }
}