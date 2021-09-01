using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mover : MonoBehaviour
{
    bool colocar;
    bool modoColocar;
    GameObject instancia;
    Vector3 posicion;
    Vector3 posicionOriginal;
    public TilemapRenderer cuadrillas;

    // Start is called before the first frame update
    void Start()
    {
        modoColocar = false;
        colocar = false;
        instancia = null;
        posicion = new Vector3(0,0,0);
        posicionOriginal = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (instancia != null && colocar == true){//Movimiento
            posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
            instancia.transform.position = new Vector3(posicion.x, posicion.y, 0);                 
        }

        if(Input.GetMouseButtonDown(0) && colocar == true){//Si se da click con espejo en la mano
            posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 posicionColocacion = Detectar.calcularPosicion(posicion); 
            
            if(posicionOriginal == posicionColocacion){

                instancia.transform.position = posicionOriginal;
                colocar = false;
                instancia = null; 
                desactivarModoColocar();               

            }else{

                if(Detectar.sePuedeColocar(posicionColocacion)){
                    colocar = false;
                    instancia.transform.position = posicionColocacion;
                    instancia = null;
                    Detectar.eliminarPtoProhibido(posicionOriginal);
                    Detectar.aggPtsProhibidos(posicionColocacion);
                    desactivarModoColocar();
                    Score.movimientos = Score.movimientos + 1;
                }

            }
        }

        if(Input.GetMouseButtonDown(1) && colocar == true){
            instancia.transform.position = posicionOriginal;
            colocar = false;
            instancia = null;
            desactivarModoColocar();
        }
    }

    public void moverEspejo(){

        colocar = true;
        Botones.espejoEnMovimiento = true;
        instancia = Botones.espejo;
        Botones.hacerVisibleBotones(false);
        posicionOriginal = Botones.espejo.transform.position;
        activarModoColocar();

    }

    void activarModoColocar()
    {
        cuadrillas.enabled = true;
        modoColocar = true;
    }

    void desactivarModoColocar()
    {
        cuadrillas.enabled = false;
        modoColocar = false;
    }    

}
