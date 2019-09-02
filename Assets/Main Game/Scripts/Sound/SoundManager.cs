using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public AudioSource SFX;
    public AudioSource Music;

    public static SoundManager Instance;

    // Used for small sound changes
    public float lowPitch = 0.95f;
    public float highPitch = 1.05f;

    void Awake()
    {
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}

        Instance = this;

       // DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        SFX.clip = clip;
        SFX.Play();
    }

    public void StopSFX()
    {
        SFX.Stop();
    }

    public void RandomizeSFX(params AudioClip[] clips)
    {
        if (clips == null)
        {
            return;
        }
        
        var index = clips.Length > 0 ? Random.Range(0, clips.Length) : 0;
        var pitch = Random.Range(lowPitch, highPitch);

        try
        {
            SFX.pitch = pitch;
            SFX.clip = clips[index];
            SFX.PlayOneShot(SFX.clip, 1f);
        }
        catch (Exception e)
        {
            Debug.Log($"SOUND MANAGER ERROR: {SFX.clip.name} | {e.Message}");
        }
    }
}
