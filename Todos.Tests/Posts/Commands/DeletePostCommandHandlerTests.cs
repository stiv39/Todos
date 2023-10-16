using Moq;
using Todos.Application.Posts.Commands.Delete;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;
using Todos.Domain.Entities;

namespace Todos.Tests.Posts.Commands;

[TestFixture]
public class DeletePostCommandHandlerTests
{
    private Mock<IPostRepository> _postRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    private DeletePostCommandHandler _sut;

    [SetUp]
    public void Setup()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new DeletePostCommandHandler(_postRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_GetsNotExistingId_ReturnsFalse()
    {
        // Arrange
        var command = new DeletePostCommand(Guid.NewGuid());

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        _postRepositoryMock.Verify(p => p.DeletePost(It.IsAny<Post>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Handle_GetsExistingId_ReturnsTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var command = new DeletePostCommand(guid);
        var post = new Post { Id = guid, Body = "body", Title = "title" };

        _postRepositoryMock.Setup(p => p.GetPostByIdAsync(guid, CancellationToken.None)).ReturnsAsync(post);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        _postRepositoryMock.Verify(p => p.DeletePost(post), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);

        Assert.That(result, Is.True);
    }
}
