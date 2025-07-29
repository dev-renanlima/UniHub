using System.Data;
using UniHub.Domain.Entities;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Infrastructure.Context;

namespace UniHub.Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AssignmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Assignment?> CreateAsync(Assignment assignment)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertAssignment\"(@p_CourseId, @p_Title, @p_Description, @p_ExpirationDate, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text;

            _dbContext.CreateParameter(command, "p_CourseId", assignment.CourseId);
            _dbContext.CreateParameter(command, "p_Title", assignment.Title);
            _dbContext.CreateParameter(command, "p_Description", assignment.Description);
            _dbContext.CreateParameter(command, "p_ExpirationDate", assignment.ExpirationDate);
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();

            assignment.SetIdentity((long)result!);

            return assignment;
        }

        public async Task<AssignmentAttachment?> CreateAssignmentAttachmentAsync(AssignmentAttachment assignmentAttachment)
        {
            using var command = _dbContext.CreateCommand();

            command.CommandText = "SELECT public.\"InsertAssignmentAttachment\"(@p_AssignmentId, @p_Url, @p_Type, @p_CreationDate, @p_UpdateDate)";
            command.CommandType = CommandType.Text;

            _dbContext.CreateParameter(command, "p_AssignmentId", assignmentAttachment.AssignmentId);
            _dbContext.CreateParameter(command, "p_Url", assignmentAttachment.Url);
            _dbContext.CreateParameter(command, "p_Type", assignmentAttachment.Type.ToString());
            _dbContext.CreateParameter(command, "p_CreationDate", DateTime.UtcNow);
            _dbContext.CreateParameter(command, "p_UpdateDate", DateTime.UtcNow);

            var result = await command.ExecuteScalarAsync();

            assignmentAttachment.SetIdentity((long)result!);

            return assignmentAttachment;
        }
    }
}
