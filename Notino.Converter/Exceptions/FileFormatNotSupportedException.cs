using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Converter.Exceptions
{
    public class FileFormatNotSupportedException : Exception
    {
        public FileFormatNotSupportedException(string message)
            : base(message)
        {

        }
    }
}
