

using System.ComponentModel.DataAnnotations;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.User
{
    public class CreateUserViewModel
    {
        public required int Id { get; set; }

        [Required(ErrorMessage = "You must enter the name of user")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "You must enter the last name of user")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "You must enter the email of user")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "You must enter the username of user")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You must enter the password of user")]
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password must match")]
        [Required(ErrorMessage = "You must enter the confirm password")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
        public string? Phone { get; set; }
        public string? ProfileImage { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must enter the valid role of user")]
        public required int Role { get; set; }
    }
}
