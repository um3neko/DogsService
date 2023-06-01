using AutoMapper;
using CodeBridge.Models.DTOs.In;
using CodeBridge.Models.Entities;
using CodeBridge.Repositories.DogRepository;
using CodeBridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeBridge.Controllers;

[Route("dogs")]
[ApiController]
public class DogsController : ControllerBase
{
    private readonly IDogRepository _dogs;
    private readonly IMapper _mapper;

    public DogsController(IDogRepository dogRepository, IMapper mapper)
    {
        _dogs = dogRepository;
        _mapper = mapper;
    }
    /// <summary>
    /// if the sort type (descending, ascending) is not specified, the default sort is ascending
    /// elso by default(if page size and number not specified) we get only 2 size collection of dogs
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="orderByDesc"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetDogs(string? attribute = "", bool? orderByDesc = false, int pageNumber = 1, int pageSize = 2)
    {
        IQueryable<Dog> dogs;
        try
        {
            dogs = orderByDesc == true
                ? _dogs.OrderDogsByAttribute(attribute, Order.desc)
                : _dogs.OrderDogsByAttribute(attribute, Order.asc);
            var paginatedDogs = _dogs.Paginate(dogs, pageNumber, pageSize);
            return Ok(paginatedDogs);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest("Invalid attribute");
        } 
    }

    [HttpPost]
    public async Task<IActionResult> AddDog([FromBody] CreateDogDTO value)
    {
        if(ModelState.IsValid)
        {
            try
            {
                var dog = _mapper.Map<Dog>(value);
                var success = await _dogs.AddDog(dog);
                if (success == true)
                {
                    return Ok(dog);
                }
                else
                {
                    return BadRequest("Error while adding");
                }
            }
            catch(DbUpdateException e)
            {
                return BadRequest("Name should be unique. Try another name");
            }            
            
        }
        else
        {
            return BadRequest("Invalide dog");
        }
    }

}
