using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opg5
{
    public class ForfatterException : Exception
    {
        public ForfatterException(string msg) : base(msg) { }
    }

    public class SideAntalException : Exception
    {
        public SideAntalException(string msg) : base(msg) { }
    }

    public class Isbn13Exception : Exception
    {
        public Isbn13Exception(string msg) : base(msg) { }
    }
}
