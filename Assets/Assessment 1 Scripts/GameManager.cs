using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameOver;


    [SerializeField] private GameObject m_PlayerPrefab;
    private GameObject m_PlayerRef;

    void Start()
    {
        //spawn player in


        m_PlayerRef.GetComponent<playerCharacter>().OnPlayerDead += RespawnPlayer;
    }

    private void OnDestroy()
    {
        m_PlayerRef.GetComponent<playerCharacter>().OnPlayerDead -= RespawnPlayer;
    }


    private void RespawnPlayer()
    {
        //gets called from event, and can sort out player respawning
    }

}
