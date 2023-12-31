﻿using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UserBasicModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public UserBasicModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
        }
    }
}
