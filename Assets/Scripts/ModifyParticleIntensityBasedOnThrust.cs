using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MultipleParticleEffectsModifier))]
public class ModifyParticleIntensityBasedOnThrust : MonoBehaviour
{
    [SerializeField]
    private SpaceshipPropulsion spaceshipPropulsion;

    private MultipleParticleEffectsModifier multipleParticle;

    [SerializeField]
    private float rate = 0.05f;

    void Start()
    {
        multipleParticle = GetComponent<MultipleParticleEffectsModifier>();
    }

    void Update()
    {
        float targetIntensity = spaceshipPropulsion.mainEngineEnabled * 3;
        if (multipleParticle.Intensity < targetIntensity - rate)
        {
            multipleParticle.Intensity += rate;
        }
        else if(multipleParticle.Intensity > targetIntensity)
        {
            multipleParticle.Intensity -= rate;
        }
    }
}
