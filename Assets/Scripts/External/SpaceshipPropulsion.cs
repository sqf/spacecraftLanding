using UnityEngine;
using System.Collections;

public class SpaceshipPropulsion : MonoBehaviour
{
    public Rigidbody spaceshipRigidbody;

    public Rigidbody mainThruster;

    public float mainEngineMaxThrust = 100f;
    public float angularEngineThrustZ = 2f;
    public float angularEngineThrustX = 2f;
    public float angularEngineThrustY = 1f;
    public float additionalEngineThrustZ = 20f;
    public float additionalEngineThrustX = 20f;
    public float additionalEngineThrustY = 20f;

    private float _mainEngineEnabled = 0;
    public float mainEngineEnabled
    {
        get
        {
            return _mainEngineEnabled;
        }
        set
        {
            _mainEngineEnabled = Mathf.Clamp01(value);
        }
    }

    private float _rotateX = 0, _rotateY = 0, _rotateZ = 0;

    void FixedUpdate()
    {
        MainEngine(_mainEngineEnabled);
        
        RotateX(_rotateX);
        _rotateX = 0;
        RotateY(_rotateY);
        _rotateY = 0;
        RotateZ(_rotateZ);
        _rotateZ = 0;
    }

    public void MainEngine(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < 0)
            thrust = 0;
        mainThruster.AddForce(transform.up * thrust * mainEngineMaxThrust, ForceMode.Acceleration);
    }

    public void setAngularThrustX(float turnIntensity)
    {
        _rotateX += turnIntensity;
    }
    public void setAngularThrustY(float turnIntensity)
    {
        _rotateY += turnIntensity;
    }
    public void setAngularThrustZ(float turnIntensity)
    {
        _rotateZ += turnIntensity;
    }

    private void RotateZ(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        spaceshipRigidbody.AddTorque(transform.forward * thrust * angularEngineThrustZ, ForceMode.Acceleration);
    }

    private void RotateX(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        spaceshipRigidbody.AddTorque(transform.right * thrust * angularEngineThrustX, ForceMode.Acceleration);
    }

    private void RotateY(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        spaceshipRigidbody.AddTorque(transform.up * thrust * angularEngineThrustY, ForceMode.Acceleration);
    }

    public float additionalEngineParasiteTorque = 3f;
    public void AdditionalEngineZ(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        Vector3 force = transform.forward * thrust * additionalEngineThrustZ;
        spaceshipRigidbody.AddForce(force, ForceMode.Acceleration);
        spaceshipRigidbody.AddTorque(transform.right * thrust * Mathf.Cos(spaceshipRigidbody.rotation.x) * additionalEngineParasiteTorque, ForceMode.Acceleration);
    }

    public void AdditionalEngineX(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        Vector3 force = transform.right * thrust * additionalEngineThrustX;
        spaceshipRigidbody.AddForce(force, ForceMode.Acceleration);

        spaceshipRigidbody.AddTorque(-transform.forward * thrust * Mathf.Cos(spaceshipRigidbody.rotation.z) * additionalEngineParasiteTorque, ForceMode.Acceleration);
    }

    public void AdditionalEngineY(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        Vector3 force = transform.up * thrust * additionalEngineThrustY;
        spaceshipRigidbody.AddForce(force, ForceMode.Acceleration);
    }
}
