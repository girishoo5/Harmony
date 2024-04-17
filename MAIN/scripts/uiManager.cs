using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class uiManager : MonoBehaviour
{
    public GameObject[] miniGames;
    public TextMeshProUGUI gameText;
    public GameObject slider;
    public string currentGame = "Gaze";
    public int index = 0;

    public void click()
    {
        switch (index)
        {
            case 0:
                miniGames[0].SetActive(false);
                miniGames[1].SetActive(true);
                slider.SetActive(false);
                index = 1;
                currentGame = miniGames[1].name;
                gameText.text = currentGame;

                break;
            case 1:
                miniGames[1].SetActive(false);
                miniGames[2].SetActive(true);
                index = 2;
                slider.SetActive(true);
                currentGame = miniGames[2].name;
                gameText.text = currentGame;
                break;
            case 2:
                miniGames[2].SetActive(false);
                miniGames[3].SetActive(true);
                slider.SetActive(true);
                index = 3;
                currentGame = miniGames[3].name;
                gameText.text = currentGame;
                break;
            case 3:
                miniGames[3].SetActive(false);
                miniGames[0].SetActive(true);
                slider.SetActive(true);
                index = 0;
                currentGame = miniGames[0].name;
                gameText.text = currentGame;
                break;
        }
    }
    public void reset()
    {
        switch (miniGames[index].name)
        {
            case "Gaze":
                miniGames[index].GetComponent<GazeInteraction>().resetGame();
                break;
            case "Memory":
                miniGames[index].GetComponent<MemoryGame>().ResetGame();
                break;
            case "Still":
                miniGames[index].GetComponent<StillInteraction>().resetGame();
                break;
            case "Splash":
                break;
        }
    }
}
