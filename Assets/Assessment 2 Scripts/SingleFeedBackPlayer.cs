using UnityEngine;

public class SingleFeedBackPlayer : MonoBehaviour
{
    private AudioSource AudioSource;
    [SerializeField] private GameObject VFX;
    [SerializeField] private Quaternion VFXrotation;
    [SerializeField] private Vector3 AddVFXoffset;
    public void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    public void PlayFeedBack()
    {
        if (VFX != null)
        {
            Vector3 PlayLocation = this.transform.position;
            PlayLocation += AddVFXoffset;
            Instantiate(VFX, PlayLocation, VFXrotation);
        }
        AudioSource.Play();
    }
}
