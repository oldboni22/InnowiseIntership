namespace Exceptions.NotFound;

public class UserNotFoundException : NotFoundException
{

    public UserNotFoundException(int id) : base($"User with id {id} not found"){ }
    public UserNotFoundException(string email) : base($"A user with id = {email} was not found")
    { }
}