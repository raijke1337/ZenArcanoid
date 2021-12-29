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

public class BallController : MonoBehaviour
{

    [SerializeField, Tooltip("Current ball speed")]
    float BallSpeed;
    [SerializeField, Tooltip("Max speed. Boosted by bouncing"), Range(0, 100)]
    int MaxSpeed = 20;

    [SerializeField, Tooltip("Multiplier for ball speed"), Range(0, 100)]
    float BallSpeedMult = 1.5f;

    [SerializeField]
    private GameObject Ball;
    private Rigidbody _ballRb;


    //reset ball to start pos
    void ResetBall()
    {
        //IsGameStarted = false;
        //Ball.transform.localPosition = player1.transform.localPosition + Vector3.forward;
        //Ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }


    //void BallMovement()
    //{
    //    if (!_ctrl.IsGameStarted) return;
    //    Ball.transform.Translate(ballVector * Time.deltaTime * BallSpeed * BallSpeedMult);
    //}
    //void CheckCollision(Collision coll)
    //{
    //    if (coll.gameObject.GetComponent<CubeComponent>() != null)
    //    {
    //        EnemyCubes.Remove(coll.gameObject);
    //        Destroy(coll.gameObject);
    //        if (EnemyCubes.Count == 0)
    //        {
    //            Debug.LogWarning("Level complete! Click to rebuild");
    //            NextLevel();
    //        }
    //        if (BallSpeed < MaxSpeed) { BallSpeed++; }
    //    }
    //    if (coll.gameObject == 
    //        )
    //    {
    //        if (BallSpeed < MaxSpeed) { BallSpeed++; }
    //    }
    //    if (BallSpeed > MaxSpeed) BallSpeed = MaxSpeed;
    //    // added fix
    //}


    private Vector3 ballVector;
    //just move forward

    // recalc vector of ball
    void RecalcDir(Collision coll)
    {
        var normalVect = coll.GetContact(0).normal;
        ballVector = Vector3.Reflect(ballVector, normalVect);
    }

}
