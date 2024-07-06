using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using LogicLayer.Services;
using Moq;
using Xunit;

namespace ShuttleX.Tests
{
    public class ChatServiceTests
    {
        [Fact]
        public void CreateMessage_ValidInput()
        {
            // Arrange
            var userId = 1;
            var chatId = 1;
            var content = "Hello";

            var userRepositoryMock = new Mock<IUserRepository>();
            var chatRepositoryMock = new Mock<IChatRepository>();
            var messageRepositoryMock = new Mock<IMessageRepository>();

            var messageService = new MessageService(messageRepositoryMock.Object, chatRepositoryMock.Object, userRepositoryMock.Object);

            userRepositoryMock.Setup(repo => repo.FindById(userId)).Returns(new User { Id = userId });
            chatRepositoryMock.Setup(repo => repo.FindById(chatId)).Returns(new Chat { Id = chatId });

            // Act
            messageService.CreateMessage(userId, chatId, content);

            // Assert
            messageRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Message>()), Times.Once);
        }

        [Fact]
        public void CreateMessage_ChatNotExists_ThrowsException()
        {
            // Arrange
            var userId = 1;
            var chatId = 1;
            var content = "Hello";

            var userRepositoryMock = new Mock<IUserRepository>();
            var chatRepositoryMock = new Mock<IChatRepository>();
            var messageRepositoryMock = new Mock<IMessageRepository>();

            var messageService = new MessageService(messageRepositoryMock.Object, chatRepositoryMock.Object, userRepositoryMock.Object);

            chatRepositoryMock.Setup(repo => repo.FindById(chatId)).Returns((Chat)null); // Simulate chat not found

            // Act & Assert
            Assert.Throws<Exception>(() => messageService.CreateMessage(userId, chatId, content));
        }

        [Fact]
        public void GetMessagesByChat_ChatDoesNotContainUser_ThrowsException()
        {
            // Arrange
            var userId = 1;
            var chatId = 1;

            var userRepositoryMock = new Mock<IUserRepository>();
            var chatRepositoryMock = new Mock<IChatRepository>();
            var messageRepositoryMock = new Mock<IMessageRepository>();

            var messageService = new MessageService(messageRepositoryMock.Object, chatRepositoryMock.Object, userRepositoryMock.Object);

            var user = new User { Id = userId };
            var chat = new Chat { Id = chatId }; // Chat doesn't contain the user

            userRepositoryMock.Setup(repo => repo.FindById(userId)).Returns(user);
            chatRepositoryMock.Setup(repo => repo.FindById(chatId)).Returns(chat);

            // Act & Assert
            Assert.Throws<Exception>(() => messageService.GetMessagesByChat(userId, chatId));
        }
    }
}
