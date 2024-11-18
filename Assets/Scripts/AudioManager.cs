using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip wrongSelection;
    [SerializeField] public AudioClip correctSelection;
    [SerializeField] public AudioClip emptySelection;

    private void Awake()
    {
        instance = this;
    }

    public void playSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
