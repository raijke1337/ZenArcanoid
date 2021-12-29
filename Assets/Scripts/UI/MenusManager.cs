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

public class MenusManager : MonoBehaviour
{

    private SettingsMenuControllerScript _sets;
    private MainMenuControllerScript _main;


    void Start()
    {
        _main = GameObject.Find("MainPanel").GetComponent<MainMenuControllerScript>();
        _sets = GameObject.Find("SettingsPanel").GetComponent<SettingsMenuControllerScript>();
    }

    public void SwitchToSettings()
    {
        _sets.SetSlidersWhenLoading();
        StartCoroutine(ToSettings());
    }
    public void SwitchToMain()
    {
        _sets.UpdateSettings();
        StartCoroutine(ToMain());
    }

    private IEnumerator ToSettings()
    {
        _main.HideMenu();
        while (_main.IsHidden == false)
        {
            yield return null;
        }
        _sets.ShowMenu();
    }
    private IEnumerator ToMain()
    {
        _sets.HideMenu();
        while (_sets.IsHidden == false)
        {
            yield return null;
        }
        _main.ShowMenu();
    }



}
