using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject spaceship;

    [SerializeField]
    private GameObject cameraWrapper;

    private SpaceshipPropulsion spaceshipPropulsion;

    void Start()
    {
        spaceshipPropulsion = spaceship.GetComponent<SpaceshipPropulsion>();
    }

    void Update()
    {
        float rotationVertical = Input.GetAxis("CameraVertical");
        if (rotationVertical > 0 && cameraWrapper.transform.rotation.eulerAngles.x < 45
            || rotationVertical < 0 && cameraWrapper.transform.rotation.eulerAngles.x > -45)
        {
            cameraWrapper.transform.Rotate(new Vector3(Input.GetAxis("CameraVertical"), 0, 0) / 10);
        }
        cameraWrapper.transform.Rotate(new Vector3(0, Input.GetAxis("CameraHorizontal"), 0) / 10);

        camera.transform.LookAt(spaceship.transform);
        HandeInput();
    }

    void HandeInput()
    {
        spaceshipPropulsion.mainEngineEnabled = Input.GetAxis("Thruster");
    }
}
