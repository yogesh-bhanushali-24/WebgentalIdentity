﻿namespace WebgentalIdentity.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}