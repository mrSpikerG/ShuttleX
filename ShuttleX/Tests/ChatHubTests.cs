using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace ShuttleX.Tests
{
    public class ChatHubTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ChatHubTests()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Program>(); // Используем Program.cs в качестве стартового класса

            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task SendMessage_ThroughChatHub_MessageReceived()
        {
            // Arrange
            var userId = 1; // Замените на существующий ID пользователя в вашей тестовой базе данных
            var chatId = 1; // Замените на существующий ID чата в вашей тестовой базе данных
            var messageContent = "Hello, integration test message";

            var hubConnection = new HubConnectionBuilder()
                .WithUrl(new UriBuilder(_client.BaseAddress) { Path = "/chatHub" }.Uri)
                .Build();

            await hubConnection.StartAsync();

            var receiveMessageTaskCompletionSource = new TaskCompletionSource<(int, string)>();
            hubConnection.On<int, string>("ReceiveMessage", (uId, msgContent) =>
            {
                receiveMessageTaskCompletionSource.SetResult((uId, msgContent));
            });

            // Act
            await hubConnection.InvokeAsync("SendMessage", chatId, userId, messageContent);

            // Assert
            var receivedMessage = await receiveMessageTaskCompletionSource.Task;
            Assert.Equal(userId, receivedMessage.Item1);
            Assert.Equal(messageContent, receivedMessage.Item2);

            // Clean up
            await hubConnection.StopAsync();
        }


        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
