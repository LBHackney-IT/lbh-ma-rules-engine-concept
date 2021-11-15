using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedArrearsRuleEngine.Tests.Application.Contracts
{
    public interface IRule
    {
        string RuleName { get; set; }
        string Expression { get; set; }
    }
}
