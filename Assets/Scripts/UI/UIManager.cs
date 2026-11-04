using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTraining.Scenario;

namespace VRTraining.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Экраны (Canvas / Panels)")]
        [SerializeField] private GameObject lobbyPanel;
        [SerializeField] private GameObject resultsPanel;

        [Header("Результаты")]
        [SerializeField] private Transform resultsContainer; // Сюда будут спавниться строки
        [SerializeField] private GameObject resultRowPrefab;  // Прераб UIResultRow

        [Header("Кнопки")]
        [SerializeField] private Button buttonStartTraining;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonGoToLobby;

        [Header("Имена сцен (для переключения)")]
        [SerializeField] private string lobbySceneName = "LobbyScene";
        [SerializeField] private string trainingSceneName = "TrainingScene";

        private void OnEnable()
        {
            // Подписываемся на окончание сценария
            ScenarioManager.OnScenarioEnded += ShowResults;

            // Настраиваем слушатели кнопок (чистый C#, без хардкода в инспекторе)
            if (buttonStartTraining != null) buttonStartTraining.onClick.AddListener(StartTraining);
            if (buttonRestart != null) buttonRestart.onClick.AddListener(RestartTraining);
            if (buttonGoToLobby != null) buttonGoToLobby.onClick.AddListener(GoToLobby);
        }

        private void OnDisable()
        {
            ScenarioManager.OnScenarioEnded -= ShowResults;

            if (buttonStartTraining != null) buttonStartTraining.onClick.RemoveListener(StartTraining);
            if (buttonRestart != null) buttonRestart.onClick.RemoveListener(RestartTraining);
            if (buttonGoToLobby != null) buttonGoToLobby.onClick.RemoveListener(GoToLobby);
        }

        private void Start()
        {
            // Определяем, в какой сцене мы находимся, и включаем нужный экран
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == lobbySceneName)
            {
                if (lobbyPanel != null) lobbyPanel.SetActive(true);
                if (resultsPanel != null) resultsPanel.SetActive(false);
            }
            else if (currentScene == trainingSceneName)
            {
                if (lobbyPanel != null) lobbyPanel.SetActive(false);
                if (resultsPanel != null) resultsPanel.SetActive(false);
            }
        }

        private void StartTraining()
        {
            SceneManager.LoadScene(trainingSceneName);
        }

        private void RestartTraining()
        {
            // Перезагружаем текущую сцену тренировки
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void GoToLobby()
        {
            SceneManager.LoadScene(lobbySceneName);
        }

        private void ShowResults(List<StepResult> results)
        {
            if (resultsPanel == null) return;

            resultsPanel.SetActive(true);

            // Очищаем старые результаты, если они были
            foreach (Transform child in resultsContainer)
            {
                Destroy(child.gameObject);
            }

            // Генерируем новые строки результатов
            foreach (var result in results)
            {
                GameObject rowInstance = Instantiate(resultRowPrefab, resultsContainer);
                UIResultRow rowScript = rowInstance.GetComponent<UIResultRow>();
                if (rowScript != null)
                {
                    rowScript.Initialize(result);
                }
            }
        }
    }
}
