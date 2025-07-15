namespace Movies.Core.Exceptions
{
    public class ActorAlreadyInMovieBadRequestException : BadRequestException
    {
        public ActorAlreadyInMovieBadRequestException(int actorId, int movieId) : base($"The Actor with ID {actorId} is already in the movie with ID {movieId}")
        {

        }
    }
}
