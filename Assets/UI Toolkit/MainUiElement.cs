using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private UIDocument uiDocument;

    private Button startButton;
    private Button quitButton;

    public AudioSource audioSource;
    
    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();

        startButton = uiDocument.rootVisualElement.Q<Button>("StartButtom");
        startButton.RegisterCallback<ClickEvent>(OnStart);

        quitButton = uiDocument.rootVisualElement.Q<Button>("QuitButtom");
        quitButton.RegisterCallback<ClickEvent>(OnQuit);
    }

    private void OnStart(ClickEvent evt)
    {
        PlayClickSound();
        SceneManager.LoadScene("Development Scene");
    }
    private void OnQuit(ClickEvent evt)
    {
        PlayClickSound();

        Application.Quit();
        Debug.Log("Quit clicked (won't exit in editor)");
    }
    private void PlayClickSound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(OnStart);
        quitButton.UnregisterCallback<ClickEvent>(OnQuit);
    }

}

