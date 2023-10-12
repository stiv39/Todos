using Posts.Domain.Interfaces.Repositories;
using Todos.Domain.Entities;

namespace Todos.Infrastructure.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    public Task<Guid> AddPostAsync(Post Post, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void DeletePost(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetPostByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Post>> GetPostsAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Post>> GetPostsByIdsAsync(List<Guid> PostIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Post?> UpdatePostAsync(Post Post, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
