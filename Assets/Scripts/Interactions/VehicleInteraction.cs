
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class VehicleInteraction : MonoBehaviour
{
    [Header("Player")]
    public Transform playerCamera;

    [Header("Interaction")]
    public float interactionDistance = 1f;

    [Header("UI")]
    public TextMeshProUGUI interactionText;




    [Header("VR Input")]
    public InputActionProperty enterVehicleAction;

    [Header("Player Physics")]
    public CharacterController playerCharacterController;

    [Header("Vehicle")]
    public Collider carCollider;


    [Header("Vehicle Seat")]
    public Transform driverSeat;
    public Transform playerRig;
    public Car carController;

    private bool isInsideVehicle = false;


    [Header("Exit")]
    public Transform exitPoint;

    private void Update()
    {
        if (playerCamera == null)
            return;

        if (carCollider == null)
            return;

        Vector3 closestPoint = carCollider.ClosestPoint(
            playerCamera.position
        );

        float distance = Vector3.Distance(
            playerCamera.position,
            closestPoint
        );

        bool canEnter = distance <= interactionDistance;

        Debug.Log(
    "Distance to car: " + distance.ToString("F2") +
    " | Can enter: " + canEnter
);

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(canEnter);

            if (canEnter)
            {
                interactionText.text = "Press E to enter the vehicle";
            }
        }


        bool triggerPressed =
      enterVehicleAction.action != null &&
      enterVehicleAction.action.WasPressedThisFrame();

        if (triggerPressed)
        {
            Debug.Log(
                "TRIGGER PRESSED | " +
                "Distance: " + distance.ToString("F2") +
                " | Can enter: " + canEnter
            );
        }

        if (triggerPressed)
        {
            if (isInsideVehicle)
            {
                ExitVehicle();
            }
            else if (canEnter)
            {
                EnterVehicle();
            }
        }

    }



    private void EnterVehicle()
    {
        if (driverSeat == null || playerRig == null ||
            carController == null || playerCamera == null)
        {
            Debug.LogError("Vehicle references are missing!");
            return;
        }

        
        if (playerCharacterController != null)
        {
            playerCharacterController.enabled = false;
        }

        
        Vector3 cameraOffset =
            playerCamera.position - playerRig.position;

        playerRig.position =
            driverSeat.position - cameraOffset;

       
        carController.isDriving = true;

        isInsideVehicle = true;

        Debug.Log("Player entered vehicle!");
    }


    private void OnEnable()
    {
        enterVehicleAction.action?.Enable();
    }

    private void OnDisable()
    {
        enterVehicleAction.action?.Disable();
    }



    private void LateUpdate()
    {
        if (!isInsideVehicle)
            return;

        if (playerRig == null || driverSeat == null || playerCamera == null)
            return;

       
        Vector3 cameraOffset =
            playerCamera.position - playerRig.position;

    
        playerRig.position =
            driverSeat.position - cameraOffset;
    }





    private void ExitVehicle()
    {
        if (playerRig == null || exitPoint == null ||
            playerCamera == null || carController == null)
        {
            Debug.LogError("Exit references are missing!");
            return;
        }

        
        carController.isDriving = false;

        isInsideVehicle = false;

        
        Vector3 cameraOffset =
            playerCamera.position - playerRig.position;

        cameraOffset.y = 0f;

        
        playerRig.position =
            exitPoint.position - cameraOffset;

        if (playerCharacterController != null)
        {
            playerCharacterController.enabled = true;
        }

        Debug.Log("Player exited vehicle!");
    }

}
