using Mapster;
using Npgsql;
using System.Net;
using UniHub.Application.Exceptions;
using UniHub.Application.Resources;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.Entities;
using UniHub.Domain.Enums;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        //private readonly IRegisterLog _registerLog;

        public CourseService(IUnitOfWork unitOfWork, IUserService userService)//, IRegisterLog registerLog)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            //_registerLog = registerLog;
        }

        public async Task<CreateCourseResponseDTO> CreateAsync(CourseDTO courseDTO)
        {
            try
            {
                var course = courseDTO.Adapt<Course>();

                var user = await _userService.GetUserByClerkIdAsync(course.AdminId!);

                if (user.Role != UserRole.ADMIN.ToString())
                    throw new HttpRequestFailException(nameof(ApplicationMsg.USR0003), ApplicationMsg.USR0003, HttpStatusCode.BadRequest);

                await _unitOfWork.CourseRepository.CreateAsync(course!);
                _unitOfWork.Commit();

                CreateCourseResponseDTO? createCourseResponseDTO = course.Adapt<CreateCourseResponseDTO>();

                return createCourseResponseDTO;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _unitOfWork.Rollback();

                throw new HttpRequestFailException(nameof(ApplicationMsg.USR0001), ApplicationMsg.USR0001, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();

                //_registerLog.RegisterExceptionLog(action: "CreateUser", details: null, ex);

                throw;
            }
        }
    }
}
