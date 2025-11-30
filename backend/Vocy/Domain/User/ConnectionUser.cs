namespace Domain.User;

public record ConnectionUser(User User, ConnectionRole Role) 
{
    public void DowngradeToStudent()
    {
        Role = ConnectionRole.Student;
    }
};