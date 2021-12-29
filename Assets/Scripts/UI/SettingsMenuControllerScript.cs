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
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SettingsMenuControllerScript : MonoBehaviour
{
    private Vector3 OriginalScale;
    public bool IsHidden;
    private string dir;
    private Settings data;

    private void Start()
    {
        OriginalScale = transform.localScale;
        IsHidden = true;
        transform.localScale = Vector3.zero;

        // for save-laod
        dir = Application.dataPath;

        SetupStuff();
        CheckSettingsFile();


        Debug.Log("Directory: "+dir);

    }


    private IEnumerator MenuDisappears()
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
        StartCoroutine(MenuDisappears());
    }


 /// <summary>
 /// settings specific stuff
 /// </summary>

    // this finds the dropdown, checkmark ets
    private Dropdown _difselect;
    private Toggle _volOff;
    private Slider _volLv;
    private void SetupStuff()
    {
        _difselect = GetComponentInChildren<Dropdown>();
        _volOff = GetComponentInChildren<Toggle>();
        _volLv = GetComponentInChildren<Slider>();
    }

    // create new or load existing
    // is run once 
    private void CheckSettingsFile()
    {
        var form = new BinaryFormatter();
        var path = dir + "settings.txt";
        if (File.Exists(path))
        {
            Debug.Log("Save data found, loading");
            using (var stream = File.OpenRead(path))
            {
                // read data from file
                data = (Settings)form.Deserialize(stream);
            }
        }        
        else
        {
            Debug.Log("Save data not found, creating new file");
            var save = File.Create(path);
            save.Close();

            // create DefaultControls settings struct 
            Settings data = new Settings();

            using (var stream = File.OpenWrite(path))
            {
                form.Serialize(stream, data);
            }
        }
    }

    // t his one sets sliders etc to current settings
    public void SetSlidersWhenLoading ()
    {
        _difselect.value = (int)data.Difficulty;
        _volLv.value = data.Volume;
        _volOff.isOn = data.IsSoundDisabled;

    }

    // use data from settings sliders to adjust existing struct DATA
    public void SetDiff()
    {
        int index = _difselect.value;
        data.Difficulty = (Settings.DiffTypes)index;
    }
    public void SetVolumeOff()
    {
        data.IsSoundDisabled = _volOff.isOn;
    }
    public void SetVolumeLvl()
    {
        data.Volume = (int)_volLv.value;
    }
    // record new DATA into settings file
    public void UpdateSettings()
    {
        var form = new BinaryFormatter();
        var path = dir + "settings.txt";
        using (var stream = File.OpenWrite(path))
        {
            form.Serialize(stream, data);
        }
    }

    [Serializable]
    private struct Settings
    {
        public bool IsSoundDisabled;
        public int Volume;
        public enum DiffTypes
        {
            Baby,
            Casual,
            Doomguy
        }
        public DiffTypes Difficulty;
    }
}
