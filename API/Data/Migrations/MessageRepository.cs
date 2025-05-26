using System;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Migrations;

public class MessageRepository(DataContext context, IMapper mapper) : IMessageRepository
{
    public void Addmessage(Message message)
    {
        context.Messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        context.Messages.Remove(message);
    }

    public async Task<Message?> GetMessage(int id)
    {
        return await context.Messages.FindAsync(id);
    }

    public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
    {
        var query = context.Messages
        .OrderByDescending(m => m.MessageSentDate)
        .AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(i => i.Recipient.UserName == messageParams.UserName && i.RecipientDeleted == false),
            "Outbox" => query.Where(o => o.Sender.UserName == messageParams.UserName && o.SenderDeleted == false),
            _ => query.Where(_ => _.Recipient.UserName == messageParams.UserName && _.DateRead == null && _.RecipientDeleted == false)
        };

        var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

        return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
    {
        var messages = await context.Messages
              .Include(x => x.Sender).ThenInclude(x => x.Photos)
              .Include(x => x.Recipient).ThenInclude(x => x.Photos)
              .Where(x =>
               x.RecipientUserName == currentUserName
               && x.RecipientDeleted == false
               && x.SenderUserName == recipientUserName ||
               x.SenderUserName == currentUserName
               && x.SenderDeleted == false
               && x.RecipientUserName == recipientUserName)
               .OrderBy(x => x.MessageSentDate)
               .ToListAsync();

        var unreadMessages = messages.Where(x => x.DateRead == null && x.RecipientUserName == currentUserName).ToList();

        if (unreadMessages.Count != 0)
        {
            unreadMessages.ForEach(x => x.DateRead = DateTime.UtcNow);
        }

        await context.SaveChangesAsync();

        return mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
