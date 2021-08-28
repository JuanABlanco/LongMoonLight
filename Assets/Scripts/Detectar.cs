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
    List<Puntos> ptsProhibidos;

    public GameObject prefab;
    public TilemapRenderer cuadrillas;
    public static bool modoColocar;

    void Awake()
    {
        colocar = false;
        ptsProhibidos = new List<Puntos>();
        desactivarModoColocar();
    }

    void Start()
    {
        distribuirPtsProhibidosInicio();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {//Si el usuario da Click

            posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //ActivarEspejo.clickEnActivarEspejo(calcularPosicion(posicion)) && colocar == false
            if (ActivarEspejo.espejoActivado && colocar == false)
            {//Si activo el boton de espejos y no se esta en modo colocar

                colocar = true;
                activarModoColocar();
                instancia = Instantiate(prefab, new Vector3(posicion.x, posicion.y, 0), Quaternion.identity);

            }
            else
            {

                if (ActivarEspejo.espejoActivado && colocar == true)
                {
                    Vector3 posicionColocacion = calcularPosicion(posicion);
                    if (sePuedeColocar(posicionColocacion))
                    {
                        colocar = false;
                        instancia.transform.position = posicionColocacion;
                        instancia = null;
                        ActivarEspejo.espejoActivado = false;
                        Botones.espejosColocados = Botones.espejosColocados + 1;
                        desactivarModoColocar();
                        aggPtsProhibidos(posicionColocacion);
                    }
                    else
                    {
                        Debug.Log("No se puede colocar");
                    }
                }

            }

        }
        else
        {//Si el usuario no da Click
            if (Input.GetMouseButtonDown(1) && colocar == true)
            {//Si el usuario da Click Derecho
                colocar = false;
                Destroy(instancia, 0f);
                desactivarModoColocar();
            }
            if (colocar == true)
            {
                posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                instancia.transform.position = new Vector3(posicion.x, posicion.y, 0);
            }
        }

    }

    private Vector3 calcularPosicion(Vector3 posicion)
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

    void aggPtsProhibidos(Vector3 posicionColocacion)
    {

        Puntos punto = new Puntos(posicionColocacion.x, posicionColocacion.y);
        ptsProhibidos.Add(punto);
    }

    bool sePuedeColocar(Vector3 posicionColocacion)
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

    void desactivarModoColocar()
    {
        cuadrillas.enabled = false;
        modoColocar = false;
    }

    void activarModoColocar()
    {
        cuadrillas.enabled = true;
        modoColocar = true;
    }

}