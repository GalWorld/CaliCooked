using UnityEngine;

public class HingeDoor : MonoBehaviour
{
    HingeJoint hinge;
    public float speed = 80f;
    public float motorForce = 200f;

    public float closedAngle = 0f;
    public float openAngle = 90f;

    public float tolerance = 2f;

    bool isOpen = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();

        JointLimits limits = hinge.limits;
        limits.min = closedAngle;
        limits.max = openAngle;
        hinge.limits = limits;
        hinge.useLimits = true;
    }

    void Update()
    {
        if (hinge.useMotor)
        {
            float angle = hinge.angle;

            if (isOpen == true && Mathf.Abs(angle - openAngle) <= tolerance)
            {
                hinge.useMotor = false;
            }

            if (isOpen == false && Mathf.Abs(angle - closedAngle) <= tolerance)
            {
                hinge.useMotor = false;
            }
        }
    }

    public void ToggleDoor()
    {
        if (isOpen)
            Close();
        else
            Open();

        isOpen = !isOpen;
    }

    void Open()
    {
        JointMotor motor = hinge.motor;
        motor.force = motorForce;
        motor.targetVelocity = speed;
        hinge.motor = motor;
        hinge.useMotor = true;
    }

    void Close()
    {
        JointMotor motor = hinge.motor;
        motor.force = motorForce;
        motor.targetVelocity = -speed;
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
