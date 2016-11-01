using UnityEngine;
using System.Collections;

public class SpaceshipPropulsionAssist : MonoBehaviour {

    public float pz = 2f, dz = 5f, px =0.3f, dx = 2f;
    public float angularAssistZ = 0.2f, angularAssistX=0.2f;
    public float brakeRatio = 0.1f;
    public float verticalAssistChange = 3;
    public float turnZRotationRatio = 0.3f;
    SpaceshipNavigation nav;
    SpaceshipPropulsion sp;
    
    public bool _stabiliseTorqueEnabled { get; set;}
    public bool _turnAssistEnabled {get; set;}
    public bool _directionAssistEnabled { get; set; }

    private bool _stabiliseTorqueTurnDisabled = false;

    void Start () {
        nav = GetComponent<SpaceshipNavigation>();
        sp = GetComponent<SpaceshipPropulsion>();
        _stabiliseTorqueEnabled = true;
        _turnAssistEnabled = true;
        _directionAssistEnabled = true;
    }
	
	void FixedUpdate() {
        if (_stabiliseTorqueEnabled)
        {
            if (!_stabiliseTorqueTurnDisabled)
            {
                StabiliseTorque(angularAssistZ, angularAssistX);
            }
            else
            {
                _stabiliseTorqueTurnDisabled = false;
            }
        }
    }

    public void DisableTemporaryStabiliseTorque()
    {
        _stabiliseTorqueTurnDisabled = true;
    }
    public void setStabiliseTorque(bool enabled)
    {
        
        _stabiliseTorqueEnabled = enabled;
    }
    public void switchStabiliseTorque()
    {
        _stabiliseTorqueEnabled = !_stabiliseTorqueEnabled;
    }
    public void setTurnAssist(bool enabled)
    {
        _turnAssistEnabled = enabled;
    }
    public void switchTurnAssist()
    {
        _turnAssistEnabled = !_turnAssistEnabled;
    }
    public void switchDirectionAssist()
    {
        _directionAssistEnabled = !_directionAssistEnabled;
    }

    public void StabiliseTorque(float stabiliseRatioZ=0.2f,float stabiliseRatioX=0.2f)
    {
        Vector3 _rotation = nav.getRotation();
        Vector3 _angularVelocity = nav.getAngularVelocity();

        //Uzaleznienie od kąta
        float pzA = pz;
        float dzA = dz;
        float zThrust = - pzA * _rotation.z - dzA * _angularVelocity.z;

        if (Mathf.Abs(_rotation.z) > 0.01f)
        {
              sp.setAngularThrustZ(zThrust);
        }

        float pxA = px;
        float dxA = dx;
        float xThrust = -pxA * _rotation.x - dxA * _angularVelocity.x;

        sp.setAngularThrustX(xThrust);
    }

    public void ReduceVelocity()
    {
        Vector3 velocity = nav.getVelocity();
        float xThrust = -(velocity.x * brakeRatio);
        float yThrust = -(velocity.y * brakeRatio);
        float zThrust = -(velocity.z * brakeRatio);
     
        sp.AdditionalEngineX(xThrust);
        sp.AdditionalEngineY(yThrust);
        sp.AdditionalEngineZ(zThrust);
    }

    public void TurnHorizontal(float direction)
    {
        if (_turnAssistEnabled)
        {
            Vector3 _rotation = nav.getRotation() * Mathf.PI / 180;

            float yThrust = direction * Mathf.Cos(_rotation.z);
            float xThrust = direction * Mathf.Sin(_rotation.z);
            sp.setAngularThrustY(yThrust);
            sp.setAngularThrustX(xThrust);
            sp.setAngularThrustZ(-direction * Mathf.Cos(_rotation.z) * turnZRotationRatio);
        }
        else
        {
            float yThrust = direction*0.5f;
            sp.setAngularThrustY(yThrust);
        }
    }

    private float _wastedVelocity = 0f;
    private bool _adjustVelocityEnabled = false;
    public float _turnVeloctityThreshold = 10f;
    public void AdjustVelocityToDirection()
    {
        Vector3 velocity = nav.getVelocity();
        
        float xThrust = -(velocity.x * brakeRatio/2);
        float yThrust = -(velocity.y * brakeRatio/2);
        _wastedVelocity += Mathf.Abs( xThrust )+ Mathf.Abs(yThrust);
        Debug.Log(_wastedVelocity);
        sp.AdditionalEngineX(xThrust);
        sp.AdditionalEngineY(yThrust);
    }
}
