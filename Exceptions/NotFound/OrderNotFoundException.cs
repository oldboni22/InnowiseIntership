namespace Exceptions.NotFound;

public class OrderNotFoundException(int id) : NotFoundException($"An order with id = {id} was not found")
{

}