using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private enum Scene { MainMenu, Gameplay }

    [SerializeField] private Scene scene;

    [Header("Main Menu"), ShowIf("scene", Scene.MainMenu)]
    [SerializeField, ShowIf("scene", Scene.MainMenu)] private Button playButton;
    [SerializeField, ShowIf("scene", Scene.MainMenu)] private Button quittButton;

    [Header("GamePlay"), ShowIf("scene", Scene.Gameplay)]
    [SerializeField, ShowIf("scene", Scene.Gameplay), SceneObjectsOnly] private Bricks bricks;
    [SerializeField, ShowIf("scene", Scene.Gameplay), SceneObjectsOnly] private LossTrigger lossTrigger;
    [SerializeField, ShowIf("scene", Scene.Gameplay), SceneObjectsOnly] private Button pauseButton;
    [SerializeField, ShowIf("scene", Scene.Gameplay), SceneObjectsOnly] private Button replayButton;
    [SerializeField, ShowIf("scene", Scene.Gameplay), SceneObjectsOnly] private Button backToMainMenuButton;
    [SerializeField, ShowIf("scene", Scene.Gameplay), SceneObjectsOnly] private RectTransform menuPanel;
    [SerializeField, ShowIf("scene", Scene.Gameplay), SceneObjectsOnly] private TextMeshProUGUI textMenue;

    private bool isPaused;

    private void Awake()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
        Time.timeScale = 1f;
        if (scene == Scene.MainMenu)
        {
            HandleMainMenuButtonsUi();
        }
        else if (scene == Scene.Gameplay)
        {
            bricks.onAllBricksGetsDestroyed += Bricks_onAllBricksGetsDestroyed;
            lossTrigger.onBallReachTheLossTrigger += LossTrigger_onBallReachTheLossTrigger;
            HandleGamePlayButtonsUi();
        }
    }

    private void LossTrigger_onBallReachTheLossTrigger(object sender, EventArgs e)
    {
        Debug.Log("you Losse");
        DisplayMenu(true, "You Losse");
        Time.timeScale = 0f;
    }

    private void Bricks_onAllBricksGetsDestroyed(object sender, EventArgs e)
    {
        Debug.Log("You Win Show Menue");
        DisplayMenu(true, "You Win");
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        if (scene == Scene.MainMenu)
        {
            HandleMainMenuButtonsUi(false);
        }
        else if (scene == Scene.Gameplay)
        {
            bricks.onAllBricksGetsDestroyed -= Bricks_onAllBricksGetsDestroyed;
            lossTrigger.onBallReachTheLossTrigger -= LossTrigger_onBallReachTheLossTrigger;
            HandleGamePlayButtonsUi(false);

        }
    }

    #region GamePlay Scene Ui Manager
    private void HandleGamePlayButtonsUi(bool sub = true)
    {
        if (sub == true)
        {
            AddListnerToButton(pauseButton, () => PauseButton());
            AddListnerToButton(replayButton, () => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
            AddListnerToButton(backToMainMenuButton, () => { SceneManager.LoadScene("MainMenuScene"); });
        }
        else
        {
            pauseButton.onClick.RemoveAllListeners();
            replayButton.onClick.RemoveAllListeners();
            backToMainMenuButton.onClick.RemoveAllListeners();
        }
    }

    private void PauseButton()
    {
        isPaused = !isPaused;
        if (isPaused == true)
        {
            Time.timeScale = 0f;
            DisplayMenu(true, "Pause Menue");
        }
        else
        {
            DisplayMenu(false);
            Time.timeScale = 1f;
        }
    }
    #endregion


    #region MainMenu Scene Ui Manager
    private void HandleMainMenuButtonsUi(bool sub = true)
    {
        if (sub == true)
        {
            AddListnerToButton(playButton, () => { SceneManager.LoadScene("GamePlayScene"); });
            AddListnerToButton(quittButton, () => { Application.Quit(); });
        }
        else
        {
            playButton.onClick.RemoveAllListeners();
            quittButton.onClick.RemoveAllListeners();
        }
    }
    #endregion


    private void AddListnerToButton(Button btn, Action action)
    {
        btn.onClick.AddListener(() =>
        {
            action();
        });
    }
    private void DisplayMenu(bool display, string menuTitle = null)
    {
        menuPanel.gameObject.SetActive(display);
        textMenue.text = menuTitle;
    }
}
