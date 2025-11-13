using UnityEngine;
using System.Collections;
using static UnityEditor.PlayerSettings;

public class PlayerFeedBackManager : MonoBehaviour
{
    public bool BDebug_PlaySound = false;

    [Header("Audio Clips")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip JumpSounds;
    [SerializeField] private AudioClip MoveSounds;
    [SerializeField] private AudioClip LandSounds;
    [SerializeField] private AudioClip DMGSounds;

    [Header("VFX")]
    [SerializeField] private ParticleSystem DMGVFX;
    [SerializeField] private ParticleSystem LandVFX;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    //get called in the health component OnDamageTaken event
    public void PlayDMGPlayerFeedBack()
    {
        if (AudioSource.clip != DMGSounds)
            AudioSource.clip = DMGSounds;

        if (LandVFX != null)
        {
            Vector3 PlayLocation = this.transform.position;
            Instantiate(DMGVFX, PlayLocation, Quaternion.identity);
        }
        else { Log.Red("No DMG VFX assigned"); }
    }
    // gets called in the player State machine OnEnter Jump State
    public void PlayJumpPlayerFeedBack()
    {
        AudioSource.loop = false;

        if (AudioSource.clip != JumpSounds)
            AudioSource.clip = JumpSounds;

        AudioSource.Play();
        if (BDebug_PlaySound) Log.Red("Play Jump Sound");
    }
    // gets called in the player State machine OnEnter Move State
    public void PlayMovePlayerFeedBack()
    {
        if (AudioSource.clip != MoveSounds)
            AudioSource.clip = MoveSounds;

        AudioSource.loop = true;
        AudioSource.Play();

        if (BDebug_PlaySound) Log.Red("Play move Sound");
    }
    public void StopMovePlayerFeedBack()
    {
        AudioSource.loop = false;
        AudioSource.Stop();
        if (BDebug_PlaySound) Log.Red("Stop move Sound");
    }
    // gets called in the player State machine OnEnter iedle if the player was previously in air state
    public void PlayLandPlayerFeedBack()
    {
        AudioSource.loop = false;

        if (AudioSource.clip != LandSounds)
            AudioSource.clip = LandSounds;

        AudioSource.Play();
        if (BDebug_PlaySound) Log.Red("Land player Feedback");

        if (LandVFX != null)
        {
            Vector3 PlayLocation = this.transform.position;
            Instantiate(LandVFX, PlayLocation, Quaternion.identity);
        }
        else { Log.Red("No Land VFX assigned"); }
    }
}
