using System;
using System.Collections.Generic;
using System.Text;

namespace BeehiveManagementSystem
{
    internal interface IWorker
    {
        public string Job { get; }

        public bool WorkTheNextShift();
    }
}
