﻿using System.Text.Json;
using Domain.Models;

namespace Service.Integration.Models;

public record Session
{
    public Guid Id { get; set; }
    
    public string Status { get; set; }
    
    public JsonElement Configuration { get; set; }
}