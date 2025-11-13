using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CheckPiont : MonoBehaviour, IInteractable
{
    public event Action<Vector3> SetNewSpwanPiont;
    private Vector3 SpawnLocation;
    private SingleFeedBackPlayer feedBackPlayer;

    void Start()
    {
        SpawnLocation = this.transform.position;
        feedBackPlayer = GetComponent<SingleFeedBackPlayer>();
    }

    public void Interact()
    {
        Log.Red("Checkpoint set!");
        SetNewSpwanPiont?.Invoke(SpawnLocation);
        feedBackPlayer.PlayFeedBack();
    }
}
