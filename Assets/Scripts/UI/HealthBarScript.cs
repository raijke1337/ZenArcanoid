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

public class HealthBarScript : MonoBehaviour
{
    private Sprite _pic;

    private void OnEnable()
    {
        _pic = Resources.Load("Art/heart") as Sprite;
    }

    public void SetUpBar(int _lives)
    {
        var rect = gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(_lives * 100, 100);
        Debug.Log("Bar setup for "+_lives+" lives");
        var txt = gameObject.GetComponentInChildren<Text>();
        txt.text = "Lives: " + _lives;
    }


}
