// Controllers/UsersController.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReactServer.Models;

namespace ReactServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ApplicationDbContext context) : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<UserApiModel>> GetUsers([FromQuery] TableFiltering filter)
        {
            List<ColumnFilter> columnFilters = null;
            List<SortingFilter> sortingFilters = null;

            try
            {
                if (!string.IsNullOrEmpty(filter.ColumnFilter))
                {
                    columnFilters = JsonSerializer.Deserialize<List<ColumnFilter>>(filter.ColumnFilter);
                }
                if (!string.IsNullOrEmpty(filter.Sorting))
                {
                    sortingFilters = JsonSerializer.Deserialize<List<SortingFilter>>(filter.Sorting);
                }
            }
            catch (Exception e)
            {
                // Log the exception
                Console.WriteLine(e.Message);
                return BadRequest("Error in deserializing filters");
            }

            var query = context.Users.AsQueryable();

            // Apply column filters
            if (columnFilters != null)
            {
                foreach (var columnFilter in columnFilters)
                {
                    switch (columnFilter.Id)
                    {
                        case "id":
                            query = query.Where(u => u.Id.Equals(columnFilter.Value));
                            break;

                        case "firstName":
                            query = query.Where(u => u.FirstName.Contains(columnFilter.Value));
                            break;
                        case "lastName":
                            query = query.Where(u => u.LastName.Contains(columnFilter.Value));
                            break;
                        case "email":
                            query = query.Where(u => u.Email.Contains(columnFilter.Value));
                            break;
                        case "phoneNumber":
                            query = query.Where(u => u.PhoneNumber.Contains(columnFilter.Value));
                            break;
                        case "address":
                            query = query.Where(u => u.Address.Contains(columnFilter.Value));
                            break;
                        case "dateOfBirth":
                            if (DateTime.TryParse(columnFilter.Value, out var dateOfBirth))
                            {
                                query = query.Where(u => u.DateOfBirth.Date == dateOfBirth.Date);
                            }
                            break;
                        case "isActive":
                            if (bool.TryParse(columnFilter.Value, out var isActive))
                            {
                                query = query.Where(u => u.IsActive == isActive);
                            }
                            break;
                        case "gender":
                            query = query.Where(u => u.Gender.Contains(columnFilter.Value));
                            break;
                    }
                }
            }

            // Apply global filter
            if (!string.IsNullOrEmpty(filter.GlobalFilter))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(filter.GlobalFilter) ||
                    u.LastName.Contains(filter.GlobalFilter) ||
                    u.Email.Contains(filter.GlobalFilter) ||
                    u.PhoneNumber.Contains(filter.GlobalFilter) ||
                    u.Address.Contains(filter.GlobalFilter) ||
                    u.Gender.Contains(filter.GlobalFilter)
                );
            }

            // Apply sorting
            //if (sortingFilters != null)
            //{
            //    IOrderedQueryable<User> orderedQuery = null;
            //    foreach (var sortingFilter in sortingFilters)
            //    {
            //        if (orderedQuery == null)
            //        {
            //            orderedQuery = sortingFilter.Desc
            //                ? query.OrderByDescending(u => EF.Property<object>(u, sortingFilter.Id))
            //                : query.OrderBy(u => EF.Property<object>(u, sortingFilter.Id));
            //        }
            //        else
            //        {
            //            orderedQuery = sortingFilter.Desc
            //                ? orderedQuery.ThenByDescending(u => EF.Property<object>(u, sortingFilter.Id))
            //                : orderedQuery.ThenBy(u => EF.Property<object>(u, sortingFilter.Id));
            //        }
            //    }

            //    query = orderedQuery ?? query;
            //}

            var pagination = new Pagination<User>(filter.PageSize, filter.PageIndex);
            var pagedResult = await pagination.Paginate(query);

            var result = new UserApiModel()
            {
                Data = pagedResult.Items,
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                TotalCount = pagedResult.TotalCount,
                PerPage = pagedResult.PerPage
            };

            return result;
        }

        public class ColumnFilter
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }
        public class SortingFilter
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
            [JsonPropertyName("desc")]
            public bool Desc { get; set; }
        }


        public class TableFiltering
        {
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
            public string ColumnFilter { get; set; }
            public string Sorting { get; set; }
            public string GlobalFilter { get; set; }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut]
        public async Task<IActionResult> PutUser(User user)
        {
            if (!await context.Users.AnyAsync(x => x.Id.Equals(user.Id)))
            {
                return BadRequest();
            }


            context.Entry(user).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return context.Users.Any(e => e.Id == id);
        }
    }
}
