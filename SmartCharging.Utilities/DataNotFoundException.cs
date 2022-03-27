using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCharging.Utilities
{
    [Serializable]
  public  class DataNotFoundException:Exception
    {
        public DataNotFoundException() { }
        public DataNotFoundException(string message) : base(message)
        {

        }

    }
}
