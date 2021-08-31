using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource AS;

    public AudioClip BotonRotacion;

    public AudioClip BotonMover;

    public AudioClip BotonEliminar;

    public AudioClip BotonCrear;

    public AudioClip BotonSeleccionar;

    public AudioClip Poner;

    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    private void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    public void BotonRotacionFX()
    {
        AS.PlayOneShot(BotonRotacion);
    }

    public void BotonMoverFX()
    {
        AS.PlayOneShot(BotonMover);
    }

    public void BotonEliminarFX()
    {
        AS.PlayOneShot(BotonEliminar);
    }

    public void BotonCrearFX()
    {
        AS.PlayOneShot(BotonCrear);
    }

    public void PonerFX()
    {
        AS.PlayOneShot(Poner);
    }

    public void BotonSeleccionarFX()
    {
        AS.PlayOneShot(BotonSeleccionar);
    }
}
