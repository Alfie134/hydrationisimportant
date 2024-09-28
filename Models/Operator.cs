using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Operator
    {
        public int OperatorId { get; set; }
        public string Name { get; set; }

        //Parameterløs 
        public Operator()
        {
        }

        public Operator(int operatorId, string name)
        {
            OperatorId = operatorId;
            Name = name;
        }
    }
}
