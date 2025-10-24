using UnityEngine;

public class timeslow : MonoBehaviour
{
    public bool ifslowtime = false;
    public float slowedtime = 0.2f;

    private void Update()
    {
        if (ifslowtime)
            Time.timeScale = slowedtime; // 10% speed
        else
            Time.timeScale = 1f; // normal speed
    }

}
