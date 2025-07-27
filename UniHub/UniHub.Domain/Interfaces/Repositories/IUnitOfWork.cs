namespace UniHub.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    ICourseRepository CourseRepository { get; }

    void Commit();

    void Rollback();
}
