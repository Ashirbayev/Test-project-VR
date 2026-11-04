using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using VRTraining.Scenario;

namespace VRTraining.Interactions
{
    [RequireComponent(typeof(XRBaseInteractable))]
    public class ScenarioXRInteractable : MonoBehaviour
    {
        [Header("Настройки сценария")]
        [SerializeField] private string objectTag;
        [SerializeField] private int stepId;

        private XRBaseInteractable _interactable;
        private ScenarioManager _scenarioManager;

        private void Awake()
        {
            _interactable = GetComponent<XRBaseInteractable>();
            _scenarioManager = FindFirstObjectByType<ScenarioManager>();
        }

        private void OnEnable()
        {
            // Подписываемся на события XR Interaction Toolkit
            _interactable.selectEntered.AddListener(OnObjectGrabbed);
            _interactable.activated.AddListener(OnObjectClicked);
        }

        private void OnDisable()
        {
            _interactable.selectEntered.RemoveListener(OnObjectGrabbed);
            _interactable.activated.RemoveListener(OnObjectClicked);
        }

        // Вызывается, когда игрок берет объект в руку (Grab)
        private void OnObjectGrabbed(SelectEnterEventArgs args)
        {
            if (_scenarioManager != null)
            {
                _scenarioManager.PlayerPerformedAction(StepActionType.GrabObject, objectTag, stepId);
            }
        }

        // Вызывается, когда игрок нажимает кнопку использования (Trigger на контроллере), наведя луч (Click)
        private void OnObjectClicked(ActivateEventArgs args)
        {
            if (_scenarioManager != null)
            {
                _scenarioManager.PlayerPerformedAction(StepActionType.ClickObject, objectTag, stepId);
            }
        }
    }
}
