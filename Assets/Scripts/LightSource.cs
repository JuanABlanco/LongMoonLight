using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
 

[RequireComponent(typeof(LineRenderer))]
public class LightSource : MonoBehaviour
{

    public TilemapRenderer cuadrillas;

    public LineRenderer LR;

    public Transform Foco;

    public List<Vector2> Puntos;

    public int BounceLimit = 2;

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
        GameObject LastPosition = Foco.transform.parent.gameObject;

        for (int i = 0; i < BounceLimit; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(Inicio, Direccion, 100f);

            if (hit)
            {
                if (hit.transform.CompareTag("Espejo"))
                {
                    if (!GameObject.Equals(LastPosition, hit.transform.gameObject))
                    {
                        Posiciones.Add(hit.point);
                        LastPosition = hit.transform.gameObject;
                        Inicio = hit.point;
                        Direccion = -hit.transform.up;
                    }
                    else
                    {
                        Inicio = hit.transform.parent.Find("PivoteLuz").transform.position;
                        Direccion = -hit.transform.up;
                    }
                }
                else
                {
                    if(hit.transform.CompareTag("Portal")){
                        Posiciones.Add(hit.point);
                        Debug.Log("Le pegue al portal");
                        if(cuadrillas.enabled == false){
                            StartCoroutine(ganar());
                        }
                        
                    }
                    Posiciones.Add(hit.point);
                    break;
                }
            }
            else
            {
                Posiciones.Add(Direccion * 1000f);
                break;
            }
        }

        LR.positionCount = Posiciones.Count;

        for (int i = 0; i < Posiciones.Count; i++)
        {
            LR.SetPosition(i, Posiciones[i]);
        }
    }

    public IEnumerator ganar(){
        yield return new WaitForSeconds(0.5f);//era 0.05f
        bool var = EditorUtility.DisplayDialog("Información", "Enhorabuena ! Has completado el level 1 y único nivel de la beta.\nEl record registrado es de 4 espejos 1 movimiento, lo has superado? \nPerdona los posibles bugs, el juego esta en desarrollo.", "Salir", "Salir");
        if(var){
            Application.LoadLevel("Inicio");
        }else{
            Application.LoadLevel("Inicio");
        }
    }
}
