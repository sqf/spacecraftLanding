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

    private Quaternion cameraWrapperDefaultRotation;

    private bool cameraLocked;

    void Start()
    {
        spaceshipPropulsion = spaceship.GetComponent<SpaceshipPropulsion>();
        cameraWrapperDefaultRotation = cameraWrapper.transform.rotation;
    }

    void Update()
    {
        HandleCameraInput();
        HandleInput();
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

        camera.transform.LookAt(spaceship.transform);

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
