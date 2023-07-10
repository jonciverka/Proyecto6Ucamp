using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    private float velocidad = 5f;
    private bool esAvanzar = false;


    // Update is called once per frame
    void Update(){
        if(esAvanzar)transform.position +=  Vector3.right * velocidad * Time.deltaTime;
    }
    public void avanzar(){
        esAvanzar = true;
    }
    public void parar(){
        esAvanzar = false;
    }
    public float getPositionX(){
        return transform.position.x;
    }

}
