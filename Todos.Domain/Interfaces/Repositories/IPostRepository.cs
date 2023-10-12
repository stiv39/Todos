using Todos.Domain.Entities;

namespace Posts.Domain.Interfaces.Repositories;

public interface IPostRepository
{
    Task<List<Post>> GetPostsAsync(
                    string? searchTerm,
                    string? sortColumn,
                    string? sortOrder,
                    int page,
                    int pageSize,
                    CancellationToken cancellationToken);
    Task<List<Post>> GetPostsByIdsAsync(List<Guid> PostIds, CancellationToken cancellationToken);
    Task<Post> GetPostByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> AddPostAsync(Post Post, CancellationToken cancellationToken);
    Task<Post?> UpdatePostAsync(Post Post, CancellationToken cancellationToken);
    void DeletePost(Guid id, CancellationToken cancellationToken);
}
