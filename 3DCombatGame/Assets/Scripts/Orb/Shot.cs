using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour, IPooledObject
{
    RFX4_EffectSettings effect;
    public ParticleSystem[] particles;

    RFX4_EffectSettings effectAux;

    class ParticlesData
    {
        public int maxParticles;
        public float rateOverTimeMax;
        public float rateOverTimeMin;
        public float rateOverDistanceMax;
        public float rateOverDistanceMin;

        public ParticlesData()
        {
            maxParticles = 0;
            rateOverTimeMax = 0;
            rateOverTimeMin = 0;
            rateOverDistanceMax = 0;
            rateOverDistanceMin = 0;
        }
    }

    ParticlesData[] particlesAux;

    void Awake()
    {
        effect = GetComponent<RFX4_EffectSettings>();
        particlesAux = new ParticlesData[particles.Length];
        for (int i = 0; i < particles.Length; i++)
        {
            particlesAux[i] = new ParticlesData();
        }
        FillParticlesAux();
    }


    public void OnObjectSpawn()
    {
        effect.enabled = true;
        foreach (var particle in particles)
        {
            particle.Play();
        }
        Invoke("Disable", 4);
    }

    void FillParticlesAux()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            particlesAux[i].maxParticles = particles[i].main.maxParticles;
            Debug.Log(particlesAux[i].maxParticles);
            particlesAux[i].rateOverDistanceMax = particles[i].emission.rateOverDistance.constantMax;
            particlesAux[i].rateOverDistanceMin = particles[i].emission.rateOverDistance.constantMin;
            particlesAux[i].rateOverTimeMin = particles[i].emission.rateOverTime.constantMin;
            particlesAux[i].rateOverTimeMax = particles[i].emission.rateOverTime.constantMax;
        }
    }

    void Disable()
    {
        RestartParticles();
        gameObject.SetActive(false);
        effect.enabled = false;
    }

    void RestartParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            var main = particles[i].main;
            main.maxParticles = particlesAux[i].maxParticles;

            var emission = particles[i].emission;
            var rateOverTime = emission.rateOverTime;
            rateOverTime.constantMax = particlesAux[i].rateOverTimeMax;
            rateOverTime.constantMin = particlesAux[i].rateOverTimeMin;
            emission.rateOverTime = rateOverTime;

            var rateOverDistance = emission.rateOverDistance;
            rateOverDistance.constantMax = particlesAux[i].rateOverDistanceMax;
            rateOverDistance.constantMin = particlesAux[i].rateOverDistanceMin;
            emission.rateOverDistance = rateOverDistance;

        }
    }
}
