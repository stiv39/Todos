namespace Todos.Contracts.Users;

public sealed record RegisterUserRequest(string Name, string Email, string Password);
