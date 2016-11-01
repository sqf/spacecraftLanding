using System;
using UnityEngine;

public class MultipleParticleEffectsModifier : ParticleEffect
{
    [SerializeField]
    private ParticleEffect[] particleEffects;

    public override float Intensity
    {
        get
        {
            return intensity;
        }
        set
        {
            intensity = value;
            foreach (ParticleEffect effect in particleEffects)
            {
                effect.Intensity = intensity;
            }
        }
    }

    [SerializeField]
    private float intensity;

    private float previousIntensity;

    void Update()
    {
        if (previousIntensity != intensity)
        {
            Intensity = intensity;
            previousIntensity = intensity;
        }
    }
}
