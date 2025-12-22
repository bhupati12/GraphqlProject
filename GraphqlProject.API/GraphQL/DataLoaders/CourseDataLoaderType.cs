using GraphqlProject.API.DTOs;
using GraphqlProject.API.Repositories;

namespace GraphqlProject.API.GraphQL.DataLoaders;

public class CourseDataLoaderType : BatchDataLoader<Guid, InstructorDTO>
{
	private readonly InstructorRepository _instructorRepository;

	public CourseDataLoaderType(IBatchScheduler batchScheduler, DataLoaderOptions options, InstructorRepository instructorRepository) : base(batchScheduler, options)
	{
		_instructorRepository = instructorRepository;
	}

	protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
	{
		return await _instructorRepository.GetManyResultById(keys, cancellationToken);
	}
}
