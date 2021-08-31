using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Clase.Puntos;

public class Detectar : MonoBehaviour
{
    Vector3 posicion;
    bool colocar;//False -> No se coloca el objeto, solo se instancia. // True -> Se coloca el objeto
    GameObject instancia;
    public static List<Puntos> ptsProhibidos;

    public GameObject prefab;
    public TilemapRenderer cuadrillas;
    public static bool modoColocar;

    public bool ActivarEspejo;

    void Awake()
    {
        colocar = false;
        ptsProhibidos = new List<Puntos>();
        cuadrillas.enabled = false;

        ActivarEspejo = false;
    }

    void Start()
    {
        distribuirPtsProhibidosInicio();
    }

    void Update()
    {

        colocarNuevoEspejo();

    }

    void colocarNuevoEspejo(){

        posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (ActivarEspejo && colocar == false)
        {//Si activo el boton de espejos y no se esta en modo colocar

            colocar = true;
            activarModoColocar();
            instancia = Instantiate(prefab, new Vector3(posicion.x, posicion.y, 0), Quaternion.identity);

        }

        if (Input.GetMouseButtonDown(0))
        {//Si el usuario da Clicks
            
            if (ActivarEspejo && colocar == true)
            {//Si el modo colocacion esta activo y hay una instancia de espejo
                Vector3 posicionColocacion = calcularPosicion(posicion);
                if (sePuedeColocar(posicionColocacion))
                {
                    AudioManager.Instance.PonerFX();
                    colocar = false;
                    instancia.transform.position = posicionColocacion;
                    instancia = null;
                    ActivarEspejo = false;
                    StartCoroutine(desactivarModoColocar());
                    aggPtsProhibidos(posicionColocacion);
                }
                else
                {
                    Debug.Log("No se puede colocar");
                }
            }else{



            }

            

        }
        else
        {//Si el usuario no da Click
            if (Input.GetMouseButtonDown(1) && colocar == true && ActivarEspejo)
            {//Si el usuario da Click Derecho
                colocar = false;
                ActivarEspejo = false;
                Destroy(instancia, 0f);
                desactivarModoColocar();
            }
            if (colocar == true)//Hace que el espejo siga el mouse a cualquier posicion
            {
                posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                instancia.transform.position = new Vector3(posicion.x, posicion.y, 0);
            }
            if(Input.GetMouseButtonDown(1) && Botones.espejoEnMovimiento == true && colocar == true){

/*                 colocar = false;
                Botones.espejoEnMovimiento = false;
                instancia.transform.position = Botones.posicionOriginalEspejo;
                instancia = null;
                cuadrillas.enabled = false;
                modoColocar = false; */
                
            }            
        }
    }

    public void moverEspejo(){

/*         colocar = true;
        Botones.espejoEnMovimiento = true;
        instancia = Botones.espejo;
        activarModoColocar(); */

    }      

    public static Vector3 calcularPosicion(Vector3 posicion)
    {
        float x = posicion.x;
        float y = posicion.y;
        float xEntera = ((int)posicion.x);
        float yEntera = ((int)posicion.y);
        float difX = 0;
        float difY = 0;
        difX = x - xEntera;
        difY = y - yEntera;

        if (difX < 0)
        {//Si X es negativo
            if (difX < -0.5)
            {
                xEntera = xEntera - 1;
            }
        }
        else
        {//Si X es positivo
            if (difX > 0.5)
            {
                xEntera = xEntera + 1;
            }
        }

        if (difY < 0)
        {//Si X es negativo
            if (difY < -0.5)
            {
                yEntera = yEntera - 1;
            }
        }
        else
        {//Si X es positivo
            if (difY > 0.5)
            {
                yEntera = yEntera + 1;
            }
        }

        //Debug.Log("Diferencias, x: " + difX + ", y: " + difY);
        //Debug.Log("X : " + x + " , xEntera: " + xEntera + " , y: " + y + " , yEntera: " + yEntera);

        return new Vector3(xEntera, yEntera, 0f);
    }

    public static void aggPtsProhibidos(Vector3 posicionColocacion)
    {

        Puntos punto = new Puntos(posicionColocacion.x, posicionColocacion.y);
        ptsProhibidos.Add(punto);
    }

    public static void eliminarPtoProhibido(Vector3 posicionInicial){
        Debug.Log("Total lista: " + ptsProhibidos.Count);
        for(int i = 0; i < ptsProhibidos.Count; i++){
            if(ptsProhibidos[i].x == posicionInicial.x && ptsProhibidos[i].y == posicionInicial.y){
                Debug.Log("Borre: " + i +", " + ptsProhibidos[i].x + ", " + ptsProhibidos[i].y);
                ptsProhibidos.RemoveAt(i);
            }
        }
    }

    public static bool sePuedeColocar(Vector3 posicionColocacion)
    {
        for (int i = 0; i < ptsProhibidos.Count; i++)
        {
            if (ptsProhibidos[i].x == posicionColocacion.x && ptsProhibidos[i].y == posicionColocacion.y)
            {

                return false;

            }
        }
        return true;
    }

    void distribuirPtsProhibidosInicio()
    {
        for (int x = -10; x <= 12; x = x + 2)
        {
            for (int y = 5; y >= -5; y--)
            {
                //Debug.Log("x: " + x + ", y: " + y);
                ptsProhibidos.Add(new Puntos(x, y));
            }
        }
        for (int x = -11; x <= 11; x = x + 2)
        {
            for (int y = 5; y >= -5; y = y - 2)
            {
                //Debug.Log("x: " + x + ", y: " + y);
                ptsProhibidos.Add(new Puntos(x, y));
            }
        }
        ptsProhibidos.Add(new Puntos(11, -4));
        ptsProhibidos.Add(new Puntos(11, -3));
    }

    IEnumerator desactivarModoColocar()
    {
        cuadrillas.enabled = false;
        yield return new WaitForSeconds(0.05f);//Espera una fraccion de 15 segundos antes de desactivar el modoColocar
        modoColocar = false;
    }

    void activarModoColocar()
    {
        cuadrillas.enabled = true;
        modoColocar = true;
    }

    public void ActivadorEspejo()
    {
        ActivarEspejo = true;
    }
}