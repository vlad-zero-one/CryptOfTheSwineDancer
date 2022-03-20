using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject controlButtons;
    [SerializeField] TickSystem tickSystem;

    public void GameOver()
    {
        //tickSystem.Tick();

        gameObject.SetActive(true);
        controlButtons.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Continue();
        return;
    }

    public void Continue()
    {
        controlButtons.SetActive(true);
        gameObject.SetActive(false);
    }
}
