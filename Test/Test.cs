using Core.DTOs;
using Core.Interface;
using MessageChat;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test
{
    [TestFixture]
    public class ChatsControllerTests
    {
        private ChatsController _controller;
        private Mock<IChatService> _chatServiceMock;

        [SetUp]
        public void Setup()
        {
            _chatServiceMock = new Mock<IChatService>();
            _controller = new ChatsController(_chatServiceMock.Object);
        }

        [Test]
        public async Task CreateChat_ValidInput_ReturnsCreatedAtAction()
        {
            var chatDto = new ChatDto { };
            int userId = 1;
            var expectedChat = new ChatDto { Id = 1 };
            _chatServiceMock.Setup(x => x.AddChatAsync(chatDto, userId)).ReturnsAsync(expectedChat);
            var result = await _controller.CreateChat(chatDto, userId);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual("GetChatById", createdAtActionResult.ActionName);
            Assert.AreEqual(expectedChat.Id, ((ChatDto)createdAtActionResult.Value).Id);
        }

        [Test]
        public async Task GetChats_ReturnsOkResultWithData()
        {
            var expectedChats = new List<ChatDto> { };
            _chatServiceMock.Setup(x => x.GetAllChatsAsync()).ReturnsAsync(expectedChats);

            // Act
            var result = await _controller.GetChats();
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(expectedChats, okObjectResult.Value);
        }

        [Test]
        public async Task GetChatById_ExistingId_ReturnsOkResultWithChat()
        {
            int chatId = 1;
            var expectedChat = new ChatDto { Id = chatId };
            _chatServiceMock.Setup(x => x.GetChatByIdAsync(chatId)).ReturnsAsync(expectedChat);
            var result = await _controller.GetChatById(chatId);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(expectedChat, okObjectResult.Value);
        }

        [Test]
        public async Task UpdateChat_ExistingId_ReturnsOkResultWithUpdatedChat()
        {
            int chatId = 1;
            var updatedChatDto = new ChatDto { Id = chatId };
            var expectedUpdatedChat = new ChatDto { Id = chatId };
            _chatServiceMock.Setup(x => x.UpdateChatAsync(chatId, updatedChatDto)).ReturnsAsync(expectedUpdatedChat);
            var result = await _controller.UpdateChat(chatId, updatedChatDto);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.AreEqual(expectedUpdatedChat, okObjectResult.Value);
        }

        [Test]
        public async Task DeleteChat_ExistingId_ReturnsNoContentResult()
        {
            int chatId = 1;
            int userId = 1;
            _chatServiceMock.Setup(x => x.DeleteChatAsync(chatId, userId)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteChat(chatId, userId);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task JoinChat_ExistingId_ReturnsOkResult()
        {
            int chatId = 1;
            int userId = 1;
            _chatServiceMock.Setup(x => x.JoinChatAsync(chatId, userId)).Returns(Task.CompletedTask);
            var result = await _controller.JoinChat(chatId, userId);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}