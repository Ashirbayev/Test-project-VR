namespace VRTraining.Scenario
{
    public class StepResult
    {
        public string GroupName { get; }
        public string StepDescription { get; }
        public StepStatus Status { get; }

        public StepResult(string groupName, string stepDescription, StepStatus status)
        {
            GroupName = groupName;
            StepDescription = stepDescription;
            Status = status;
        }
    }
}
