using UnityEngine;
using System.Collections;

public class SpaceshipPropulsion : MonoBehaviour
{
    public Rigidbody rb;

    public float mainEngineThrust = 100f;
    public float angularEngineThrustZ = 2f;
    public float angularEngineThrustX = 2f;
    public float angularEngineThrustY = 1f;
    public float additionalEngineThrustZ = 20f;
    public float additionalEngineThrustX = 20f;
    public float additionalEngineThrustY = 20f;

    private bool _mainEngineEnabled = false;
    public bool mainEngineEnabled
    {
        get
        {
            return _mainEngineEnabled;
        }
        set
        {
            _mainEngineEnabled = value;
        }
    }

    private float _rotateX = 0, _rotateY = 0, _rotateZ = 0;

    void FixedUpdate()
    {
        if (_mainEngineEnabled)
        {
            MainEngine(1);
        }
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
        rb.AddForce(transform.forward * thrust * mainEngineThrust, ForceMode.Acceleration);

    }
    public void MainEngineEnable()
    {
        _mainEngineEnabled = true;
    }
    public void MainEngineDisable()
    {
        _mainEngineEnabled = false;
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
        rb.AddTorque(transform.forward * thrust * angularEngineThrustZ, ForceMode.Acceleration);

    }

    private void RotateX(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        rb.AddTorque(transform.right * thrust * angularEngineThrustX, ForceMode.Acceleration);
    }

    private void RotateY(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        rb.AddTorque(transform.up * thrust * angularEngineThrustY, ForceMode.Acceleration);
    }

    public float additionalEngineParasiteTorque = 3f;
    public void AdditionalEngineZ(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        Vector3 force = transform.forward * thrust * additionalEngineThrustZ;
        rb.AddForce(force, ForceMode.Acceleration);
        rb.AddTorque(transform.right * thrust * Mathf.Cos(rb.rotation.x) * additionalEngineParasiteTorque, ForceMode.Acceleration);
    }

    public void AdditionalEngineX(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        Vector3 force = transform.right * thrust * additionalEngineThrustX;
        rb.AddForce(force, ForceMode.Acceleration);

        rb.AddTorque(-transform.forward * thrust * Mathf.Cos(rb.rotation.z) * additionalEngineParasiteTorque, ForceMode.Acceleration);
    }

    public void AdditionalEngineY(float thrust)
    {
        if (thrust > 1)
            thrust = 1;
        if (thrust < -1)
            thrust = -1;
        Vector3 force = transform.up * thrust * additionalEngineThrustY;
        rb.AddForce(force, ForceMode.Acceleration);
    }
}
