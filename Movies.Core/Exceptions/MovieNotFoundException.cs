namespace Movies.Core.Exceptions
{
    public class MovieNotFoundException : NotFoundException
    {
        public MovieNotFoundException(int id) : base($"The movie with ID {id} was not found")
        {
            
        }
    }
}
