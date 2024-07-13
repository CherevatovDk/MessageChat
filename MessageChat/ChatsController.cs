using Core.DTOs;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace MessageChat
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<ChatDto>> CreateChat([FromBody] ChatDto chatDto, [FromQuery] int userId)
        {
            try
            {
                var chat = await _chatService.AddChatAsync(chatDto, userId);
                return CreatedAtAction(nameof(GetChatById), new { id = chat.Id }, chat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetChats()
        {
            try
            {
                var chats = await _chatService.GetAllChatsAsync();
                return Ok(chats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatDto>> GetChatById(int id)
        {
            try
            {
                var chat = await _chatService.GetChatByIdAsync(id);
                if (chat == null)
                {
                    return NotFound();
                }

                return Ok(chat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChat(int id, [FromBody] ChatDto chatDto)
        {
            try
            {
                var updatedChat = await _chatService.UpdateChatAsync(id, chatDto);
                if (updatedChat == null)
                {
                    return NotFound();
                }

                return Ok(updatedChat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id, [FromQuery] int userId)
        {
            try
            {
                await _chatService.DeleteChatAsync(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinChat(int id, [FromQuery] int userId)
        {
            try
            {
                await _chatService.JoinChatAsync(id, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}