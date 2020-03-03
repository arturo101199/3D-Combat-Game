using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour, IPooledObject
{
    RFX4_EffectSettings effect;
    public ParticleSystem[] particles;

    ParticleSystem[] particlesAux;
    RFX4_EffectSettings effectAux;

    void Awake()
    {
        effect = GetComponent<RFX4_EffectSettings>();
        effectAux = effect;

        MakeAuxParticles(particles);
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

    void MakeAuxParticles(ParticleSystem[] _particles)
    {
        particlesAux = new ParticleSystem[particles.Length];
        particlesAux = _particles;
    }

    void Disable()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            Debug.Log("particles emission: " + particles[i].maxParticles + " Aux emission: " + particlesAux[i].maxParticles);
            particles[i] = particlesAux[i];
        }
        gameObject.SetActive(false);
        effect = effectAux;
        effect.enabled = false;
    }
}
