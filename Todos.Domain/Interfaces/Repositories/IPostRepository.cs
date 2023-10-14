using Todos.Domain.Entities;

namespace Todos.Domain.Interfaces.Repositories;

public interface IPostRepository
{
    Task<List<Post>> GetPostsAsync(
                    string? searchTerm,
                    string? sortColumn,
                    string? sortOrder,
                    int page,
                    int pageSize,
                    CancellationToken cancellationToken);
    Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddPostAsync(Post post, CancellationToken cancellationToken);
    Task<bool> UpdatePostAsync(Post post, CancellationToken cancellationToken);
    void DeletePost(Post post);
}
