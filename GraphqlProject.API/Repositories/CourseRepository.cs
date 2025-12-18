using GraphqlProject.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.API.Repositories;

public class CourseRepository(IDbContextFactory<SchoolDbContext> dbContextFactory)
{
	private readonly IDbContextFactory<SchoolDbContext> _dbContextFactory = dbContextFactory;

	public async Task<IEnumerable<CourseDTO>> GetAll(CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

		return await context.Courses
			.ToListAsync(cancellationToken);
	}

	public async Task<CourseDTO> GetById(Guid Id, CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

		return await context.Courses
			.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
	}

	public async Task<CourseDTO> Create(CourseDTO course, CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

		await context.Courses.AddAsync(course, cancellationToken);
		await context.SaveChangesAsync(cancellationToken);
		
		return course;
	}

	public async Task<CourseDTO> Update(CourseDTO course, CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
		var courseFind = await context.Courses.FindAsync([course.Id], cancellationToken: cancellationToken);

		if (courseFind == null) return null;

		courseFind.Name = course.Name;
		courseFind.Subject = course.Subject;
		courseFind.InstructorId = course.InstructorId;

		await context.SaveChangesAsync(cancellationToken);

		return course;
	}

	public async Task<bool> Delete(Guid courseId, CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
		var courseFind = await context.Courses.FindAsync([courseId], cancellationToken: cancellationToken);

		if (courseFind == null) return false;

		context.Courses.Remove(courseFind);
		return await context.SaveChangesAsync(cancellationToken) > 0;
	}
}
