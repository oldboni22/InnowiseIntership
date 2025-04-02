namespace Exceptions.NotFound;

public class ReviewFoundException(int id) : NotFoundException($"A review with id = {id} was not found")
{

}