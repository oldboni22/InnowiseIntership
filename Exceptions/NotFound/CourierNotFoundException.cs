namespace Exceptions.NotFound;

public class CourierFoundException(int id) : NotFoundException($"A courier with id = {id} was not found")
{

}