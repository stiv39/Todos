using Moq;
using Todos.Application.Posts.Commands.Create;
using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;

namespace Todos.Tests.Posts.Commands;


[TestFixture]
public class CreatePostCommandHandlerTests
{
    private Mock<IPostRepository> _postRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    private CreatePostCommandHandler _sut;

    [SetUp]
    public void Setup()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new CreatePostCommandHandler(_postRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_GetsPost_ReturnsSuccess()
    {
        // Arrange
        var command = new CreatePostCommand("title", "body");

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        _postRepositoryMock.Verify(p => p.AddPostAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.InstanceOf<Guid>());
        });
    }

    [Test]
    public async Task Handle_ThrowsException_ReturnsFailure()
    {
        // Arrange
        var command = new CreatePostCommand("title", "body");
        _postRepositoryMock
            .Setup(p => p.AddPostAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("msg"));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo("err"));
            Assert.That(result.Error.Message, Is.EqualTo("msg"));
        });
    }
}
