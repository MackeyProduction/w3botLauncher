using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3botLauncher.ExceptionMessage
{
    public class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException()
        {
        }

        public InvalidFileTypeException(string fileType) : base(String.Format("The file type by the name {0} could not be found.", fileType))
        {
        }
    }
}
