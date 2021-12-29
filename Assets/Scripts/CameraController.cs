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

public class CameraController : MonoBehaviour
{
    private Transform _target;

    private void Start()
    {
        _target = GameController.MainController.GetPlatform;
        transform.parent = null;
    }

    private void Update()
    {
        //FollowThePivot();
    }

    private void FollowThePivot()
    {
        var move = Vector3.Lerp(transform.localPosition, _target.position, Time.deltaTime);
        transform.localPosition += move;
    }


}
