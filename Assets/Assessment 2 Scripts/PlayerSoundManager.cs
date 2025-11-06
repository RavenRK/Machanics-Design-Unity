using UnityEngine;
using System.Collections;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private playerStateManager stateM;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip JumpSounds;
    [SerializeField] private AudioClip MoveSounds;
    [SerializeField] private AudioClip LandSounds;


    void Start()
    {
        stateM = GetComponentInParent<playerStateManager>();    }

    void Update()
    {
    }
    public void PlayJumpSound()
    {
        if (AudioSource.clip != JumpSounds)
        AudioSource.clip = JumpSounds;

        AudioSource.Play();
        Log.Red("Play Jump Sound");
    }
    public void PlayMoveSound()
    {
        if (AudioSource.clip != MoveSounds)
        {
            AudioSource.clip = MoveSounds;
            AudioSource.Play();
        }
        else
        {
            Log.Red("dont play Sound");
        }
    }
    public void PlayLandSound()
    {
        if (AudioSource.clip != LandSounds)
            AudioSource.clip = LandSounds;

        AudioSource.Play();
        Log.Red("Play Move Sound");
    }
}
