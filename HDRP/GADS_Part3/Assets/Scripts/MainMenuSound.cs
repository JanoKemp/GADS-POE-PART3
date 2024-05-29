using UnityEngine;
using UnityEngine.UI;

public class MainMenuSound : MonoBehaviour
{
    public static MainMenuSound Instance;
    // References to Audio Sources
    public AudioSource musicSource;
    public AudioSource buttonClickSource; // Renamed for clarity

    private void Start()
    {// Singleton initialization
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
        // Play music (no need to use SoundManager since you have the AudioSource directly)
        if (musicSource != null) // Check if AudioSource is assigned
            musicSource.Play();

        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayButtonClickSound());
        }
    }
    
    public void PlayButtonClickSound()
    {
        // Play button click sound using the assigned AudioSource
        if (buttonClickSource != null) 
            buttonClickSource.Play();
    }
}