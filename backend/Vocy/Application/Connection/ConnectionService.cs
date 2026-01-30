using Application.Connection.Commands;
using Application.Interfaces;
using Domain.User;

namespace Application.Connection;

public class ConnectionService(IUnitOfWork unitOfWork)
{
    private readonly IRepository<Domain.User.Connection> _connectionRepository =
        unitOfWork.GetRepository<Domain.User.Connection>();

    private readonly IRepository<User> _userRepository = unitOfWork.GetRepository<User>();

    public async Task CreateConnection(CreateConnectionCommand createConnectionCommand, string userToken)
    {
        var connectionUsers = new List<ConnectionUser>();

        foreach (var connectionUnit in createConnectionCommand.ConnectionUnits)
        {
            var user = await _userRepository.GetFirstAsync(u => u.Email == connectionUnit.Email);

            connectionUsers.Add(new ConnectionUser(user.Id, connectionUnit.Role));
        }

        await _connectionRepository.AddAsync((new Domain.User.Connection(connectionUsers)));
        await unitOfWork.SaveChangesAsync();
    }
}