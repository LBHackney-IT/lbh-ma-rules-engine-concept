# Using Rules Engine for managed arrears conditions

This is simple proof of concept test using the [Microsoft RulesEngine](https://microsoft.github.io/RulesEngine/)

Rules engine is made of three parts:

**Engine Part** - processes the rules and applies them to produce a result

**Rules Collection** - multiple parts that describe the condition, are usually grouped together

**System Input**  - information that needs to be processed e.g. customer/context discount

### Rule Collection is the key component:
- Keep individual rules simple
- Allow for complexity through combinations of simple rules
- Decide how rules will combine or be chosen


### Basic Structure of the App:

Entities folder represents objects to be evaluated against rule.
Features folder with CQRS pattern

Rules were created with [Rule Engine Editor](https://alexreich.github.io/RulesEngineEditor/)

### Apply the Rules Engine

This has been done in the unit test project:

- Initalize test input for request
- Create workflow rules, contractor injection or via repos/files into the handler
- Process to see which rule was successful, the success event name could be the action to do.
- If none of the rules were successfully applied, the original customer stage is passed back from the handler.

Given a set of inputs, this is passed into the request from the unit test
```csharp
var myCustomer = new Customer(){HasValidPhoneNumber = true};
var myAccount = new Account() { 
    ArrearsAmount = 100m, IsFirstTimeInArrears=true, IsAccountPaused=false};
var myCurrentStage = new Stage() { Number = 0, Action=0};

var getNextStageQuery = new GetNextStageQuery() 
    { customer = myCustomer, arrearsAccount = myAccount, currentStage = myCurrentStage };
```

Load the rules into the engine into handler.  In this test project, this is the ```ArrearsWorkFlow.json``` file downloaded from the 
[Rule Engine Editor](https://alexreich.github.io/RulesEngineEditor/).  
In the rules engine they are referred to as workflows.
```csharp
var re = new RulesEngine.RulesEngine(workflowRules, null);
```

Apply the rules by loading inputs and do something as here we get the next stage 
```csharp
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
```
### Outside of Concept but worth exploring

Loggers and rule store for the input message.
![](https://github.com/microsoft/RulesEngine/blob/main/assets/BlockDiagram.png)


### Some thoughts on going forward with concept

1. I've combined multiple paramenter into one rule.  These could be nested inside of each other 
and combined.  Nesting would make for easier maintenance of individual parameter rule.

2. Version could be done by enabling or disabling a rule.  Additionally a start and end date for rules could be
included.  These could be included in the expression body as part of the rules evaluation.

3. The rules can create/edited with the [Rule Engine Editor](https://alexreich.github.io/RulesEngineEditor/).  These exported rules
could be store in the database.  There is an Entity Framework example in 
the [demo](https://github.com/microsoft/RulesEngine/tree/main/demo/DemoApp.EFDataExample) documentation.

4. The [Rule Engine Editor](https://alexreich.github.io/RulesEngineEditor/) does for input parameters to be provided
and to see [whether rules are successful or rule has failed](https://alexreich.github.io/RulesEngineEditor/demo).


4. It's also possible to abstract away the onSucess method to map the object from the database.  
This way actions can also be changed without touching the code. \
Not sure what value this point would provide as will add maintenance for both code and repo.



