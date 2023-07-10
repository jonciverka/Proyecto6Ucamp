using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    private GameObject[] controllerGeneralGameObject;
    public ControllerGeneral controllerGeneral;
    public int puntajeLocal;
    // public AudioSource moneda;
    private void Start() {
        controllerGeneralGameObject = GameObject.FindGameObjectsWithTag("GameController");
        controllerGeneral = controllerGeneralGameObject[0].GetComponent<ControllerGeneral>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            controllerGeneral.sumarMonedas();
            // moneda.Play();
            Destroy(gameObject);
        }
    }
}
