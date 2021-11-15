using ManagedArrearsRuleEngine.Tests.Application.Features.Queries;
using ManagedArrearsRuleEngine.Tests.Domain.Entities;
using Newtonsoft.Json;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ManagedArrearsRuleEngine.Tests.Tests
{
    public class GetNextStageQueryHandlerTests
    {
        private List<Workflow> workflowRules;

        public GetNextStageQueryHandlerTests()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "ArrearsWorkFlow.json", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
                throw new Exception("Rules not found.");

            var fileData = File.ReadAllText(files[0]);
            workflowRules = JsonConvert.DeserializeObject<List<Workflow>>(fileData);
        }

        [Fact]
        public async void ShouldEvaluateToNextStageEquals1()
        {
            //arrange
            var sut = new GetNextStageQueryHandler(workflowRules.ToArray());


            var myCustomer = new Customer(){HasValidPhoneNumber = true};
            var myAccount = new Account() { 
                ArrearsAmount = 100m, IsFirstTimeInArrears=true, IsAccountPaused=false};
            var myCurrentStage = new Stage() { Number = 0, Action=0};

            var getNextStageQuery = new GetNextStageQuery() 
                { customer = myCustomer, arrearsAccount = myAccount, currentStage = myCurrentStage };

            //act
            var nextStage = await sut.Handle(getNextStageQuery, CancellationToken.None);

            //assert
            Assert.Equal(1, nextStage.Number);

        }



    }
}
