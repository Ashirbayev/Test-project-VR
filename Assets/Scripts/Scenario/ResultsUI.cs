
using UnityEngine;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    [SerializeField] private GameObject resultsCanvas;
    [SerializeField] private TMP_Text resultText;

    private void Awake()
    {
        if (resultsCanvas != null)
        {
            resultsCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("ResultsCanvas is not assigned!");
        }


    }


    public void ShowResults()
    {
        if (resultsCanvas == null || resultText == null)
        {
            Debug.LogError("ResultsUI references are missing!");
            return;
        }

        if (ScenarioManager.Instance == null)
        {
            Debug.LogError("RESULTS: ScenarioManager NOT FOUND!");
            return;
        }

        int completed = ScenarioManager.Instance.CompletedCount;

        Debug.Log("RESULTS: CompletedCount = " + completed);

        for (int i = 1; i <= 9; i++)
        {
            Debug.Log(
                "RESULTS: Step " + i + " = " +
                ScenarioManager.Instance.GetStepStatus(i)
            );
        }

        resultText.text = $"Result: {completed} 88888/ 9";
        resultsCanvas.SetActive(true);

    }

}
