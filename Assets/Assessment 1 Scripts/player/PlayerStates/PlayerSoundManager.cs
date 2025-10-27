using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private playerStateManager stateM;
    [SerializeField] private AudioSource JumpSounds;
    [SerializeField] private AudioSource MoveSounds;
    [SerializeField] private AudioSource LandSounds;
    void Start()
    {
        stateM = GetComponentInParent<playerStateManager>();
    }

    void Update()
    {
        
    }
    public void PlayJumpSound()
    {
        JumpSounds.Play();
    }
    public void PlayMoveSound()
    {
        MoveSounds.Play();
    }
    public void PlayLandSound()
    {
        MoveSounds.Play();
    }
}
