
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [Header("Vehicle")]
    public Car carController;

    [Header("Results")]
    public ResultsUI resultsUI;

    private bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (finished)
            return;

        
        Car car = other.GetComponentInParent<Car>();

        if (car != null && car == carController)
        {
            if (!car.isDriving)
                return;

            finished = true;

            Debug.Log("FINISH: Player arrived by car!");

            if (ScenarioManager.Instance != null)
                ScenarioManager.Instance.CompleteStep(9);

            ShowResults();
            return;
        }

       
        CharacterController player =
            other.GetComponentInParent<CharacterController>();

        if (player != null &&
            carController != null &&
            !carController.isDriving)
        {
            finished = true;

            Debug.Log("FINISH: Player arrived on foot!");

            ShowResults();
        }
    }


    private void ShowResults()
    {
        Debug.Log("FINISH: ShowResults called!");

        if (resultsUI == null)
        {
            Debug.LogError("FINISH: ResultsUI is NOT assigned!");
            return;
        }

        Debug.Log("FINISH: Opening ResultsCanvas");

        resultsUI.ShowResults();
    }


}
