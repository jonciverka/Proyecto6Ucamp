using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private bool esAvanzar = false;
    private float velocidad = 5f;
    
    public Jugador jugador;

    private void Start() {
        StartCoroutine(corrutina());
    }
    IEnumerator corrutina()
    {
         while(true){
            yield return new WaitForSeconds(1);
            transform.position = new Vector3(transform.position.x,jugador.getPosition(),0) ;
        }
    }
    void Update(){
        if(esAvanzar){
            transform.position +=  new Vector3(1,0,0) * velocidad * Time.deltaTime;
        }
    }
    public void avanzar(){
        esAvanzar = true;
    }
    public void parar(){
        esAvanzar = false;
    }
}
