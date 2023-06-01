using CodeBridge.Models.Contexts;
using CodeBridge.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeBridge.Repositories.DogRepository;

public enum Order {
    asc,
    desc
}

public class DogRepository : IDogRepository
{
    private readonly DogContext _dogContext;

    public DogRepository(DogContext dogContext)
    {
        _dogContext = dogContext;
    }

    public IQueryable<Dog> GetAll()
    {
        var values = _dogContext.Dogs.AsQueryable();
        return values;
    }

    public async Task<bool> AddDog(Dog dog)
    {
        await _dogContext.Dogs.AddAsync(dog);
        var result = await _dogContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<int> DogsCount()
    {
        return await _dogContext.Dogs.CountAsync();
         
    }

    public IQueryable<Dog> OrderDogsByAttribute(string attribute, Order order)
    {
        IQueryable<Dog>? dogs = attribute.ToLower() switch
        {
            "name" => order == Order.desc ? _dogContext.Dogs.OrderByDescending(d => d.Name) : _dogContext.Dogs.OrderBy(d => d.Name),
            "color" => order == Order.desc ? _dogContext.Dogs.OrderByDescending(d => d.Color) : _dogContext.Dogs.OrderBy(d => d.Color),
            "tail_length" => order == Order.desc ? _dogContext.Dogs.OrderByDescending(d => d.TailLength) : _dogContext.Dogs.OrderBy(d => d.TailLength),
            "weigth" => order == Order.desc ? _dogContext.Dogs.OrderByDescending(d => d.Weight) : _dogContext.Dogs.OrderBy(d => d.Weight),
            "" => GetAll(),
            _ => throw new InvalidOperationException("Invalid attribute for sorting")
        };
        return dogs;
    }

    public IQueryable<Dog> Paginate(IQueryable<Dog> dogs, int pageNumber = 1, int pageSize = 2)
    {
        var paginatedDogs = dogs.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsQueryable<Dog>();
        return paginatedDogs;
    }


}
       
