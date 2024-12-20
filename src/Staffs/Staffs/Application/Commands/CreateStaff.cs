using Microsoft.Extensions.Logging;
namespace Staffs.Application.Commands;

public static class CreateStaff
{
    public record Command(Guid Id, string FirstName, string LastName) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Id).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(255);
        }
    }

    public class Handler(IStaffRepository repository, ILogger<Handler> log) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken token)
        {
            var staff = Staff.Create(command.Id,command.FirstName,command.LastName);
            await repository.Add(staff);
        }
    }
}