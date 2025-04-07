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

    private async Task<Courier> TryGetCourierAsync(int id,bool trackChanges)
    {
        var courier = await _repositoryManager.Courier.GetCourierByIdAsync(id,trackChanges);
        if (courier == null)
            throw new CourierFoundException(id);
        
        return courier;
    }
    
    public async Task<CourierDto> GetCourierByIdAsync(int id, bool trackChanges)
    {
        var courier = await TryGetCourierAsync(id,trackChanges);

        var result = _mapper.Map<CourierDto>(courier);
        return result;
    }

    public async Task<CourierDto> CreateCourierAsync(CourierCreationDto courier)
    {
        var entity = _mapper.Map<Courier>(courier);

        _repositoryManager.Courier.CreateCourier(entity);
        await _repositoryManager.SaveAsync();

        return _mapper.Map<CourierDto>(entity);
    }

    public async Task UpdateCourierAsync(int id, CourierForUpdateDto courier)
    {
        var entity = await TryGetCourierAsync(id,true);    

        _mapper.Map(courier,entity);
        await _repositoryManager.SaveAsync();
    }
}