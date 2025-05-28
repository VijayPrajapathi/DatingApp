using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class MessagesController(IMessageRepository messageRepository,
 IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var userName = User.GetUsername();

        if (userName == createMessageDto.RecipientUserName.ToLower()) return BadRequest("You cannot send messages to yourself.");

        var sender = await userRepository.GetUserByUsernameAsync(userName);

        var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUserName);

        if (recipient == null || sender == null || sender.UserName ==  null || recipient.UserName == null)
            return NotFound("Can not send message at this time.");

        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUserName = sender.UserName,
            RecipientUserName = recipient.UserName,
            Content = createMessageDto.Content,
        };

        messageRepository.Addmessage(message);
        if (await messageRepository.SaveAllAsync()) return Ok(mapper.Map<MessageDto>(message));

        return BadRequest("Failed to send message");
    }

    [HttpGet()]

    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
    {
        messageParams.UserName = User.GetUsername();

        var messages = await messageRepository.GetMessagesForUser(messageParams);

        Response.AddPaginationheader(messages);

        return Ok(messages);
    }

    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
    {
        var currentUserName = User.GetUsername();
        return Ok(await messageRepository.GetMessageThread(currentUserName, username));
    }

    [HttpDelete("{id}")]

    public async Task<ActionResult> DeleteMessage(int id)
    {
        var username = User.GetUsername();
        var message = await messageRepository.GetMessage(id);
        if (message == null) return BadRequest("Message not found");

        if (message.SenderUserName != username && message.RecipientUserName != username) return Forbid();

        if (message.SenderUserName == username) message.SenderDeleted = true;
        if (message.RecipientUserName == username) message.RecipientDeleted = true;

        if (message is { SenderDeleted: true, RecipientDeleted: true })
        {
            messageRepository.DeleteMessage(message);
        }


        if (await messageRepository.SaveAllAsync()) return Ok();

        return BadRequest("Failed to delete message");


    }   
}