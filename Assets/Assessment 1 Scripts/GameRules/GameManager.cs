using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameOver;

    [SerializeField] private GameObject PlayerPrefab;
    private  HealthComponent healthComponent;

    private Vector3 CurrentCheckPointLocation;

    [SerializeField] private CheckPiont[] CheckPiontsArray;

    public float respawnDelay = 2f;

    //Time slow variables
    [Header("Time Variables")]
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

        /* dont think this is the best way to do this !!!!!!!
        if there is time come and change me 
        cp is the current array index */
        foreach (CheckPiont cp in CheckPiontsArray)             
            cp.SetNewSpwanPiont += OnCheckpointSet;

        //set first check point as start location
        CheckPiont firstCP = CheckPiontsArray[0];
        CurrentCheckPointLocation = firstCP.transform.position;
        PlayerPrefab.transform.position = CurrentCheckPointLocation;
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
    }

    public void OnplayerDamaged(float current, float max, float damage)
    {
        Log.Red(" we DMged");
        //StartCoroutine(DeadUpdate());
    }
    //public IEnumerator DeadUpdate()
    //{
    //    yield return new WaitForSecondsRealtime(respawnDelay);
    //    PlayerPrefab.transform.position = CurrentCheckPointLocation;
    //    StopAllCoroutines();
    //}
    public void OnPlayerDead(MonoBehaviour causer)
    {
        Log.Red("Game Manager > Player Dead");
    }
    private void OnCheckpointSet(Vector3 CheckPiontLocation)
    {
        CurrentCheckPointLocation = CheckPiontLocation;
        Log.Red($"Game Manager > Checkpoint set! {CheckPiontLocation}");
    }
}
