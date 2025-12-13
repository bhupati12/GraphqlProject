using GraphqlProject.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.API.Repositories;

public class SchoolDbContext : DbContext
{
	public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
	{
	}

	public DbSet<StudentDTO> Students { get; set; }
	public DbSet<CourseDTO> Courses { get; set; }
	public DbSet<InstructorDTO> Instructors { get; set; }
}
