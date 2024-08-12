using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Filters;
using Web.Constants;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Web.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public UserController(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    [RequiredXDeviceHeaderFilter(HttpHeaderXDevice.Web, HttpHeaderXDevice.Mobile, HttpHeaderXDevice.Mail)]
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        var dbUser = _mapper.Map<Infrastructure.Models.User>(user);

        _applicationDbContext.Users.Add(dbUser);
        await _applicationDbContext.SaveChangesAsync();
        return Ok(JsonConvert.SerializeObject(dbUser.Id));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        var dbUser = await _applicationDbContext.Users.Where(
            user => user.Id == id).FirstAsync();

        if (dbUser == null)
        {
            return NotFound();
        }
        return Ok(JsonConvert.SerializeObject(dbUser));
    }

    [HttpGet]
    public async Task<IActionResult> Find(UserSearchOptions userSearchOptions)
    {
        IQueryable<Infrastructure.Models.User> query = _applicationDbContext.Users;

        if (userSearchOptions.Surname != null)
        {
            query = query.Where(user => user.Surname == userSearchOptions.Surname);
        }
        if (userSearchOptions.Name != null)
        {
            query = query.Where(user => user.Name == userSearchOptions.Name);
        }
        if (userSearchOptions.Patronymic != null)
        {
            query = query.Where(user => user.Patronymic == userSearchOptions.Patronymic);
        }
        if (userSearchOptions.PhoneNumber != null)
        {
            query = query.Where(user => user.PhoneNumber == userSearchOptions.PhoneNumber);
        }
        if (userSearchOptions.Email != null)
        {
            query = query.Where(user => user.Email == userSearchOptions.Email);
        }

        var dbUsers = await query.Take(100).ToListAsync();

        if (dbUsers == null)
        {
            return NotFound();
        }

        return Ok(JsonConvert.SerializeObject(dbUsers));
    }
}
