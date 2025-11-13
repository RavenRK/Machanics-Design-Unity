using UnityEngine;

public class VFX_killer : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
