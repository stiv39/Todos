using Microsoft.EntityFrameworkCore;
using Posts.Domain.Interfaces.Repositories;
using System.Linq.Expressions;
using Todos.Domain.Entities;

namespace Todos.Infrastructure.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DatabaseContext _databaseContext;

    public PostRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddPostAsync(Post post, CancellationToken cancellationToken)
    {
        await _databaseContext.Posts.AddAsync(post, cancellationToken);
    }

    public void DeletePost(Post post)
    {
        _databaseContext.Posts.Remove(post);
    }

    public async Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _databaseContext.Posts.FirstOrDefaultAsync(post => post.Id == id, cancellationToken);
    }

    public async Task<List<Post>> GetPostsAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken)
    {
        IQueryable<Post> postsQuery = _databaseContext.Posts;

        var totalCount = postsQuery.Count();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            postsQuery = postsQuery.Where(m => m.Title.ToLower().Contains(searchTerm.ToLower()) || m.Body.ToLower().Contains(searchTerm.ToLower()));
        }

        if (sortOrder == "desc")
        {
            postsQuery = postsQuery.OrderByDescending(GetSortColumn(sortColumn));
        }
        else
        {
            postsQuery = postsQuery.OrderBy(GetSortColumn(sortColumn));
        }

        return await postsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdatePostAsync(Post post, CancellationToken cancellationToken)
    {
        var postFromDb = await _databaseContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id, cancellationToken);

        if(postFromDb != null)
        {
            postFromDb.Title = post.Title;
            postFromDb.Body = post.Body;
            return true;
        }

        return false;

    }

    private static Expression<Func<Post, object>> GetSortColumn(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "title" => todo => todo.Title,
            "body" => todo => todo.Body,
            _ => todo => todo.Id
        };
    }
}
