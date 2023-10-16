using Moq;
using Todos.Application.Posts.Commands.Update;
using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;

namespace Todos.Tests.Posts.Commands;

[TestFixture]
public class UpdatePostCommandHandlerTests
{
    private Mock<IPostRepository> _postRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    private UpdatePostCommandHandler _sut;

    [SetUp]
    public void Setup()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new UpdatePostCommandHandler(_postRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_GetsNotExistingPost_ReturnsFalse()
    {
        // Arrange
        var command = new UpdatePostCommand(Guid.NewGuid(), "title", "body");

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Handle_GetsExistingPost_ReturnsTrue()
    {
        // Arrange
        var command = new UpdatePostCommand(Guid.NewGuid(), "title", "body");
        _postRepositoryMock.Setup(p => p.UpdatePostAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
       
        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);

        Assert.That(result, Is.True);
    }
}
