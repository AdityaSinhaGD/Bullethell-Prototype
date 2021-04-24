using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject player; //Gets reference to player
    private PlayerController playerScript; //Reference to player's script
    [SerializeField] private GameObject goal; //Gets reference to goal
    //private Goal goalScript; //Reference to goal's script

    [SerializeField] private Slider lifeSlider; //Gets reference to life bar
    [SerializeField] private Image lifeFill; //Gets reference to life bar's fill
    [SerializeField] private GameObject shotIcon; //Parent gameobject for shot timer
    [SerializeField] private Image shotTimer; //Gets reference to triple shot timer
    [SerializeField] private GameObject shieldIcon; //Parent gameobject for shield timer
    [SerializeField] private Image shieldTimer; //Gets reference to shield timer
    [SerializeField] private GameObject speedIcon; //Parent gameobject for speed timer
    [SerializeField] private Image speedTimer; //Gets reference to speed timer




    //private float powerupDuration = 0.0f; //Amount of time powerup lasts
    //private float powerupRemaining = 0.0f; //Amount of time left for powerup

    // Start is called before the first frame update
    private void Start()
    {
        if (!player) //Attempts to search for player if one has not been assigned
            player = GameObject.Find("Player");

        if (player) //Get's reference to script if player exists
            playerScript = player.GetComponent<PlayerController>();

        if (shotIcon) //Hides triple shot timer
            shotIcon.gameObject.SetActive(false);
        if (shieldIcon) //Hides shield timer
            shieldIcon.gameObject.SetActive(false);
        if (speedIcon) //Hides speed timer
            speedIcon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLifeBar();

        if (playerScript) //Ends the game
        {
            if (playerScript.IsDead)
                GoToGameOver();
            if (playerScript.AtGoal)
                GoToWin();
        }
    }

    //------------HUD Updating------------
    private void UpdateLifeBar() //Updates player life bar
    {
        float percentHealth = 0;
        
        if (playerScript)
            percentHealth = playerScript.CurrentHealth / playerScript.MaxHealth;

        if (lifeSlider) //Updates life bar value
            lifeSlider.value = percentHealth;

        if (lifeFill) //Updates life bar color
        {
            if (percentHealth <= 0.15f && lifeFill.color != Color.red) //Set color to red
                lifeFill.color = Color.red;
            else if (percentHealth > 0.15f && percentHealth <= 0.5f && lifeFill.color != Color.yellow) //Set color to yellow
                lifeFill.color = Color.yellow;
            else if (percentHealth > 0.5f && lifeFill.color != Color.green) //Set color to green
                lifeFill.color = Color.green;
        }
    }

    public void StartPowerupTimer(Powerup powerup, float duration) //Updates timer for triple shot power up
    {

        if (powerup is MultipleBulletsPowerup)
        {
            if (shotIcon)
                StartCoroutine(UpdateShotTimer(duration));
        }
        else if (powerup is SheildPowerup)
        {
            if (shieldIcon)
                StartCoroutine(UpdateShieldTimer(duration));
        }
        else if (powerup is SpeedPowerup)
        {
            if (speedIcon)
                StartCoroutine(UpdateSpeedTimer(duration));
        }
    }

    private IEnumerator UpdateShotTimer(float duration)
    {
        float powerupDuration = duration;
        float powerupRemaining = duration;

        shotIcon.SetActive(true); //Shows shot icon
        shieldIcon.SetActive(false); //Hides shield icon
        speedIcon.SetActive(false); //Hides speed icon

        if (shotTimer) //Resets shot timer
            shotTimer.fillAmount = 1.0f;

        while (powerupRemaining > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            powerupRemaining -= Time.deltaTime;

            if (shotTimer)
                shotTimer.fillAmount = powerupRemaining / powerupDuration;
        }

        shotIcon.SetActive(false); //Hides shot icon
    }

    private IEnumerator UpdateShieldTimer(float duration)
    {
        float powerupDuration = duration;
        float powerupRemaining = duration;

        shotIcon.SetActive(false); //Hides shot icon
        shieldIcon.SetActive(true); //Shows shield icon
        speedIcon.SetActive(false); //Hides speed icon

        if (shieldTimer) //Resets shield timer
            shieldTimer.fillAmount = 1.0f;

        while (powerupRemaining > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            powerupRemaining -= Time.deltaTime;

            if (shieldTimer)
                shieldTimer.fillAmount = powerupRemaining / powerupDuration;
        }

        shieldIcon.SetActive(false); //Hides shield icon
    }

    private IEnumerator UpdateSpeedTimer(float duration)
    {
        float powerupDuration = duration;
        float powerupRemaining = duration;

        shotIcon.SetActive(false); //Hides shot icon
        shieldIcon.SetActive(false); //Hides shield icon
        speedIcon.SetActive(true); //Shows speed icon

        if (speedTimer) //Resets speed timer
            speedTimer.fillAmount = 1.0f;

        while (powerupRemaining > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            powerupRemaining -= Time.deltaTime;

            if (speedTimer)
                speedTimer.fillAmount = powerupRemaining / powerupDuration;
        }

        speedIcon.SetActive(false); //Hides speed icon
    }

    //------------Scene Loading------------
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

    private bool IsSceneValid(string sceneName) //Returns whether the passed scene name is valid
    {
        return SceneUtility.GetBuildIndexByScenePath("Scenes/" + sceneName) >= 0;
    }
}
