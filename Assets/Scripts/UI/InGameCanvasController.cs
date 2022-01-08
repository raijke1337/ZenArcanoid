using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject StartText;
    [SerializeField]
    protected GameObject PauseMenuPanel;

    private void Start()
    {
        GameController.MainController.GameStarted += MainController_GameStarted;
        GameController.MainController.PausePressedEventForUI += UIPauseHappened;
    }

    // toggle panels

    private void MainController_GameStarted(bool value)
    {
        StartText.SetActive(!value);
#if UNITY_STANDALONE_WIN
        GameController.Debugs.AddLogEvent($"Game start at {System.DateTime.Now.TimeOfDay}");
#endif
    }

    void UIPauseHappened(bool isPause)
    {
        PauseMenuPanel.SetActive(isPause);
#if UNITY_STANDALONE_WIN
        GameController.Debugs.AddLogEvent($"Paused: {isPause} at {System.DateTime.Now.TimeOfDay}");
#endif
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
#if UNITY_STANDALONE_WIN
        GameController.Debugs.AddLogEvent($"Return to menu {System.DateTime.Now.TimeOfDay}");
#endif
    }


}
