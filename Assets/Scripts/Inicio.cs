using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inicio : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Awake(){
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jugar(){
        Application.LoadLevel("SampleScene");
    }

    public void tutorial(){
        Application.LoadLevel("Tuto");
    }

    public void acercaDe(){

        bool var = EditorUtility.DisplayDialog("Información",
        "Desarrolladores: Juan Blanco, Carlos Morales. \nDiseño: Carlos Morales (tambien imagenes libres). \nMúsica: Carlos Martinez (también sonidos libres). \nJuego en fase beta, por lo que esta incompleto.", "Volver", "Volver");

        if (var){
            Application.LoadLevel("Inicio");
        }else{

        };

    }

    public void historia(){
        bool var = EditorUtility.DisplayDialog("Información",
        "No disponible!", "Volver", "Volver");

        if (var){
            Application.LoadLevel("Inicio");
        }else{

        };        
    }    
}
