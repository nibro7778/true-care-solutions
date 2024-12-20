namespace Staffs.Domain;

public class Staff
{
    private Staff(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public static Staff Create(Guid id, string firstName, string lastName) =>
        new(id, firstName, lastName);

    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

 }