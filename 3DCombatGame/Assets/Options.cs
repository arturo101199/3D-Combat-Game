﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("General Volume", volume);
    }
}
