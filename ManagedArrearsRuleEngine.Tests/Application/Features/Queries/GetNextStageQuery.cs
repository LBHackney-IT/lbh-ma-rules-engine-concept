using ManagedArrearsRuleEngine.Tests.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagedArrearsRuleEngine.Tests.Application.Features.Queries
{
    public class GetNextStageQuery:IRequest<Stage>
    {
        public Customer customer { get; set; }
        public Stage currentStage { get; set; }
        public Account arrearsAccount { get; set; }

    }
}
