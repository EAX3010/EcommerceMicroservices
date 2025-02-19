#region

global using Microsoft.Extensions.DependencyInjection;
global using Ordering.Application.Data;
global using Ordering.Application.Dtos;
global using Ordering.Application.Mappers;
global using Ordering.Domain.Models;
global using Ordering.Domain.ValueObjects;
global using Shared.CQRS;
global using System;
global using System.Reflection;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using Ordering.Domain.Events;
global using Microsoft.EntityFrameworkCore;

#endregion