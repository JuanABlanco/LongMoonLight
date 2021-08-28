using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botones : MonoBehaviour
{
    public Button girarIzq;
    public Button girarDer;
    public Button eliminar;
    public static GameObject espejo;
    public static int espejosColocados;

    void Start()
    {

        girarIzq.gameObject.SetActive(false);
        girarDer.gameObject.SetActive(false);
        eliminar.gameObject.SetActive(false);
        espejosColocados = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(espejosColocados>0){
            girarIzq.gameObject.SetActive(true);
            girarDer.gameObject.SetActive(true); 
            eliminar.gameObject.SetActive(true);           
        }else{
            girarIzq.gameObject.SetActive(false);
            girarDer.gameObject.SetActive(false);
            eliminar.gameObject.SetActive(false);            
        }
    }

    public void girarDerecha(){
        espejo.transform.Rotate(Vector3.forward, 20f);
    }

    public void girarIzquierda(){
        espejo.transform.Rotate(Vector3.forward, -20f);
    }    

    public void eliminarEspejo(){
        Destroy(espejo, 0f);
        espejosColocados = espejosColocados - 1;
    }
}
