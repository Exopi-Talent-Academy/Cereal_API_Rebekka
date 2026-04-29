namespace Cereal_API.Models;

/// <summary>
/// A user data transfer object (DTO) that represents a user in the system.
/// </summary>
/// <param name="Id">The unique identifier of the user</param>
/// <param name="Username">The username of the user</param>
/// <param name="Password">The password of the user</param>
public record UserDTO(Guid Id, string Username, string Password);
// This is temporary, don't want to expose the password in this way when it comes to the real thing