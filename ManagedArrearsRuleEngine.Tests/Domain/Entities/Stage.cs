using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedArrearsRuleEngine.Tests.Domain.Entities
{
    public class Stage
    {
        public int Number { get; set; }
        public ArrearsAction Action { get; set; }
        public bool IsAutomated { get; set; }
        public bool IsTemplated { get; set; }
        public string TimingOfAction { get; set; }
        public string CompletionOfAction { get; set; }


    }
}
