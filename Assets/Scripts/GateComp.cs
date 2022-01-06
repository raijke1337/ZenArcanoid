using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateComp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var pos = other.transform.position;
        other.transform.position = new Vector3(pos.x, pos.y * -1, pos.z);
    }

}
