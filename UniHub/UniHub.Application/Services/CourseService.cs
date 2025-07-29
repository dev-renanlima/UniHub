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

        public CourseService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<CreateCourseResponseDTO> CreateAsync(CourseDTO courseDTO)
        {
            try
            {
                var user = await _userService.GetUserByExternalIdentifierAsync(courseDTO.UserIdentifier!);

                if (user.Role != UserRole.Admin.ToString())
                    throw new HttpRequestFailException(nameof(ApplicationMsg.USR0003), ApplicationMsg.USR0003, HttpStatusCode.BadRequest);

                var course = (courseDTO, user).Adapt<Course>();

                await _unitOfWork.CourseRepository.CreateAsync(course!);
                _unitOfWork.Commit();

                CreateCourseResponseDTO createCourseResponseDTO = course.Adapt<CreateCourseResponseDTO>();

                return createCourseResponseDTO;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _unitOfWork.Rollback();

                throw new HttpRequestFailException(nameof(ApplicationMsg.USR0001), ApplicationMsg.USR0001, HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        public async Task<GetCourseByCodeResponseDTO?> GetCourseByCodeAsync(string code)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByCodeAsync(code) ??
                    throw new HttpRequestFailException(nameof(ApplicationMsg.CRS0001), string.Format(ApplicationMsg.CRS0001, code), HttpStatusCode.NotFound);

                _unitOfWork.Commit();

                GetCourseByCodeResponseDTO? getCourseByCodeResponseDTO = course.Adapt<GetCourseByCodeResponseDTO>();

                return getCourseByCodeResponseDTO;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _unitOfWork.Rollback();

                throw new HttpRequestFailException(nameof(ApplicationMsg.USR0004), ApplicationMsg.USR0004, HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        public async Task<AddCourseMemberResponseDTO> AddMemberByCodeAsync(CourseMemberDTO courseMemberDTO)
        {
            try
            {
                var user = await _userService.GetUserByExternalIdentifierAsync(courseMemberDTO.ExternalIdentifier!);

                if (user.Role != UserRole.Member.ToString())
                    throw new HttpRequestFailException(nameof(ApplicationMsg.USR0003), ApplicationMsg.USR0003, HttpStatusCode.BadRequest);

                var course = await GetCourseByCodeAsync(courseMemberDTO.Code!);

                var courseMember = (course, user).Adapt<CourseMember>();

                await _unitOfWork.CourseRepository.CreateCourseMemberAsync(courseMember!);
                _unitOfWork.Commit();

                AddCourseMemberResponseDTO? addCourseMemberResponseDTO = courseMember.Adapt<AddCourseMemberResponseDTO>();

                return addCourseMemberResponseDTO;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _unitOfWork.Rollback();

                throw new HttpRequestFailException(nameof(ApplicationMsg.USR0004), ApplicationMsg.USR0004, HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        public async Task<GetCoursesByUserResponseDTO?> GetCoursesByUserAsync(string externalIdentifier)
        {
            try
            {
                var user = await _userService.GetUserByExternalIdentifierAsync(externalIdentifier!);

                var courses = await _unitOfWork.CourseRepository.GetCoursesByUserAsync(user.Id!);

                _unitOfWork.Commit();

                GetCoursesByUserResponseDTO getCoursesByUserResponseDTO = (user, courses).Adapt<GetCoursesByUserResponseDTO>();

                return getCoursesByUserResponseDTO;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }
    }
}
