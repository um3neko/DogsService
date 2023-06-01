using CodeBridge.Models.Entities;

namespace CodeBridge.Repositories.DogRepository
{
    public interface IDogRepository
    {
        Task<bool> AddDog(Dog dog);
        Task<int> DogsCount();
        IQueryable<Dog> GetAll();
        IQueryable<Dog> OrderDogsByAttribute(string attribute, Order order);
        IQueryable<Dog> Paginate(IQueryable<Dog> dogs, int pageNumber = 1, int pageSize = 2);
    }
}