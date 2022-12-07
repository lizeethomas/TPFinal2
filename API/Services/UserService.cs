using TPFinalStrasbourg.Repositories;
using TPFinalStrasbourg.Services;
using TPFinalStrasbourg.DTOs;
using TPFinalStrasbourg.Models;

namespace TPFinalStrasbourg.Services

{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        private readonly RoleRepository _roleRepo;

        public UserService(UserRepository userRepo, RoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public List<UserResponseDTO> DisplayAll()
        {
            List<User> users = _userRepo.FindAll();
            List<UserResponseDTO> responseDTOs = new List<UserResponseDTO>();
            if (users != null)
            {
                users.ForEach(u =>
                {
                    responseDTOs.Add(new UserResponseDTO()
                    {
                        Name = u.Name,
                        Email = u.Email,
                        UserRole = u.Role.RoleUser,
                        Status = u.Status
                    });
                });
                return responseDTOs;
            }
            return null;
        }

        public UserResponseDTO CreateAdmin(UserRequestDTO req)
        {
            UserResponseDTO response = new UserResponseDTO();
            User user = new User
            {
                Name = req.Name,
                Email = req.Email,
                Password = req.Password,
                Status = true
            };

            Role role = _roleRepo.FindById(1);
            user.Role = role;

            if (_userRepo.Save(user))
            {
                response.Email = req.Email;
                response.Name = req.Name;
                response.UserRole = user.Role.RoleUser;
                response.Status = user.Status;

                return response;
            }

            return response;
        }

        public UserResponseDTO CreateUser(UserRequestDTO req)
        {
            UserResponseDTO response = new UserResponseDTO();
            User user = new User
            {
                Name = req.Name,
                Email = req.Email,
                Password = req.Password,
                Status = false
            };

            Role role = _roleRepo.FindById(2);
            user.Role = role;

            if (_userRepo.Save(user))
            {
                response.Email = req.Email;
                response.Name = req.Name;
                response.UserRole = user.Role.RoleUser;
                response.Status = user.Status;

                return response;
            }

            return response;
        }
    }

}