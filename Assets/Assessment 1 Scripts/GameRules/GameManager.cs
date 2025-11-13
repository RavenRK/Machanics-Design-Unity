using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameOver;

    [SerializeField] private GameObject PlayerPrefab;
    private  HealthComponent healthComponent;

    private Vector3 CurrentCheckPointLocation;

    [SerializeField] private CheckPiont[] CheckPiontsArray;

    //Time slow variables
    public bool ifslowtime = false;
    public float slowedtime = 0.2f;
    private void Awake()
    {
        if (PlayerPrefab == null)
            Log.Red("Game manager > player ref > null ");

        PlayerPrefab = Instantiate(PlayerPrefab);
    }
    void Start()
    {
        healthComponent = PlayerPrefab.GetComponent<HealthComponent>();
        healthComponent.OnDamageTaken += OnplayerDamaged;
        healthComponent.OnDead += OnPlayerDead;

        // dont think this is the best way to do this !!!!!!!
        // if there is time come and change me 
        //cp is the current array index
        foreach (CheckPiont cp in CheckPiontsArray)             
            cp.SetNewSpwanPiont += OnCheckpointSet;
    }

    private void OnDestroy()
    {
        healthComponent.OnDamageTaken -= OnplayerDamaged;
        healthComponent.OnDead -= OnPlayerDead;
        //cp is the current index
        foreach (CheckPiont cp in CheckPiontsArray)
            cp.SetNewSpwanPiont -= OnCheckpointSet;
    }
    private void Update()
    {
        if (ifslowtime)
            Time.timeScale = slowedtime; // 10% speed
        else
            Time.timeScale = 1f; // normal speed
    }
    public void OnplayerDamaged(float current, float max, float damage)
    {
        Log.Red("Game Manager > Player DMGed");
        RespawnPlayer();
    }
    public void OnPlayerDead(MonoBehaviour causer)
    {
        Log.Red("Game Manager > Player Dead");
    }
    private void RespawnPlayer()
    {
        PlayerPrefab.transform.position = CurrentCheckPointLocation;
        //spawn player in after X seconds
    }
    private void OnCheckpointSet(Vector3 CheckPiontLocation)
    {
        CurrentCheckPointLocation = CheckPiontLocation;
        Log.Red($"Game Manager > Checkpoint set! {CheckPiontLocation}");
    }
}
