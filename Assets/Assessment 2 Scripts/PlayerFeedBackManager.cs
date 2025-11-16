using UnityEngine;
using System.Collections;
using static UnityEditor.PlayerSettings;

public class PlayerFeedBackManager : MonoBehaviour
{
    public bool BDebug_PlaySound = false;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip DMGSounds;
    [SerializeField] private AudioClip JumpSounds;
    [SerializeField] private AudioClip MoveSounds;
    [SerializeField] private AudioClip LandSounds;
    [SerializeField] private AudioSource AudioSource;

    [Header("VFX")]
    [SerializeField] private ParticleSystem DMGVFX;
    [SerializeField] private ParticleSystem LandVFX;

    [SerializeField] private SpriteRenderer SpriteRenderer;
    private Color Oricolors;
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        Color[] Oricolors = { SpriteRenderer.color };
    }
    public void PlayDMGPlayerFeedBack()
    {
       PlaySound(DMGSounds, false);
       PlayVfX(DMGVFX);
    }
    public void PlayJumpPlayerFeedBack()
    {
       PlaySound(JumpSounds, false);
    }
    public void PlayMovePlayerFeedBack()
    {
        PlaySound(MoveSounds, true);
    }
    public void StopMovePlayerFeedBack()
    {
        AudioSource.loop = false;
        AudioSource.Stop();
        if (BDebug_PlaySound) Log.Red("Stop move Sound");
    }
    public void PlayLandPlayerFeedBack()
    {
       PlaySound(LandSounds, false);
       PlayVfX(LandVFX);
    }

    private void PlayVfX(ParticleSystem VFX)
    {
        Vector3 PlayLocation = this.transform.position;
        Instantiate(VFX, PlayLocation, Quaternion.identity);
    }
    private void PlaySound(AudioClip Sound, bool bShouldLoop)
    {
        AudioSource.loop = bShouldLoop;
        if (AudioSource.clip != Sound)
        {
            AudioSource.clip = Sound;
            AudioSource.Play();
        }
    }
}
