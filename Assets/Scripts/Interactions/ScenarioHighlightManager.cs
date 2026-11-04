using UnityEngine;
using VRTraining.Scenario;

namespace VRTraining.Interactions
{
    public class ScenarioHighlightManager : MonoBehaviour
    {
        [Header("Настройки подсветки")]
        [SerializeField] private Material outlineMaterial; // Материал светящегося контура

        private GameObject _currentHighlightedObject;
        private Material[] _originalMaterials;
        private Renderer _currentRenderer;

        private void OnEnable()
        {
            ScenarioManager.OnStepActivated += HandleStepActivated;
            ScenarioManager.OnActionSuccess += ClearHighlight;
            ScenarioManager.OnSequenceViolation += ClearHighlight;
        }

        private void OnDisable()
        {
            ScenarioManager.OnStepActivated -= HandleStepActivated;
            ScenarioManager.OnActionSuccess -= ClearHighlight;
            ScenarioManager.OnSequenceViolation -= ClearHighlight;
        }

        private void HandleStepActivated(StepGroupData group, StepData step)
        {
            // Сначала убираем старую подсветку, если она была
            ClearHighlight();

            // Ищем объект на сцене по тегу, указанному в шаге сценария
            GameObject targetObject = GameObject.FindWithTag(step.TargetTag);

            if (targetObject != null)
            {
                HighlightObject(targetObject);
            }
        }

        private void HighlightObject(GameObject obj)
        {
            _currentHighlightedObject = obj;
            _currentRenderer = obj.GetComponent<Renderer>();

            if (_currentRenderer != null)
            {
                // Сохраняем оригинальные материалы
                _originalMaterials = _currentRenderer.sharedMaterials;

                // Создаем новый массив материалов, добавляя материал контура вторым слоем
                Material[] newMaterials = new Material[_originalMaterials.Length + 1];
                for (int i = 0; i < _originalMaterials.Length; i++)
                {
                    newMaterials[i] = _originalMaterials[i];
                }
                newMaterials[newMaterials.Length - 1] = outlineMaterial;

                _currentRenderer.materials = newMaterials;
            }
        }

        private void ClearHighlight()
        {
            if (_currentHighlightedObject != null && _currentRenderer != null && _originalMaterials != null)
            {
                // Возвращаем исходные материалы
                _currentRenderer.materials = _originalMaterials;
            }

            _currentHighlightedObject = null;
            _currentRenderer = null;
            _originalMaterials = null;
        }
    }
}
