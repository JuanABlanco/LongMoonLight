using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botones : MonoBehaviour
{
    public Button girarIzq;
    public Button girarDer;
    public Button eliminar;
    public Button mover;
    public static GameObject espejo;
    public static Vector3 posicionOriginalEspejo;
    public static bool manipularEspejo;
    public static bool espejoEnMovimiento;

    void Start()
    {

        girarIzq.gameObject.SetActive(false);
        girarDer.gameObject.SetActive(false);
        eliminar.gameObject.SetActive(false);
        mover.gameObject.SetActive(false);
        manipularEspejo = false;
        espejoEnMovimiento = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
         if(manipularEspejo){
            girarIzq.gameObject.SetActive(true);
            girarDer.gameObject.SetActive(true); 
            eliminar.gameObject.SetActive(true); 
            mover.gameObject.SetActive(true);          
        }else{
            girarIzq.gameObject.SetActive(false);
            girarDer.gameObject.SetActive(false);
            eliminar.gameObject.SetActive(false);
            mover.gameObject.SetActive(false);           
        }

    }

    public void girarDerecha(){
        SpinObject.clickEnEspejo = true;
        StartCoroutine(esperaGirarDerecha());
    }

    public void girarIzquierda(){
        SpinObject.clickEnEspejo = true;
        StartCoroutine(esperaGirarIzquierda());
    }

    public IEnumerator esperaGirarDerecha(){
        yield return new WaitForSeconds(0.1f);
        espejo.transform.Rotate(Vector3.forward, 20f);
        SpinObject.clickEnEspejo = true;
    }

    public IEnumerator esperaGirarIzquierda(){
        yield return new WaitForSeconds(0.1f);
        espejo.transform.Rotate(Vector3.forward, -20f);
        SpinObject.clickEnEspejo = true;
    }    

    public void eliminarEspejo(){
        Destroy(espejo, 0f);
        hacerVisibleBotones(false);
    }

    public static void hacerVisibleBotones(bool valor){
        manipularEspejo = valor;  
    }
}
