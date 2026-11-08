
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public bool isDriving = false;

    public Rigidbody rigid;

    public WheelCollider wheel1;
    public WheelCollider wheel2;
    public WheelCollider wheel3;
    public WheelCollider wheel4;

    public float drivespeed = 600f;
    public float streetspeed = 40f;

    private float horizontalInput;
    private float verticalInput;


    private void Update()
    {
        horizontalInput = 0f;
        verticalInput = 0f;

        if (!isDriving)
            return;

        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.wKey.isPressed)
            verticalInput = 1f;

        if (keyboard.sKey.isPressed)
            verticalInput = -1f;

        if (keyboard.aKey.isPressed)
            horizontalInput = -1f;

        if (keyboard.dKey.isPressed)
            horizontalInput = 1f;
    }


    private void FixedUpdate()
    {
        float motor = verticalInput * drivespeed;

        wheel1.motorTorque = motor;
        wheel2.motorTorque = motor;
        wheel3.motorTorque = motor;
        wheel4.motorTorque = motor;

        float steering = horizontalInput * streetspeed;

        wheel1.steerAngle = steering;
        wheel2.steerAngle = steering;
    }
}
