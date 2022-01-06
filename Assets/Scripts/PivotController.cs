using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    private Transform _pivot;
    private Vector2 _input;

    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField,Tooltip("Multiplies input's 1 or -1 by this value and the result is used as Y angle for target rotation")]
    private float rotateMultiplier = 3f;


    private Vector2 targetHeight = Vector2.zero;
    private Quaternion targetRotation;

    private void Start()
    {
        _pivot = GameController.MainController.GetPivot;
    }

    private void Update()
    {
        _input = GameController.MainController.InputValue;
        if (_input.x != 0)
        {
            targetRotation = Quaternion.Euler(0f, _pivot.localEulerAngles.y - (_input.x*rotateMultiplier), 0f);
        }
        if (_input.y != 0)
        {
            targetHeight = _pivot.position + new Vector3(0f, _input.y);
        }
        PivotMovement();
    }
    private void PivotMovement()
    {
        _pivot.position = Vector3.Lerp(_pivot.position, targetHeight, Time.deltaTime);
        // todo make this movement pretty
        _pivot.rotation = Quaternion.LerpUnclamped(_pivot.rotation,targetRotation,Time.deltaTime);

    }

}
