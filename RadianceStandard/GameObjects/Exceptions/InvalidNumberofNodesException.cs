using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadianceStandard.GameObjects.Exceptions
{
    public class InvalidNumberOfNodesException : Exception
    {
        #region ctors

        public InvalidNumberOfNodesException()
            : base("Invalid number of nodes!")
        {

        }

        public InvalidNumberOfNodesException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
