using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject optionsMenuPanel;
    public GameObject askTutorial;
    public void StartGame()
    {
        AskTutorial();
    }
    public void QuitGame()
    {
        
        Application.Quit();
     
    }
    public void AskTutorial()
    {
        askTutorial.SetActive(true);
    }
    public void YesTutorial()
    {
        SceneManager.LoadScene("GameSceneTutorial");
    }
    public void NoTutorial()
    {
        SceneManager.LoadScene("GameScene");
    }
    private void Update()
    {
        if (optionsMenuPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenuPanel.SetActive(false);
        }
    }
    public void Options()
    {
        optionsMenuPanel.SetActive(true);
    }
    public void SetWidth(int newWidth)
    {
        width = newWidth;
    }
    public void SetHeight(int newHeight)
    {
        height = newHeight;
    }
    public void SetRes()
    {
        Screen.SetResolution(width, height, false);
    }
}
