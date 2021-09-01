using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int contador;
    public static int movimientos;
    public Text textoContador;
    public Text textoContMovimientos;
    // Start is called before the first frame update
    void Start()
    {
        contador = 0;
        movimientos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textoContador.text = "x" + contador;
        textoContMovimientos.text = "x" + movimientos;
    }
}
