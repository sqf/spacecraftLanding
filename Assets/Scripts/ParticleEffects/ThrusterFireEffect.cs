﻿using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ThrusterFireEffect : ParticleEffect
{
    public override float Intensity
    {
        get
        {
            return intensity;
        }
        set
        {
            intensity = value;
            UpdateIntensity();
        }
    }

   // [SerializeField]
    private float intensity = 0.5f;  //values 0 to 1

    [SerializeField]
    private float baseSpeed = 5;

    private ParticleSystem particleSystem;

    private float defaultEmissionRateMin, defaultEmissionRateMax;
    private float defaultStartSpeedMin, defaultStartSpeedMax;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        defaultEmissionRateMin = particleSystem.emission.rate.constantMin;
        defaultEmissionRateMax = particleSystem.emission.rate.constantMax;
    }

    void Update()
    {
        UpdateIntensity();
    }

    private void UpdateIntensity()
    {
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        if (intensity == 0)
        {
            emission.enabled = false;
        }
        else
        {
            emission.enabled = true;
        }

        ParticleSystem.MinMaxCurve rate = emission.rate;
        rate.constantMin = intensity * defaultEmissionRateMin;
        rate.constantMax = intensity * defaultEmissionRateMax;
        emission.rate = rate;

        particleSystem.startSpeed = intensity + baseSpeed;
    }
}
