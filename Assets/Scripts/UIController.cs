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

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected GameObject PauseMenuPanel;
    [SerializeField]
    protected GameObject HealthBar;

    private void Start()
    {
        GameController.MainController.PausePressedEventForUI += UIPauseHappened;
    }

    // toggle menu
    void UIPauseHappened()
    { 
        if (GameController.MainController.IsGamePaused)
        {            
            PauseMenuPanel.SetActive(true);
        }
        else
        {
            PauseMenuPanel.SetActive(false);
        }
    }

    // add from prefab
    // place on canvas
    private void HealthbarSetup()
    {
        HealthBar = Instantiate(Resources.Load<GameObject>("Prefabs/bar"));
        HealthBar.transform.parent = GameObject.Find("Menu").transform;
        HealthBar.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        HealthBar.GetComponent<HealthBarScript>().SetUpBar(3);
    }
}
