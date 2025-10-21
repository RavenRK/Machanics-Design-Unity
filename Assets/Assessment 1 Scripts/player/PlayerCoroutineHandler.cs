using UnityEngine;
using System.Collections;

public class PlayerCoroutineHandler : MonoBehaviour
{
    public playerCharacter PC;

    public Coroutine CGroundUpdate;
    public Coroutine CInputBufferUpdate;

    public IEnumerator GroundCheckUpdate()
    {
        PC.bIsGrounded = false;
        while (!PC.bIsGrounded)
        {
            Log.Blue("Ground Check Update Coroutine Running");
            PC.bIsGrounded = Physics2D.Raycast(PC.raycastPosition.position, Vector2.down, 0.05f, PC.gGroundLayer);     //ray cast for check grounded
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator InputBufferUpdate()
    {
        PC.jumpBufferTimer = PC.originalJumpBufferTimer;
        while (PC)
        {
            //if (bCanDebug) { Log.Blue("input buffer"); }
            PC.jumpBufferTimer -= Time.deltaTime;
            //InputBuffer();
            yield return null;
        }
        PC.jumpBufferTimer = PC.originalJumpBufferTimer;
    }

}
