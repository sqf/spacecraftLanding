using UnityEngine;
using System.Collections;

public class PropulsionController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody mainThrusterPivot;

    [SerializeField]
    private float mainThrusterMaxForce = 5f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            mainThrusterPivot.AddRelativeForce(0, mainThrusterMaxForce, 0, ForceMode.Force);
        }
    }
}
