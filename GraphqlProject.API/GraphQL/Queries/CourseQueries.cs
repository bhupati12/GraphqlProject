using Bogus;
using GraphqlProject.API.CommonModel;
using GraphqlProject.API.GraphQL.Type;
using HotChocolate.Language;

namespace GraphqlProject.API.GraphQL.Queries;

[ExtendObjectType(OperationType.Query)]
public class CourseQueries
{
	Faker<InstructorType> _instructorFacker;
	Faker<StudentType> _studentFaker;
	Faker<CourseType> _courseFaker;

	public CourseQueries()
	{
		_instructorFacker = new Faker<InstructorType>()
			.RuleFor(c => c.Id, f => Guid.NewGuid())
			.RuleFor(c => c.FirstName, f => f.Name.FirstName())
			.RuleFor(c => c.LastName, f => f.Name.LastName())
			.RuleFor(c => c.Salary, f => f.Random.Double(0, 100000));

		_studentFaker = new Faker<StudentType>()
			.RuleFor(c => c.Id, f => Guid.NewGuid())
			.RuleFor(c => c.FirstName, f => f.Name.FirstName())
			.RuleFor(c => c.LastName, f => f.Name.LastName())
			.RuleFor(c => c.GPA, f => f.Random.Double(0, 100000));

		_courseFaker = new Faker<CourseType>()
			.RuleFor(c => c.Id, f => Guid.NewGuid())
			.RuleFor(c => c.Name, f => f.Name.JobTitle())
			.RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
			.RuleFor(c => c.Instructor, f => _instructorFacker.Generate())
			.RuleFor(c => c.Students, f => _studentFaker.Generate(2));
	}
	
	public IEnumerable<CourseType> GetCourses()
	{
		var courses = _courseFaker.Generate(10);

		return courses;
	}

	public CourseType GetCourse(Guid id)
	{
		var course = _courseFaker.Generate();
		course.Id = id;

		return course;
	}
}
