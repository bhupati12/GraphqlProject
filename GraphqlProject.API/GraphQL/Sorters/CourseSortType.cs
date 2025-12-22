using GraphqlProject.API.GraphQL.Type;
using HotChocolate.Data.Sorting;

namespace GraphqlProject.API.GraphQL.Sorters;

public class CourseSortType : SortInputType<CourseType>
{
	protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
	{
		descriptor.Ignore(x => x.Id);
		descriptor.Ignore(x => x.InstructorId);
	}
}
