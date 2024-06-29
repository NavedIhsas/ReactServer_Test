using Microsoft.EntityFrameworkCore;
using ReactServer.Models;

namespace ReactServer.Services
{
    public interface ICourseGroupRepository
    {
        Task<IEnumerable<CourseGroup>> GetAllAsync();
        Task<CourseGroup> GetByIdAsync(int id);
        Task AddAsync(CourseGroup courseGroup);
        Task UpdateAsync(CourseGroup courseGroup);
        Task DeleteAsync(int id);
    }

    public class CourseGroupRepository(ApplicationDbContext _context) : ICourseGroupRepository
    {
        public async Task<IEnumerable<CourseGroup>> GetAllAsync()
        {
            return await _context.CourseGroups.Include(cg => cg.Courses).Include(cg => cg.Groups).ToListAsync();
        }

        public async Task<CourseGroup> GetByIdAsync(int id)
        {
            return await _context.CourseGroups.Include(cg => cg.Courses).Include(cg => cg.Groups)
                .FirstOrDefaultAsync(cg => cg.Id == id);
        }

        public async Task AddAsync(CourseGroup courseGroup)
        {
            _context.CourseGroups.Add(courseGroup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseGroup courseGroup)
        {
            _context.CourseGroups.Update(courseGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var courseGroup = await _context.CourseGroups.FindAsync(id);
            if (courseGroup != null)
            {
                _context.CourseGroups.Remove(courseGroup);
                await _context.SaveChangesAsync();
            }
        }
    }

}
