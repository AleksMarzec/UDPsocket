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
        public int NumsLength;

        public OperationCommand(string operation = null)
        {
            this.Operation = operation;
            this.Nums = new List<int>();
            this.Nums.Clear();
        }

        public OperationCommand(List<int> nums, string operation = null, bool end = false)
        {
            this.Operation = operation;
            this.Nums = new List<int>();
            this.Nums.Clear();
            this.Nums = nums;
        }

    }
}
