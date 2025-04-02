namespace Exceptions.NotFound;

public class UserNotFoundException(int id) : NotFoundException($"A user with id = {id} was not found")
{

}