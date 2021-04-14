using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject startMenu;
    public GameObject optionsMenu;
    public GameObject modeMenu;
    public GameObject difficultyMenu;
    public GameObject nextMenu;
    public GameObject deadMenu;
    public GameObject finishSpeedrun;
    public GameObject easterEgg;

    public void HideMenu()
    {

        startMenu.SetActive(false);
        optionsMenu.SetActive(false);
        modeMenu.SetActive(false);
        difficultyMenu.SetActive(false);
        nextMenu.SetActive(false);
        deadMenu.SetActive(false);
        finishSpeedrun.SetActive(false);
        easterEgg.SetActive(false);

    }

    public void OpenStartMenu()
    {

        HideMenu();
        startMenu.SetActive(true);

    }

    public void OpenOptionsMenu()
    {

        HideMenu();
        optionsMenu.SetActive(true);

    }

    public void OpenModeMenu()
    {

        HideMenu();
        modeMenu.SetActive(true);

    }

    public void OpenDifficultyMenu()
    {

        modeMenu.SetActive(false);
        difficultyMenu.SetActive(true);

    }

}
