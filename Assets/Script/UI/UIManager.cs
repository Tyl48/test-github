using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause Game")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive (false);
        pauseScreen.SetActive (false);
    }

    private void Update()
    {
        if(Input.GetKeyDown (KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);   

            else 
                PauseGame(true);
        }
    }
    #region Game Over
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif    
    }
    #endregion
    
    #region Pause
    public void PauseGame(bool status)
    {
        //If status == true pause | if status == false unpause
        pauseScreen.SetActive(status);
        
        //When pause status is true change timescale to 0 ( time stops )
        //| when it's false change it back to 1 (time goes by normally)
        if(status)
            Time.timeScale = 0;

        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);

    }
    #endregion


}
