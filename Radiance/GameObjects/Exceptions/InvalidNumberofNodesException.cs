using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiance.GameObjects.Exceptions
{
    public class InvalidNumberOfNodesException : Exception
    {
        #region ctors

        public InvalidNumberOfNodesException()
        {

        }

        public InvalidNumberOfNodesException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
