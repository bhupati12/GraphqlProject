using GraphqlProject.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.API.Repositories;

public class InstructorRepository(IDbContextFactory<SchoolDbContext> dbContextFactory)
{
	private readonly IDbContextFactory<SchoolDbContext> _dbContextFactory = dbContextFactory;

	public async Task<IEnumerable<InstructorDTO>> GetAll(CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
		return await context.Instructors
			.ToListAsync(cancellationToken);
	}

	public async Task<InstructorDTO> GetById(Guid Id, CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
		return await context.Instructors
			.FindAsync([Id], cancellationToken);
	}

	public async Task<IReadOnlyDictionary<Guid, InstructorDTO>> GetManyResultById(IEnumerable<Guid> Ids, CancellationToken cancellationToken)
	{
		await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
		return await context.Instructors.AsNoTracking().Where(x => Ids.Contains(x.Id))
			.ToDictionaryAsync(x => x.Id, cancellationToken);
	}
}
