using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    //El objeto necesita un collider

    void Update(){

    }

    void OnMouseDown(){
        
        Debug.Log("Click sobre mi");
        Botones.espejo = this.gameObject;
        
    }    

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //transform.Rotate(Vector3.forward, 20f);
        } else if (Input.GetMouseButtonDown(0))
        {
            //transform.Rotate(Vector3.forward, -20f);
        }
    }

}
