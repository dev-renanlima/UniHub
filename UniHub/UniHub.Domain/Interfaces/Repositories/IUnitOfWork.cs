namespace UniHub.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    ICourseRepository CourseRepository { get; }

    IAssignmentRepository AssignmentRepository { get; }

    INewsRepository NewsRepository { get; }

    //INotificationRepository NotificationRepository { get; }

    void Commit();

    void Rollback();
}
