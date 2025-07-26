using Mapster;
using Microsoft.Data.SqlClient;
using System.Net;
using UniHub.Application.Exceptions;
using UniHub.Application.Resources;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.Course;
using UniHub.Domain.Entities;
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

        public async Task<CreateCourseResponseDTO> Create(CourseDTO courseDTO)
        {
            try
            {
                var course = courseDTO.Adapt<Course>();

                await _userService.GetUserByClerkId(course.AdminId!);

                _unitOfWork.CourseRepository.Create(course!);
                _unitOfWork.Commit();

                CreateCourseResponseDTO? createCourseResponseDTO = course.Adapt<CreateCourseResponseDTO>();

                return createCourseResponseDTO;
            }
            catch (SqlException ex) when (ex.Number is 2601 or 2627)
            {
                _unitOfWork.Rollback();

                throw new HttpRequestFailException(ApplicationMsg.USR0001, HttpStatusCode.BadRequest);
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
