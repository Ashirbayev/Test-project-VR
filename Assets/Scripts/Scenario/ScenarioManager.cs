using System;
using System.Collections.Generic;
using UnityEngine;


namespace VRTraining.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] private List<StepGroupData> stepGroups;

        private int _currentGroupIndex = 0;
        private int _currentStepIndex = 0;

 
        private List<StepResult> _results = new List<StepResult>();

        
        public static event System.Action<StepGroupData, StepData> OnStepActivated;
        public static event System.Action OnActionSuccess;
        public static event System.Action OnActionWrong; 
        public static event System.Action OnSequenceViolation;
        public static event System.Action<List<StepResult>> OnScenarioEnded;

        private void Start()
        {
            StartScenario();
        }

        public void StartScenario()
        {
            _currentGroupIndex = 0;
            _currentStepIndex = 0;
            _results.Clear();

            if (stepGroups != null && stepGroups.Count > 0)
            {
                ActivateCurrentStep();
            }
            else
            {
                Debug.LogError("Список групп шагов пуст!");
            }
        }

  
        public void PlayerPerformedAction(StepActionType actionType, string targetTag, int interactedStepId = -1)
        {
            if (_currentGroupIndex >= stepGroups.Count) return;

            StepGroupData currentGroup = stepGroups[_currentGroupIndex];
            StepData expectedStep = currentGroup.Steps[_currentStepIndex];

         
            if (interactedStepId != -1 && interactedStepId != expectedStep.Id)
            {
                HandleSequenceViolation(currentGroup);
                return;
            }

    
            if (actionType == expectedStep.ExpectedAction && targetTag == expectedStep.TargetTag)
            {
                HandleStepSuccess(currentGroup, expectedStep);
            }
            else
            {
                HandleStepFailure();
            }
        }

        private void ActivateCurrentStep()
        {
            if (_currentGroupIndex < stepGroups.Count)
            {
                var group = stepGroups[_currentGroupIndex];
                if (_currentStepIndex < group.Steps.Count)
                {
                    OnStepActivated?.Invoke(group, group.Steps[_currentStepIndex]);
                    return;
                }
            }
            EndScenario();
        }

        private void HandleStepSuccess(StepGroupData group, StepData step)
        {
            _results.Add(new StepResult(group.GroupName, step.Description, StepStatus.Success));
            OnActionSuccess?.Invoke();

            AdvanceStep();
        }

        private void HandleStepFailure()
        {
            OnActionWrong?.Invoke();
        }

        private void HandleSequenceViolation(StepGroupData group)
        {
            OnSequenceViolation?.Invoke();

            for (int i = _currentStepIndex; i < group.Steps.Count; i++)
            {
                _results.Add(new StepResult(group.GroupName, group.Steps[i].Description, StepStatus.Skipped));
            }

            _currentGroupIndex++;
            _currentStepIndex = 0;
            ActivateCurrentStep();
        }

        private void AdvanceStep()
        {
            _currentStepIndex++;
            StepGroupData currentGroup = stepGroups[_currentGroupIndex];

            if (_currentStepIndex >= currentGroup.Steps.Count)
            {
                _currentGroupIndex++;
                _currentStepIndex = 0;
            }

            ActivateCurrentStep();
        }

        private void EndScenario()
        {
            OnScenarioEnded?.Invoke(_results);
        }
    }
}
