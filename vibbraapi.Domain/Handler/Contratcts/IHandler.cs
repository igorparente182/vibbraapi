using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vibbraapi.Domain.Commands.Contracts;

namespace vibbraapi.Domain.Handler.Contratcts
{
    public interface IHandler<T> where T:ICommand
    {
        ICommandResult Handle(T command);
    }
}
