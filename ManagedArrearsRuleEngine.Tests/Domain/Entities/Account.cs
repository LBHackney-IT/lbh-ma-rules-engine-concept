using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedArrearsRuleEngine.Tests.Domain.Entities
{
    public class Account
    {
        public decimal ArrearsAmount { get; set; }
        public bool IsAccountPaused { get; set; }
        public bool IsFirstTimeInArrears { get; set; }
    }
}
