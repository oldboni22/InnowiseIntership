using AutoMapper;
using Domain.Entities;
using Exceptions.NotFound;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input;
using Shared.Input.Creation;
using Shared.Output;

namespace Service;

public class CourierService(IRepositoryManager repositoryManager, IMapper mapper) : ICourierService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public async Task<CourierDto> GetCourierByIdAsync(int id, bool trackChanges)
    {
        var courier = await _repositoryManager.Courier.GetCourierByIdAsync(id,false);
        if (courier == null)
            throw new CourierFoundException(id);

        var result = _mapper.Map<CourierDto>(courier);
        return result;
    }

    public async Task CreateCourierAsync(CourierCreationDto courier)
    {
        var entity = _mapper.Map<Courier>(courier);

        _repositoryManager.Courier.CreateCourier(entity);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateCourier(int id, CourierForUpdateDto courier)
    {
        var entity = await GetCourierByIdAsync(id,true);
        if (entity == null)
            throw new CourierFoundException(id);

        _mapper.Map(courier,entity);
        await _repositoryManager.SaveAsync();
    }
}