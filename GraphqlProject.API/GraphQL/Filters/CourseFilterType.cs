using GraphqlProject.API.GraphQL.Type;
using HotChocolate.Data.Filters;

namespace GraphqlProject.API.GraphQL.Filters;

public class CourseFilterType : FilterInputType<CourseType>
{
	protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
	{
		descriptor.Field(x => x.Students).Ignore();
	}
}
