using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    //El objeto necesita un collider
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(Vector3.forward, 20f);
        } else if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(Vector3.forward, -20f);
        }
    }
}
