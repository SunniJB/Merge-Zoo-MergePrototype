using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource audioSource;

    void Start()
    {
        // Create an audio source component on the same game object.
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayAudioClip(int index)
    {
        // Validate index
        if(index < 0 || index >= audioClips.Count){
            Debug.LogError("Invalid audio clip index!");
            return;
        }

        // Assign the clip to the audio source
        audioSource.clip = audioClips[index];
        // Play the audio clip
        audioSource.Play();
    }
}
