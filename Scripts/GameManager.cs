using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int currentScene;

    private int initialSpeedrunScene;
    private int initialExploreScene;

    private bool isEasy;
    private bool isMedium;

    private bool hasChosenSpeedrun;

    private UIManager uiManager;

    private GameObject uiCamera;

    private void Awake()
    {

        currentScene = 0;
        initialSpeedrunScene = 1;
        initialExploreScene = 6;

        isEasy = false;
        isMedium = false;
        hasChosenSpeedrun = false;

    }

    private void Start()
    {

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiCamera = GameObject.Find("UICamera");

        uiManager.OpenStartMenu();

    }

    #region Difficoltà Speedrun

        public void SetEasy()
        {

            isEasy = true;
            isMedium = false;

            LoadSpeedrun();

        }

        public void SetMedium()
        {

            isEasy = false;
            isMedium = true;

            LoadSpeedrun();

        }

        public void SetHardcore()
        {

            isEasy = false;
            isMedium = false;

            LoadSpeedrun();

        }

        public bool IsEasy()
        {

            return isEasy;

        }

        /*Probabilmente rimovibile (forse utile con l'implementazione delle torrette nella modalità difficile)*/
        public bool IsMedium()
        {

            return isMedium;

        }

        public bool IsHard()
        {

            return !isEasy && !isMedium;

        }

    #endregion

    #region Gestione Scene

        #region Caricamento Livelli

            private void LoadScene(int sceneIndex)
            {

                PrepareForLoad(sceneIndex);
                SceneManager.LoadScene(currentScene, LoadSceneMode.Additive);

            }

            private void PrepareForLoad(int sceneIndex)
            {

                uiCamera.SetActive(false);
                uiManager.HideMenu();

                currentScene = sceneIndex;

            }

            public void LoadSpeedrun()
            {

                hasChosenSpeedrun = true;
                LoadScene(initialSpeedrunScene);

            }

            public void LoadExplore()
            {

                hasChosenSpeedrun = false;
                LoadScene(initialExploreScene);

            }

            public void LoadCurrentScene()
            {

                LoadScene(currentScene);

            }

            public void LoadNextScene()
            {

                LoadScene(currentScene + 1);

            }

            public void LoadRestartScene()
            {

                if (hasChosenSpeedrun && IsHard()) LoadScene(initialSpeedrunScene);
                else LoadScene(currentScene);

            }

        #endregion

        #region Caricamento UI

            private void LoadTop(GameObject uiToActivate)
            {

                SceneManager.UnloadSceneAsync(currentScene);

                uiCamera.SetActive(true);
                uiToActivate.SetActive(true);

            }

            public void OnDead()
            {

                LoadTop(uiManager.deadMenu);

            }

            public void OnNext()
            {

                LoadTop(uiManager.nextMenu);

            }

            public void OnFinishSpeedrun()
            {

                LoadTop(uiManager.finishSpeedrun);

            }

            public void OnFinishExplore()
            {

                LoadTop(uiManager.startMenu);

            }

            public void OnEasterEgg()
            {

                LoadTop(uiManager.easterEgg);

            }

        #endregion

    #endregion

    public CurrentSceneManager GetCurrentSceneManager()
    {

        return GameObject.Find("Manager").GetComponent<CurrentSceneManager>();

    }

    public GameObject GetUiCamera()
    {

        return uiCamera;

    }

    public void Quit()
    {

        Application.Quit();

    }

}
