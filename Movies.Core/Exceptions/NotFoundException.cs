namespace Movies.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Title { get; }
        public NotFoundException(string message, string title = "Not found") : base(message)
        {
            Title = title;
        }

    }
}
