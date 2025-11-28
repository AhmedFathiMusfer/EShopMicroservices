using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<out TRespons> : IRequest<TRespons> where TRespons : notnull
    {

    }
}