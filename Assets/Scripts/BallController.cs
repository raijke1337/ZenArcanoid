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
    
    public float BallSpeed  {get;set;}
       
    public bool IsBallActive { get; set; }

    private BouncyItemComponent platform;

    private void OnEnable()
    {
        platform = GameController.MainController.GetPivot.GetComponentInChildren<BouncyItemComponent>();
        ResetBall();
    }

    void ResetBall()
    {
        gameObject.transform.localPosition = platform.transform.localPosition + Vector3.forward;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Update()
    {
        gameObject.transform.localPosition += ballVector * BallSpeed * Time.deltaTime;
    }

    public event CollisionEventsHandler CollisionEvent;
    private void OnCollisionEnter(Collision collision)
    {
        var comp = collision.gameObject.GetComponent<BouncyItemComponent>();
        CollisionEvent?.Invoke(collision,comp);
    }

    private Vector3 ballVector = Vector3.forward;
    //just move forward

    // recalc vector of ball
    public void RecalcDir(Collision coll)
    {
        var normalVect = coll.GetContact(0).normal;
        ballVector = Vector3.Reflect(ballVector, normalVect);
    }

}
