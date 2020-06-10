using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float stepPitchMin;
    public float stepPitchMax;

    public float attackPitchMin;
    public float attackPitchMax;

    public float getHitPitchMin;
    public float getHitPitchMax;

    public float rollPitchMin;
    public float rollPitchMax;

    [SerializeField]
    private AudioClip[] steps;

    [SerializeField]
    private AudioClip[] attacks;

    [SerializeField]
    private AudioClip[] getHits;

    [SerializeField]
    private AudioClip[] roll;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        audioSource.pitch = UnityEngine.Random.Range(attackPitchMin, attackPitchMax);
        AudioClip clip = GetRandomClip(attacks);
        audioSource.PlayOneShot(clip);
    }

    public void GetHit()
    {
        audioSource.pitch = UnityEngine.Random.Range(getHitPitchMin, getHitPitchMax);
        AudioClip clip = GetRandomClip(getHits);
        audioSource.PlayOneShot(clip);
    }

    public void Step()
    {
        audioSource.pitch = UnityEngine.Random.Range(stepPitchMin, stepPitchMax);
        AudioClip clip = GetRandomClip(steps);
        audioSource.PlayOneShot(clip);
    }

    private void Roll(int i)
    {
        audioSource.pitch = UnityEngine.Random.Range(rollPitchMin, rollPitchMax);
        audioSource.PlayOneShot(roll[i]);
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
