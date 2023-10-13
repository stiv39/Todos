namespace Todos.Contracts;

public record UpdatePostRequest(Guid Id, string Title, string Body);
