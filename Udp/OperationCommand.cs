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
        public List<int> Nums { get; set; }
        public int NumsLength { get; set; }
        public bool End { get; set; }

        public OperationCommand(string operation = null, bool end = true)
        {
            this.Operation = operation;
            this.End = end;
            this.Nums = new List<int>();
            this.Nums.Clear();
        }

        public OperationCommand(List<int> nums, string operation = null, bool end = true)
        {
            this.Operation = operation;
            this.End = end;
            this.Nums = new List<int>();
            this.Nums.Clear();
            this.Nums = nums;
        }

    }
}
