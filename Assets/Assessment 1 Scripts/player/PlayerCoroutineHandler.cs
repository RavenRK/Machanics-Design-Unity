using UnityEngine;
using System.Collections;

public class PlayerCoroutineHandler : MonoBehaviour
{
    public bool bCanDebug = false;
    public playerCharacter PC { get; private set; }
    public playerStateManager StateManager { get; private set; }

    public Coroutine C_GroundCheck { get; private set; }
    public Coroutine C_VerticalDirectionCheck { get; private set; }
    public Coroutine C_InputBufferCheck { get; private set; }
    public Coroutine C_CoyoteTimeCheck { get; private set; }
    public Coroutine C_MoveCheck { get; private set; }

    public void Awake()
    {
        PC = GetComponent<playerCharacter>();
        StateManager = GetComponent<playerStateManager>();
    }
    public IEnumerator GroundCheckUpdate()
    {
        yield return new WaitForSeconds(0.05f);
        while (!PC.bIsGrounded)
        {
            yield return new WaitForEndOfFrame();
        }
        Log.Green("Grounded Detected");
            PC.originalValuesReset();
            StateManager.ChangeState(StateManager.IdleState);
        C_GroundCheck = null;
    }
    public IEnumerator VerticalDirectionCheck()
    {
        while (PC.rb2D.linearVelocity.y > -0.25)
        {
            yield return null;
        }
        StateManager.ChangeState(StateManager.AirState);
        yield return new WaitForEndOfFrame();
        C_VerticalDirectionCheck = null;
    }
    public IEnumerator InputBufferUpdate()
    {
        PC.bIsInputbuffer = true;
        PC.jumpBufferTimer = PC.originalJumpBufferTimer;
        while (PC.jumpBufferTimer > 0)
        {
            PC.jumpBufferTimer -= Time.deltaTime;
            if (PC.bIsGrounded)
            {
                Log.Green("Input Buffer Jump Activated");
                PC.bCanJump = true;
                StateManager.ChangeState(StateManager.JumpState);
                break;
            }
            yield return null;

            if (PC.jumpBufferTimer <= 0)
            break;
        }
        C_InputBufferCheck = null;
    }
    public IEnumerator CoyoteTimeUpdate()
    {
        PC.coyoteTimeTimer = PC.originalCoyoteTimeTimer;
        PC.bCanCoyoteJump = true;
        while (PC.coyoteTimeTimer > 0)
        {
            PC.coyoteTimeTimer -= Time.deltaTime;
            yield return null;
        }

        C_CoyoteTimeCheck = null;
    }
    public IEnumerator MoveUpdate()
    {
        while (PC.Movedirection != Vector2.zero)
        {
            yield return null;
            PC.rb2D.linearVelocity = new Vector2(PC.Movedirection.x * PC.moveSpeed, PC.rb2D.linearVelocity.y);
        }
        StateManager.ChangeState(StateManager.IdleState);
    }
    #region Start / Stop Coroutines Func
    public Coroutine RunCoroutine(IEnumerator IEnum, Coroutine C_coroutine)
    {
        if (C_coroutine == null)
        {
            if (bCanDebug) Log.Green("Statr" + IEnum.ToString());
            return StartCoroutine(IEnum);
        }
        else
        {
            if (bCanDebug) Log.Red("Coroutine is already running");
            return null;
        }
    }
    public void StopPlayerCoroutine(Coroutine C_coroutine)
    {
        if (C_coroutine != null)
        {
            StopCoroutine(C_coroutine);
            C_coroutine = null;
        }
    }
    #endregion
}
