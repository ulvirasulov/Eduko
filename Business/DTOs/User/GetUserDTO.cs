using Business.DTOs.Common;
using Core.Entities.Enums;

namespace Business.DTOs.User;

public class GetUserDTO : BaseIdDTO
{
    public string Name { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Bio { get; set; }
    public UserType UserType { get; set; }
}