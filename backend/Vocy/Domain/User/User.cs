using Domain.Base;

namespace Domain.User;

public record User(String Name, String Email, String Password) : BaseIdentity;