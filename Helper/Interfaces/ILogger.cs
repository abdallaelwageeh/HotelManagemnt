using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Interfaces
{
    public interface ILogger
    {
        void LogErrorToUser(Exception ex);
    }
}
