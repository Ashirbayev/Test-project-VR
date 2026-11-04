using UnityEngine;
using VRTraining.Scenario;

namespace VRTraining.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class ScenarioTriggerZone : MonoBehaviour
    {
        [Header("Настройки сценария")]
        [SerializeField] private string zoneTag; // Должен совпадать с TargetTag в StepData
        [SerializeField] private int stepId;     // ID шага для проверки последовательности

        [Header("Настройки триггера")]
        [SerializeField] private string playerTag = "Player"; // Тег XR-рига игрока

        private ScenarioManager _scenarioManager;

        private void Awake()
        {
            // Находим менеджер на сцене (так как DI запрещен, используем классический поиск)
            _scenarioManager = FindFirstObjectByType<ScenarioManager>();

            if (_scenarioManager == null)
            {
                Debug.LogError($"[{gameObject.name}] ScenarioManager не найден на сцене!");
            }

            // Гарантируем, что коллайдер настроен как триггер
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, что вошел именно игрок (или его камера/контроллер)
            if (other.CompareTag(playerTag) || other.GetComponentInParent<CharacterController>() != null)
            {
                ReportAction();
            }
        }

        private void ReportAction()
        {
            if (_scenarioManager != null)
            {
                // Передаем тип действия, тег зоны и ID шага для проверки на нарушение порядка
                _scenarioManager.PlayerPerformedAction(StepActionType.WalkToZone, zoneTag, stepId);
            }
        }
    }
}
