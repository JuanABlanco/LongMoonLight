using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Botones : MonoBehaviour
{
    public Button girarIzq;
    public Button girarDer;
    public Button eliminar;
    public Button mover;
    public Transform BtnsAcciones;
    public static GameObject espejo;
    //public static Vector3 posicionOriginalEspejo;
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
            BtnsAcciones.position = espejo.transform.position;
        }
        else{
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
        yield return new WaitForSeconds(0.05f);//era 0.05f
        espejo.transform.Rotate(Vector3.forward, 20f);
        SpinObject.clickEnEspejo = true;
    }

    public IEnumerator esperaGirarIzquierda(){
        yield return new WaitForSeconds(0.05f);
        espejo.transform.Rotate(Vector3.forward, -20f);
        SpinObject.clickEnEspejo = true;
    }    

    public void eliminarEspejo(){
        Destroy(espejo, 0f);
        hacerVisibleBotones(false);
        Detectar.eliminarPtoProhibido(espejo.transform.position);
        Score.contador = Score.contador - 1;
        Score.movimientos = Score.movimientos + 1;
    }

    public static void hacerVisibleBotones(bool valor){
        manipularEspejo = valor;  
    }

    public void volverAlMenu(){
        if (EditorUtility.DisplayDialog("Regresar",
        "¿Desea regresar al menú principal?", "Sí", "No") == true){
            Application.LoadLevel("Inicio");
        };
    }
}
