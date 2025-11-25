using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUi : MonoBehaviour
{
    private UIDocument uiDocument;

    private Label scoreLabel;

    private HealthComponent healthComponent;
    [SerializeField] private GameObject PlayerPrefab;

    private int DeathCounter = 0;

    //this dont work and im out of time :D

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        scoreLabel = uiDocument.rootVisualElement.Q<Label>("DeathCounter");
    }
    void Start()
    {
        healthComponent = PlayerPrefab.GetComponent<HealthComponent>();
        healthComponent.OnDamageTaken += OnPlayerDeadUiUpdate;

        // Initialize UI
        scoreLabel.text = DeathCounter.ToString();
    }
    private void OnPlayerDeadUiUpdate(float current, float max, float damage)
    {
        Log.Red("Game Manager > Player Dead");
        DeathCounter++;
        scoreLabel.text = $"Death Counter: {DeathCounter}";
        scoreLabel.text = DeathCounter.ToString();
    }
    private void OnDestroy()
    {
        healthComponent.OnDamageTaken -= OnPlayerDeadUiUpdate;
       // healthComponent.OnDead -= OnPlayerDead;

    }
}