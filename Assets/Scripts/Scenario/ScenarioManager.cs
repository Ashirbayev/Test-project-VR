
using UnityEngine;

public enum StepStatus
{
    NotStarted,
    InProgress,
    Done,
    Failed
}

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance { get; private set; }

    [Header("Scenario Progress")]
    [SerializeField]
    private StepStatus[] steps = new StepStatus[9];

    public int CompletedCount
    {
        get
        {
            int count = 0;

            foreach (StepStatus status in steps)
            {
                if (status == StepStatus.Done)
                    count++;
            }

            return count;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        steps = new StepStatus[9];
    }

    public void CompleteStep(int stepNumber)
    {
        int index = stepNumber - 1;

        if (index < 0 || index >= steps.Length)
            return;

        if (steps[index] == StepStatus.Done)
            return;

        steps[index] = StepStatus.Done;

        Debug.Log(
            $"Step {stepNumber} DONE | " +
            $"Progress: {CompletedCount}/9"
        );
    }

    public void FailStep(int stepNumber)
    {
        int index = stepNumber - 1;

        if (index < 0 || index >= steps.Length)
            return;

        if (steps[index] == StepStatus.Done)
            return;

        steps[index] = StepStatus.Failed;

        Debug.Log($"Step {stepNumber} FAILED");
    }

    public StepStatus GetStepStatus(int stepNumber)
    {
        int index = stepNumber - 1;

        if (index < 0 || index >= steps.Length)
            return StepStatus.NotStarted;

        return steps[index];
    }

    public void ResetScenario()
    {
        steps = new StepStatus[9];
    }
}
