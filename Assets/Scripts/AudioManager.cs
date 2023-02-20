using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static string SfxGroup = "SFX";


    [Header("UI Sounds")]
    [Tooltip("General button click.")]
    [SerializeField] AudioClip m_DefaultButtonSound;
    // Start is called before the first frame update
    public static void PlayDefaultButtonSound()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
            return;

        PlayOneSFX(audioManager.m_DefaultButtonSound, Vector3.zero);
    }

    public static void PlayOneSFX(AudioClip clip, Vector3 sfxPosition)
    {
        if (clip == null)
            return;

        GameObject sfxInstance = new GameObject(clip.name);
        sfxInstance.transform.position = sfxPosition;

        AudioSource source = sfxInstance.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        // destroy after clip length
        Destroy(sfxInstance, clip.length);
    }

}
