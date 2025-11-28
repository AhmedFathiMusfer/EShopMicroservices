using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TRespons> : IRequestHandler<TQuery, TRespons> where TQuery : IQuery<TRespons> where TRespons : notnull
    {

    }
}