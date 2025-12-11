using GraphqlProject.API.GraphQL.Model;
using GraphqlProject.API.GraphQL.Type;
using HotChocolate.Language;

namespace GraphqlProject.API.GraphQL.Mutation;

[ExtendObjectType(OperationType.Mutation)]
public class CourseMutation
{
	private readonly List<CourseResult> _createdCourse;

	public CourseMutation()
	{
		_createdCourse = [];
	}

	public CourseResult CreateCourse(CourseInput courseInput)
	{
		var course = new CourseResult
		{
			Id = Guid.NewGuid(),
			Name = courseInput.Name,
			Subject = courseInput.Subject,
			InstructorId = courseInput.InstructorId
		};

		_createdCourse.Add(course);

		return course;
	}

	public CourseResult UpdateCourse(Guid id, CourseInput courseInput)
	{
		var course = _createdCourse.FirstOrDefault(c => c.Id == id);
		if (course == null)
		{
			throw new GraphQLException(ErrorBuilder.New()
				.SetMessage("Course not found.")
				.SetCode("COURSE_NOT_FOUND")
				.Build());
		}

		course.Name = courseInput.Name;
		course.Subject = courseInput.Subject;
		course.InstructorId = courseInput.InstructorId;
		return course;
	}

	public bool DeleteCourse(Guid id)
	{
		return _createdCourse.RemoveAll(c => c.Id == id) > 0;
	}
}
