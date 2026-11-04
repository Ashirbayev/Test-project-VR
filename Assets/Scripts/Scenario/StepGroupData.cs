using System.Collections.Generic;
using UnityEngine;

namespace VRTraining.Scenario
{
    [CreateAssetMenu(fileName = "NewStepGroup", menuName = "Training/Step Group")]
    public class StepGroupData : ScriptableObject
    {
        [SerializeField] private string groupName;
        [SerializeField] private List<StepData> steps = new List<StepData>();

        public string GroupName => groupName;
        public IReadOnlyList<StepData> Steps => steps;
    }
}