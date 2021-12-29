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

public class MainMenuControllerScript : MonoBehaviour
{
    protected string Version = "0.2";
    private Vector3 OriginalScale;
    public bool IsHidden;


    private void Start()
    {
        var txt = GameObject.Find("version").GetComponent<Text>().text = "Version: " + Version;
        OriginalScale = transform.localScale;
        IsHidden = false;
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
    }
    public void StartTheGame()
    {
        StartCoroutine(MenuDisappears(true));
    }

    private IEnumerator MenuDisappears(bool loadlevel)
    {
        // set buttons as inactive
        var buttons = GetComponentsInChildren<Button>();
        foreach (var b in buttons)
        {
            b.interactable = false;
        }

        var rect = gameObject.GetComponent<RectTransform>();

        float progress = 0; 
        Vector3 from = rect.localScale;
        Vector3 to = Vector3.zero;

        while (progress < 1)
        {
            progress += Time.deltaTime;
            rect.localScale = Vector3.Lerp(from, to, progress);
            yield return null;
        }
        if (progress < 0) yield break;
        IsHidden = true;
        if (!loadlevel) yield break;
        SceneManager.LoadSceneAsync(1);
    }
    private IEnumerator MenuReappears()
    {
        var buttons = GetComponentsInChildren<Button>();
        var rect = gameObject.GetComponent<RectTransform>();

        float progress = 0;
        Vector3 from = rect.localScale;
        Vector3 to = OriginalScale;

        while (progress < 1)
        {
            progress += Time.deltaTime;
            rect.localScale = Vector3.Lerp(from, to, progress);
            yield return null;
        }

        foreach (var b in buttons)
        {
            b.interactable = true;
        }
        IsHidden = false;
    }
    public void ShowMenu()
    {
        StartCoroutine(MenuReappears());
    }
    public void HideMenu()
    {
        StartCoroutine(MenuDisappears(false));
    }


}
