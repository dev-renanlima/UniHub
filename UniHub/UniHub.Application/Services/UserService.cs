using Mapster;
using Npgsql;
using System.Net;
using UniHub.Application.Exceptions;
using UniHub.Application.Resources;
using UniHub.Domain.DTOs;
using UniHub.Domain.DTOs.Responses.User;
using UniHub.Domain.Entities;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IRegisterLog _registerLog;

        public UserService(IUnitOfWork unitOfWork)//, IRegisterLog registerLog)
        {
            _unitOfWork = unitOfWork;
            //_registerLog = registerLog;
        }

        public async Task<CreateUserResponseDTO> CreateAsync(UserDTO userDTO)
        {
            try
            {
                User user = userDTO.Adapt<User>();

                await _unitOfWork.UserRepository.CreateAsync(user!);
                _unitOfWork.Commit();

                CreateUserResponseDTO? createUserResponseDTO = user.Adapt<CreateUserResponseDTO>();

                return createUserResponseDTO;
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

        public async Task<GetUserResponseDTO> GetUserByClerkIdAsync(string clerkId)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetUserByClerkIdAsync(clerkId)
                    ?? throw new HttpRequestFailException(nameof(ApplicationMsg.USR0002), string.Format(ApplicationMsg.USR0002, clerkId), HttpStatusCode.NotFound);

                _unitOfWork.Commit();

                GetUserResponseDTO? getUserResponseDTO = user.Adapt<GetUserResponseDTO>();

                return getUserResponseDTO;
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
