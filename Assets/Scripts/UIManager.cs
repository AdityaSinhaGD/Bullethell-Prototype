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

    private bool firstFrame = true;
    private GameObject titleScreen;
    private bool hasTitle = false;
    private GameObject controlsScreen;
    private bool hasControls = true;
    private Button[] buttons;
    private EventTrigger[] triggers;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("TitleScreen");
        if (titleScreen)
        {
            hasTitle = true;
            //titleScreen.SetActive(true);
        }

        controlsScreen = GameObject.Find("ControlsScreen");
        if (controlsScreen)
        {
            hasControls = true;
            //controlsScreen.SetActive(false);
        }

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
        if (firstFrame)
        {
            HideControls();
            firstFrame = false;
        }
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

    public void ShowControls() //Shows Controls Screen
    {
        if (hasTitle)
            titleScreen.SetActive(false);
        if (hasControls)
            controlsScreen.SetActive(true);
    }

    public void HideControls() //Hides Controls Screen
    {
        if (hasTitle)
            titleScreen.SetActive(true);
        if (hasControls)
            controlsScreen.SetActive(false);
    }

    public void GoToGame() //Loads Game Scene
    {
        if (IsSceneValid("TestLevel"))
        {
            StopAllMusic();
            SceneManager.LoadScene("TestLevel");
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
