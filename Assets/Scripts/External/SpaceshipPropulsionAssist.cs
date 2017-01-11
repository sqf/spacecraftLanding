using UnityEngine;
using System.Collections;

public class SpaceshipPropulsionAssist : MonoBehaviour
{
    [SerializeField]
    private float pz = 2f, dz = 5f, px = 0.3f, dx = 2f;

    private bool _stabiliseTorqueTurnDisabled = false;

    private SpaceshipNavigation spaceshipNavigation;

    private SpaceshipPropulsion spaceshipPropulsion;

    public bool StabiliseTorqueEnabled { get; set; }
    public bool TurnAssistEnabled { get; set; }
    public bool DirectionAssistEnabled { get; set; }

    void Start()
    {
        spaceshipNavigation = GetComponent<SpaceshipNavigation>();
        spaceshipPropulsion = GetComponent<SpaceshipPropulsion>();
        StabiliseTorqueEnabled = true;
        TurnAssistEnabled = true;
        DirectionAssistEnabled = true;
    }

    void FixedUpdate()
    {
        if (StabiliseTorqueEnabled)
        {
            if (!_stabiliseTorqueTurnDisabled)
            {
                StabiliseTorque();
            }
            else
            {
                _stabiliseTorqueTurnDisabled = false;
            }
        }
    }

    public void setStabiliseTorque(bool enabled)
    {

        StabiliseTorqueEnabled = enabled;
    }

    public void switchStabiliseTorque()
    {
        StabiliseTorqueEnabled = !StabiliseTorqueEnabled;
    }

    public void StabiliseTorque()
    {
        Vector3 _rotation = spaceshipNavigation.getRotation();
        Vector3 _angularVelocity = spaceshipNavigation.getAngularVelocity();

        float zThrust = -pz * _rotation.z - dz * _angularVelocity.z;

        if (Mathf.Abs(_rotation.z) > 0.02f)
        {
            spaceshipPropulsion.setAngularThrustZ(zThrust);
        }

        float xThrust = -px * _rotation.x - dx * _angularVelocity.x;

        if (Mathf.Abs(_rotation.x) > 0.02f)
        {
            spaceshipPropulsion.setAngularThrustX(xThrust);
        }
    }
}
