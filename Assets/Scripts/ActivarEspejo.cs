using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clase.Puntos;

public class ActivarEspejo : MonoBehaviour
{
    public bool espejoActivado;
    private static ActivarEspejo _instance;
    public static ActivarEspejo Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _instance.espejoActivado = false;
    }


    public void ActivaEspejo(){

        _instance.espejoActivado = true;
        
    }

}
