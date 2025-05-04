using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class OVRControllerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 90.0f; // Degrees per second
    public float verticalMoveSpeed = 2.0f;
    public bool isEnabled = false;

    void Update()
    {
        if (!isEnabled) return;

        // Left joystick - horizontal/forward movement
        Vector2 leftJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector3 move = new Vector3(leftJoystick.y, 0, leftJoystick.x);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);

        // Right joystick - rotate (yaw)
        //Vector2 rightJoystick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //float yaw = rightJoystick.x * rotateSpeed * Time.deltaTime;
        //transform.Rotate(0, yaw, 0, Space.Self);

        // Triggers - vertical movement (world space)
        float up = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger); // Right trigger
        float down = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger); // Left trigger
        float vertical = (up - down) * verticalMoveSpeed * Time.deltaTime;
        transform.position += new Vector3(0, vertical, 0);
    }
}
