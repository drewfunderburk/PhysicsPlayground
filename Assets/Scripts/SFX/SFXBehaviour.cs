using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores Sound objects in an internal array and provides functionality for playing them.
/// 
/// Depends on Sound
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SFXBehaviour : MonoBehaviour
{
    [Tooltip("List of Sound objects this SFXBehaviour will use")]
    [SerializeField] private Sound[] _sounds;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a Sound from the array with a given name. If the sound was null, will play nothing
    /// </summary>
    public void PlaySound(string name)
    {
        // Get the sound from the array and ensure it isn't null
        Sound sound = FindSoundByName(name);
        if (sound == null)
            return;

        // Play the sound
        sound.Play(_source);
    }

    /// <summary>
    /// Finds and returns a sound from the array with a given name. Returns null if no sound found
    /// </summary>
    private Sound FindSoundByName(string name)
    {
        foreach (Sound sound in _sounds)
        {
            if (sound.Name == name)
                return sound;
        }
        return null;
    }
}