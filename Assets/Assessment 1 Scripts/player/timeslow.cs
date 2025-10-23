using UnityEngine;

public class timeslow : MonoBehaviour
{
    public bool ifslowtime = false;
    void Start()
    {
        if(ifslowtime)
            Time.timeScale = 0.1f; // 10% speed
    }

}
