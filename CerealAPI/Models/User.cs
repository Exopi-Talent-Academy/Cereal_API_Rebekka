namespace Cereal_API.Models;

/// <summary>
/// A user's data in the system.
/// </summary>
/// <param name="Id">The unique identifier of the user</param>
/// <param name="Username">The username of the user</param>
/// <param name="Password">The password of the user</param>
public record User(Guid Id, string Username, string Password);
