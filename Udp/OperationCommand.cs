using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udp
{
    public class OperationCommand
    {
        public string Operation = null;
        public int Num1 { get; set; } = 0;
        public int Num2 { get; set; } = 0;
        public int Num3 { get; set; } = 0;

        public OperationCommand(string operation = null, int num1 = 0, int num2 = 0, int num3 = 0)
        {
            this.Operation = operation;
            this.Num1 = num1;
            this.Num2 = num2;
            this.Num3 = num3;
        }
    }
}
