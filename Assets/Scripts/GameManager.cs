using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject mainMenu;
    public GameObject crosshairs;

    public FirstPersonMovement player;
    public FirstPersonLook playerLook;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        crosshairs.SetActive(false);
        player.enabled = false;
        playerLook.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void StartGame()
    {
        crosshairs.SetActive(true);
        player.enabled = true;
        playerLook.enabled = true;
        mainMenu.SetActive(false);
    }

    public void Win()
    {
        winUI.SetActive(true);
        player.enabled = false;
        playerLook.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
    }

    public void Lost()
    {
        Invoke("enableLoseCanvas", 5f);
    }

    public void enableLoseCanvas()
    {
        loseUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        player.enabled = false;
        playerLook.enabled = false;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
