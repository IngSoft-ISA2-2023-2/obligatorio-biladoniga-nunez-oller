﻿using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class RoleModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public RoleModelResponse(Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}
