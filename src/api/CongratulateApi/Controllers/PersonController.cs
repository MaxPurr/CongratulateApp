using CongratulateApi.Domain.Entities;
using CongratulateApi.Domain.Models.Birthday;
using CongratulateApi.Domain.Models.Image;
using CongratulateApi.Domain.Models.Person;
using CongratulateApi.Domain.Services.Interfaces;
using CongratulateApi.Extensions;
using CongratulateApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CongratulateApi.Controllers;

[ApiController]
[Route("v1/api/people")]
public class PersonController : ControllerBase
{
    private IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    private ActionResult<PersonEntity> PersonResult(long personId, PersonEntity? person)
    {
        return person == null ? NotFound($"Person with id = {personId} not found.") : Ok(person);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<PersonEntity>> GetAsync(long id, CancellationToken token)
    {
        var personGet = new PersonGetModel()
        {
            Id = id
        };
        var person = await _personService.GetAsync(personGet, token);
        return PersonResult(id, person);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<PersonEntity>>> GetAllAsync(PeopleOrderBy orderBy = PeopleOrderBy.Id, 
        bool orderByDescending = false, CancellationToken token = default)
    {
        var order = new PeopleOrderModel()
        {
            By = orderBy.ToDomain(),
            Descending = orderByDescending
        };
        var people = await _personService.GetAllAsync(order, token);
        return Ok(people);
    }
    
    [HttpGet("birthdays")]
    public async Task<ActionResult<List<BirthdayEntity>>> GetNearestBirthdays(
        int maxDaysLeft = 0, 
        BirthdaysOrderBy orderBy = BirthdaysOrderBy.PersonId, 
        bool orderByDescending = false, 
        CancellationToken token = default)
    {
        var order = new BirthdaysOrderModel()
        {
            By = orderBy.ToDomain(),
            Descending = orderByDescending
        };
        var birthdays = await _personService.GetNearestBirthdaysAsync(maxDaysLeft, order, token);
        return Ok(birthdays);
    }

    [HttpPost("add")]
    public async Task<ActionResult<PersonEntity>> AddAsync(PersonAddModel personAdd, CancellationToken token)
    {
        var person = await _personService.AddAsync(personAdd, token);
        return Ok(person);
    }

    [HttpPost("uploadPhoto/{id:long}")]
    public async Task<ActionResult<PersonEntity>> UploadPhotoAsync(long id, IFormFile photo, CancellationToken token)
    {
        Console.WriteLine($"Length: {photo.Length}");
        if (photo.Length == 0)
        {
            return BadRequest("No file uploaded");
        }
        var personGet = new PersonGetModel()
        {
            Id = id
        };
        var imageUpload = new ImageUploadModel()
        {
            FileName = photo.FileName,
            FileStream = photo.OpenReadStream()
        };
        var updatedPerson = await _personService.UploadPhotoAsync(personGet, imageUpload, token);
        return PersonResult(id, updatedPerson);
    }
    
    [HttpDelete("delete/{id:long}")]
    public async Task<ActionResult<PersonEntity>> RemoveAsync(long id, CancellationToken token)
    {
        var personRemove = new PersonRemoveModel()
        {
            Id = id
        };
        var person = await _personService.RemoveAsync(personRemove, token);
        return PersonResult(id, person);
    }
    
    [HttpPatch("update")]
    public async Task<ActionResult<PersonEntity>> UpdateAsync(PersonUpdateModel personUpdate, CancellationToken token)
    {
        var person = await _personService.UpdateAsync(personUpdate, token);
        return PersonResult(personUpdate.Id, person);
    }
}