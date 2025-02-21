﻿#region

using Shared.Exceptions;

#endregion

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"Order with Id {id} was not found")
    {
    }
}