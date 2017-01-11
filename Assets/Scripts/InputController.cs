using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject spaceship;

    [SerializeField]
    private GameObject cameraWrapper;

    private SpaceshipPropulsion spaceshipPropulsion;

    private SpaceshipPropulsionAssist spaceshipPropulsionAssist;

    private Quaternion cameraWrapperDefaultRotation;

    private bool cameraLocked = true;

    void Start()
    {
        spaceshipPropulsion = spaceship.GetComponent<SpaceshipPropulsion>();
        spaceshipPropulsionAssist = spaceship.GetComponent<SpaceshipPropulsionAssist>();
        cameraWrapperDefaultRotation = cameraWrapper.transform.rotation;
    }

    void Update()
    {
        HandleCameraInput();
        HandleInput();
        HandleStabilisationInput();
    }

    private void HandleStabilisationInput()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {   
            spaceshipPropulsionAssist.StabiliseTorqueEnabled = !spaceshipPropulsionAssist.StabiliseTorqueEnabled;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            spaceshipPropulsionAssist.DirectionAssistEnabled = !spaceshipPropulsionAssist.DirectionAssistEnabled;
        }
    }

    private void HandleCameraInput()
    {
        if (!cameraLocked)
        {
            float rotationVertical = Input.GetAxis("CameraVertical");
            if (rotationVertical < 0 && cameraWrapper.transform.rotation.eulerAngles.x < 45
                || rotationVertical > 0 && cameraWrapper.transform.rotation.eulerAngles.x > -45)
            {
                cameraWrapper.transform.Rotate(new Vector3(Input.GetAxis("CameraVertical"), 0, 0) / 10);
            }
            cameraWrapper.transform.Rotate(new Vector3(0, Input.GetAxis("CameraHorizontal"), 0) / 10);
        }

        if (camera != null)
        {
            camera.transform.LookAt(spaceship.transform);
        }

        if (Input.GetMouseButtonDown(0))
        {
            cameraWrapper.transform.localRotation = cameraWrapperDefaultRotation;
        }
        if (Input.GetMouseButtonDown(1))
        {
            cameraLocked = !cameraLocked;
        }
    }

    private void HandleInput()
    {
        spaceshipPropulsion.mainEngineEnabled = Input.GetAxis("Thruster");
        spaceshipPropulsion.setAngularThrustY(Input.GetAxis("RotateHorizontal"));
        spaceshipPropulsion.setAngularThrustX(Input.GetAxis("RotateVertical"));
        spaceshipPropulsion.setAngularThrustZ(Input.GetAxis("RotateZ"));
    }
}
