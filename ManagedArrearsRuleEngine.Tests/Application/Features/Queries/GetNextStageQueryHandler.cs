using ManagedArrearsRuleEngine.Tests.Application.Contracts;
using ManagedArrearsRuleEngine.Tests.Domain.Entities;
using MediatR;
using RulesEngine.Extensions;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagedArrearsRuleEngine.Tests.Application.Features.Queries
{
    public class GetNextStageQueryHandler : IRequestHandler<GetNextStageQuery, Stage>
    {
        private readonly Workflow[] workflowRules;

        public GetNextStageQueryHandler(Workflow[] workflowRules)
        {
            this.workflowRules = workflowRules;
        }

        public async Task<Stage> Handle(GetNextStageQuery request, CancellationToken cancellationToken)
        {
            var re = new RulesEngine.RulesEngine(workflowRules, null);

            var calcStage = request.currentStage;

            var ruleParameters = new RuleParameter[]
            {
                new RuleParameter("account", request.arrearsAccount),
                new RuleParameter("customer", request.customer),
                new RuleParameter("stage", calcStage)
            };            

            List<RuleResultTree> resultList 
                = await re.ExecuteAllRulesAsync("ArrearsWorkFlow", ruleParameters);
            
            resultList.OnSuccess((eventname) => 
            { calcStage = new Stage() { Number = 1, Action = ArrearsAction.EarlySmsWarning }; });
            
            return calcStage;

        }
    }
}
