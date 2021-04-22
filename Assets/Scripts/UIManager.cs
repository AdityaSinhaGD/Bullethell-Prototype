using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Scene Loading
    public void GoToTitle() //Loads Title Screen
    {
        if (IsSceneValid("TitleScene"))
            SceneManager.LoadScene("TitleScene"); 
    }

    public void GoToControls() //Loads Controls Screen
    {
        if (IsSceneValid("ControlsScene"))
            SceneManager.LoadScene("ControlsScene");
    }

    public void GoToGame() //Loads Game Scene
    {
        if (IsSceneValid("GameScene"))
            SceneManager.LoadScene("GameScene");
    }

    public void GoToWin() //Loads Win Scene
    {
        if (IsSceneValid("WinScene"))
            SceneManager.LoadScene("WinScene");
    }

    public void GoToGameOver() //Loads GameOver Scene
    {
        if (IsSceneValid("GameOverScene"))
            SceneManager.LoadScene("GameOverScene");
    }

    public void ExitGame() //Exits the game
    {
        Application.Quit();
    }

    private bool IsSceneValid(string sceneName) //Returns whether the passed scene name is valid
    {
        return SceneUtility.GetBuildIndexByScenePath("Scenes/" + sceneName) >= 0;
    }
}
