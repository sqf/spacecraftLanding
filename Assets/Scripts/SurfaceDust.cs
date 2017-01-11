using UnityEngine;
using System.Collections;

public class SurfaceDust : MonoBehaviour
{
    [SerializeField]
    private Transform spacecraft;

    [SerializeField]
    private float startDistance;

    [SerializeField]
    private float maxIntensity;

    private DustEffect dustEffect;

    private SpaceshipPropulsion spaceshipPropulsion;
    void Start()
    {
        dustEffect = GetComponent<DustEffect>();
        spaceshipPropulsion = spacecraft.gameObject.GetComponent<SpaceshipPropulsion>();
    }

    void Update()
    {
        transform.position = new Vector3(spacecraft.position.x, transform.position.y, spacecraft.position.z);

        float yDistance = spacecraft.position.y - transform.position.y;
        if (yDistance < startDistance && yDistance > 0)
        {
            dustEffect.Intensity = spaceshipPropulsion.mainEngineEnabled * (startDistance - yDistance) / startDistance * maxIntensity;
        }
        else if (yDistance <= 0)
        {
            dustEffect.Intensity = spaceshipPropulsion.mainEngineEnabled * maxIntensity;
        }
        else
        {
            dustEffect.Intensity = 0;
        }
    }
}
