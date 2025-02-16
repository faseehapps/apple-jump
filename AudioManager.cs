using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Audio Sources")]
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource FX;

    [Header("Audio Clips")]
    public AudioClip jump;
    public AudioClip crunch;
    public AudioClip stop;
    public AudioClip death;

    public void PlaySFX(AudioClip clip)
    {
        FX.PlayOneShot(clip);
    }
}
