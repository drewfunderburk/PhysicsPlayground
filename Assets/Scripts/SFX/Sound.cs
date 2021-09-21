using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for an audio file, pitch, volume, and play functionality
/// </summary>
[System.Serializable]
public class Sound
{
    public string Name = "New Sound";
    public AudioClip Clip;
    [Range(-3, 3)] public float Pitch = 1f;
    [Range(0, 1)] public float Volume = 1f;

    /// <summary>
    /// Play this sound from the specified audio source
    /// </summary>
    public void Play(AudioSource source)
    {
        source.pitch = Pitch;
        source.volume = Volume;
        source.PlayOneShot(Clip);
    }
}