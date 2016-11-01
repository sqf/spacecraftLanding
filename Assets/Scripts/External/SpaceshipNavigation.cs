using UnityEngine;
using System.Collections;

public class SpaceshipNavigation : MonoBehaviour {
    [SerializeField]
    private Rigidbody rb;

    Vector3 prevRotation = Vector3.zero;
    public Vector3 getAngularVelocity()
    {
        Vector3 angularVelocity = rb.rotation.eulerAngles - prevRotation;
        prevRotation = rb.rotation.eulerAngles;
        return angularVelocity;
    }

    public Vector3 getRotation()
    {
        Vector3 angles = rb.rotation.eulerAngles;
        return new Vector3(toAngularDifference(angles.x), toAngularDifference(angles.y), toAngularDifference(angles.z));
    }

    public Vector3 getVelocity()
    {
        return transform.InverseTransformDirection(rb.velocity);
    }

    public Vector3 getPosition()
    {
        return rb.position;
    }

    private float toAngularDifference(float angle)
    {
        if (angle > 180)
        {
            angle = angle - 360;
        }
        return angle;
    }
}
