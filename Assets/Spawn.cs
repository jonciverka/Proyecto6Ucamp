using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private GameObject[] controllerGeneralGameObject;
    private ControllerGeneral controllerGeneral;
    private void Start() {
        controllerGeneralGameObject = GameObject.FindGameObjectsWithTag("GameController");
        controllerGeneral = controllerGeneralGameObject[0].GetComponent<ControllerGeneral>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            controllerGeneral.instanceNewEscenario();
        }
    }
}
