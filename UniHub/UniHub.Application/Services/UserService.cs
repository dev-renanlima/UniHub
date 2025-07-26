using Mapster;
using Microsoft.Data.SqlClient;
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

        public async Task<CreateUserResponseDTO> CreateUser(UserDTO userDTO)
        {
            try
            {
                User user = userDTO.Adapt<User>();

                await _unitOfWork.UserRepository.CreateAsync(user!);
                _unitOfWork.Commit();

                CreateUserResponseDTO? createUserResponseDTO = user.Adapt<CreateUserResponseDTO>();

                return createUserResponseDTO;
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
