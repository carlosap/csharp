﻿using Twilio;
namespace WebApi.Interfaces.Communications
{
    public interface IServerNotification
    {
        Message SendMessage(string body);
    }
}

