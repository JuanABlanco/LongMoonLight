using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clase.Puntos;

public class ActivarEspejo : MonoBehaviour
{

    List<Puntos> ptsActivarEspejos;
    public static bool espejoActivado;

    void Awake()
    {
        ptsActivarEspejos = new List<Puntos>();
        espejoActivado = false;
        distribuirPtsEspecialesInicio();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    void distribuirPtsEspecialesInicio()
    {
        //11 -4 11 -3
        ptsActivarEspejos.Add(new Puntos(11, -4));
        ptsActivarEspejos.Add(new Puntos(11, -3));
        ptsActivarEspejos.Add(new Puntos(12, -4));
        ptsActivarEspejos.Add(new Puntos(12, -3));
    }

    void OnMouseDown(){
        
        espejoActivado = true;
        Debug.Log("Espejo activadisimo");
        
    }

    public static bool clickEnActivarEspejo(Vector3 posicion)
    {
        if (posicion.x < 11)
        {
            return false;
        }
        else
        {
            for (int x = 11; x < 13; x++)
            {
                for (int j = -4; j <= -3; j++)
                {
                    if (posicion.y == j)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }  

}
