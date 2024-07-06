using System;
using System.Net.Http;
using System.Threading.Tasks;


using Microsoft.AspNetCore.SignalR.Client;
using Tests.Tests;
using Xunit;

namespace Tests
{
    public class ChatHubTests : IClassFixture<WebApiFactory>
    {
        private readonly WebApiFactory _factory;

        public ChatHubTests(WebApiFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task sendMessage()
        {
           
         
            _factory.CreateClient(); // need to create a client for the server property to be available
            var server = _factory.Server;

            var connection = await StartConnectionAsync(server.CreateHandler(), "chatHub");

            connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            // Act
            string receivedUserId = null;
            string receivedMessage = null;
            var receivedMessageReceived = new TaskCompletionSource<bool>();

            connection.On<int, string>("ReceiveMessage", (userId, message) =>
            {
                receivedUserId = userId.ToString();
                receivedMessage = message;
                receivedMessageReceived.SetResult(true);
            });

            await connection.InvokeAsync("JoinChat", 1, 4); // Join the chat group
            await connection.InvokeAsync("SendMessage", 1, 4, "Hello World!!");

            await receivedMessageReceived.Task;

            //Assert
            Assert.Equal("4", receivedUserId);
            Assert.Equal("Hello World!!", receivedMessage);
        }

        private static async Task<HubConnection> StartConnectionAsync(HttpMessageHandler handler, string hubName)
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl($"ws://localhost/{hubName}", o =>
                {
                    o.HttpMessageHandlerFactory = _ => handler;
                })
                .Build();

            await hubConnection.StartAsync();

            return hubConnection;
        }
    }

}