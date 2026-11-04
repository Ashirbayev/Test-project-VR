using UnityEngine;

namespace VRTraining.Scenario
{
    [System.Serializable]
    public class StepData
    {
        [SerializeField] private int id;
        [SerializeField] private string description;
        [SerializeField] private StepActionType expectedAction;
        [SerializeField] private string targetTag; // Используем тег или ID для идентификации цели без жесткой связи на GameObject

        public int Id => id;
        public string Description => description;
        public StepActionType ExpectedAction => expectedAction;
        public string TargetTag => targetTag;
    }
}