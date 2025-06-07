using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController(AppDbContext context) : BaseApiController
{
  [HttpGet]
  public async Task<ActionResult<List<Domain.Activity>>> GetActivities()
  {
    return await context.Activities.ToListAsync();
  }
}
