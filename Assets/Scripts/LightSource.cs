using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightSource : MonoBehaviour
{
    public LineRenderer LR;
    public Transform Foco;
    public List<Vector2> Puntos;
    public Vector2 Direccion;
    private void Awake()
    {
        LR = transform.GetComponent<LineRenderer>();
    }

    void Update()
    {
        StartCoroutine(CalculateReflect());
    }

    private IEnumerator CalculateReflect()
    {
        GetPoints();
        yield return new WaitForSeconds(0.5f);
        DrawLine();
    }
    private void DrawLine()
    {
        LR.positionCount = Puntos.Count;
        for (int i = 0; i < Puntos.Count; i++)
        {
            LR.SetPosition(i, Puntos[i]);
        }
    }

    private void GetPoints()
    {
        Puntos = new List<Vector2>();

        Puntos.Add(new Vector2(Foco.position.x, Foco.position.y));
        Direccion = transform.up;

        bool Choque = false;

        while (!Choque)
        {
            //Revisamos la direccion con respecto al ultimo cuerpo
            RaycastHit2D hit = Physics2D.Raycast(Puntos[Puntos.Count - 1], Direccion,1000f);

            //Si encuentra contra que tocar y es un espejo entonces agrega el punto y rebota, si encuentra algo contra que tocar y no es espejo  agrega el punto y choca, y si no encuentra contra que chocar choca en el "infinito"
            if(hit && hit.transform.CompareTag("Espejo"))
            {
                Vector2 Punto = new Vector2(hit.point.x,hit.point.y);
                Puntos.Add(Punto);
                Direccion = Vector2.Reflect(Direccion, hit.normal).normalized;
            } else if (hit)
            {
                Vector2 Punto = new Vector2(hit.point.x, hit.point.y);
                Puntos.Add(Punto);
                Choque = true;
            } else
            {
                Vector2 Punto = Direccion * 10000f;
                Puntos.Add(Punto);
                Choque = true;
            }
        }
    }
}
