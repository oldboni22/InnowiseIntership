namespace Exceptions.AlreadyExists;

public class UserAlreadyExistsException(string email) : AlreadyExistsException($"a user with email {email} already exists")
{
    
}