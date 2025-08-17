using Mapster;
using Npgsql;
using System.Net;
using UniHub.Application.Exceptions;
using UniHub.Application.Resources;
using UniHub.Domain.DTOs;
using UniHub.Domain.Entities;
using UniHub.Domain.Interfaces.Repositories;
using UniHub.Domain.Interfaces.Services;

namespace UniHub.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            try
            {
                User user = userDTO.Adapt<User>();

                await _unitOfWork.UserRepository.CreateAsync(user!);
                _unitOfWork.Commit();

                UserDTO createdUser = user.Adapt<UserDTO>();

                return createdUser;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                _unitOfWork.Rollback();

                throw new HttpRequestFailException(nameof(ApplicationMsg.USR0001), ApplicationMsg.USR0001, HttpStatusCode.Conflict);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdentifierAsync(string identifier)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetUserByIdentifierAsync(identifier)
                    ?? throw new HttpRequestFailException(nameof(ApplicationMsg.USR0002), string.Format(ApplicationMsg.USR0002, identifier), HttpStatusCode.NotFound);

                _unitOfWork.Commit();

                UserDTO userDTO = user.Adapt<UserDTO>();

                return userDTO;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid? userId)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId)
                    ?? throw new HttpRequestFailException(nameof(ApplicationMsg.USR0002), string.Format(ApplicationMsg.USR0002, userId), HttpStatusCode.NotFound);

                _unitOfWork.Commit();

                UserDTO userDTO = user.Adapt<UserDTO>();

                return userDTO;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }
    }
}
