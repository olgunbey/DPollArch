using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoll.Application.Common.Exceptions
{
    public class NotFoundException(string message):Exception(message)
    {
    }
}
