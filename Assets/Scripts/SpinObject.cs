using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    //El objeto necesita un collider
    public static bool clickEnEspejo;
    public Vector3 posicionOriginal;

    void Awake(){
        clickEnEspejo = false;
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(registrarClick());
        }
    }

    void OnMouseDown(){
        
        Debug.Log("Click sobre mi");
/*         Botones.posicionOriginalEspejo = this.transform.position;
 */     Botones.espejo = this.gameObject;
        StartCoroutine(esperarModoColocar());
    }

    IEnumerator registrarClick(){
        //clickEnEspejo = false;
        yield return new WaitForSeconds(0);
         if(clickEnEspejo == false){
            Botones.hacerVisibleBotones(false);
        }else{

        }
    }    

    IEnumerator esperarModoColocar(){//Se necesita esperar que el modoColocar termine, para evitar bugs
        yield return new WaitForSeconds(0.1f);
        if(Detectar.modoColocar == true){
            //Si despues de 0.1 segundos, modoColocar == true, entonces significa que
            //el click registrado sobre el espejo (reflector) es porque a penas se coloco
            //dicho espejo por primera vez en pantalla, por ello, los botones no deben
            //ser visibles
            Botones.hacerVisibleBotones(false);
        }else{
            //Si despues de 0.1 segundos, modoColocar == false, entonces significa que
            //el click registrado sobre el espejo (reflector) es valido, entonces
            //los botones deben ser visibles ya que se hizo click en un espejo
            //sin intervencion del modo colocar
            clickEnEspejo = true;
            Botones.hacerVisibleBotones(true);
        }
    }

}
