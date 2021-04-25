using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMOD.Studio;

public class UIManager : MonoBehaviour
{
    private EventInstance menuMusic;
    private EventInstance gameMusic;
    private EventInstance winMusic;
    private EventInstance lossMusic;

    private EventInstance currentMusic;

    private Button[] buttons;
    private EventTrigger[] triggers;

    // Start is called before the first frame update
    void Start()
    {
        menuMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Menu");
        gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Game");
        winMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Jingles/Win");
        lossMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Jingles/Lose");

        if (SceneManager.GetActiveScene().name == "TitleScene")
            currentMusic = menuMusic;
        else if (SceneManager.GetActiveScene().name == "ControlsScene")
            currentMusic = menuMusic;
        else if (SceneManager.GetActiveScene().name == "WinScene")
            currentMusic = winMusic;
        else if (SceneManager.GetActiveScene().name == "GameOverScene")
            currentMusic = lossMusic;

        currentMusic.start();


        buttons = FindObjectsOfType<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(delegate { PlayAudio("event:/UI/Click"); });
        }

        triggers = FindObjectsOfType<EventTrigger>();
        for (int i = 0; i < triggers.Length; i++)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener(delegate { PlayAudio("event:/UI/Hover"); });
            triggers[i].triggers.Add(entry);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //------------Scene Loading------------
    public void GoToTitle() //Loads Title Screen
    {
        if (IsSceneValid("TitleScene"))
        {
            StopAllMusic();
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void GoToControls() //Loads Controls Screen
    {
        if (IsSceneValid("ControlsScene"))
        {
            StopAllMusic();
            SceneManager.LoadScene("ControlsScene");
        }
    }

    public void GoToGame() //Loads Game Scene
    {
        if (IsSceneValid("GameScene"))
        {
            StopAllMusic();
            SceneManager.LoadScene("GameScene");
        }
    }

    public void GoToWin() //Loads Win Scene
    {
        if (IsSceneValid("WinScene"))
        {
            StopAllMusic();
            SceneManager.LoadScene("WinScene");
        }
    }

    public void GoToGameOver() //Loads GameOver Scene
    {
        if (IsSceneValid("GameOverScene"))
        {
            StopAllMusic();
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void ExitGame() //Exits the game
    {
        Application.Quit();
    }

    private bool IsSceneValid(string sceneName) //Returns whether the passed scene name is valid
    {
        return SceneUtility.GetBuildIndexByScenePath("Scenes/" + sceneName) >= 0;
    }

    private void StopAllMusic() //Stops all playing music
    {
        menuMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        winMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        lossMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void PlayAudio(string path) //Plays audio found at path
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }
}
