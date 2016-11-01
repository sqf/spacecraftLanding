using UnityEngine;
using System.Collections;

public class SpaceshipNavigation : MonoBehaviour {
    [SerializeField]
    private Rigidbody spaceshipRigidbody;

    Vector3 prevRotation = Vector3.zero;
    public Vector3 getAngularVelocity()
    {
        Vector3 angularVelocity = spaceshipRigidbody.rotation.eulerAngles - prevRotation;
        prevRotation = spaceshipRigidbody.rotation.eulerAngles;
        return angularVelocity;
    }

    public Vector3 getRotation()
    {
        Vector3 angles = spaceshipRigidbody.rotation.eulerAngles;
        return new Vector3(toAngularDifference(angles.x), toAngularDifference(angles.y), toAngularDifference(angles.z));
    }

    public Vector3 getVelocity()
    {
        return transform.InverseTransformDirection(spaceshipRigidbody.velocity);
    }

    public Vector3 getPosition()
    {
        return spaceshipRigidbody.position;
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
