using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightSource : MonoBehaviour
{
    public LineRenderer LR;

    public Transform Foco;

    public List<Vector2> Puntos;

    public int BounceLimit = 2;

    private Vector2 LastPosition;

    public List<Vector2> Posiciones = new List<Vector2>();

    private void Awake()
    {
        LR = transform.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        CastRay(Foco.position, Foco.up);
    }

    private void CastRay(Vector2 Inicio, Vector2 Direccion)
    {
        Posiciones = new List<Vector2>();
        Posiciones.Add(Foco.position);
        LastPosition = Foco.position;

        for (int i=0; i<BounceLimit; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(Inicio, Direccion, 100f);

            if (hit)
            {
                if (hit.transform.CompareTag("Espejo"))
                {
                    if(hit.point != LastPosition)
                    {
                        Posiciones.Add(hit.point); 
                        LastPosition = hit.point;
                        Inicio = hit.point;
                        Direccion = Vector2.Reflect(Direccion, hit.normal);
                    }
                } else
                {
                    Posiciones.Add(hit.point);
                    break;
                }
            } else
            {
                Posiciones.Add(Direccion * 1000f);
                break;
            }
        }

        LR.positionCount = Posiciones.Count;
        
        for(int i = 0; i<Posiciones.Count; i++)
        {
            LR.SetPosition(i, Posiciones[i]);
        }
    }
}
