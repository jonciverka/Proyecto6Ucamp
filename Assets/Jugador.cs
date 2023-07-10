using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private bool saltar;
    private float velocidad = 5f;
    public Camara camara;
    public Bala bala;
    private GameObject[] controllerGeneralGameObject;
    public ControllerGeneral controllerGeneral;
    private float cameraPositionX =0;
    private float positionX = 0;
    public AudioSource salto;
    public AudioSource muerto;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        controllerGeneralGameObject = GameObject.FindGameObjectsWithTag("GameController");
        controllerGeneral = controllerGeneralGameObject[0].GetComponent<ControllerGeneral>();
        
    }

    void Update(){
        if(animator.GetBool("Caminando")){
            transform.position += Vector3.right * velocidad * Time.deltaTime;
            camara.avanzar();
            bala.avanzar();
        }else{
            camara.parar();
        }
        if((positionX+3f)>transform.position.x && animator.GetBool("Choque") && animator.GetBool("Caminando")==false){
            transform.position += Vector3.right * velocidad * Time.deltaTime;
        }
        if((positionX+3f)<transform.position.x && animator.GetBool("Choque") && animator.GetBool("Caminando")==false){
           animator.SetBool("Caminando", true);
            animator.SetBool("Choque", false);
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }
        if((cameraPositionX+3f)<camara.getPositionX() && cameraPositionX!=0  && animator.GetBool("Saltando")==false){
            animator.SetBool("Caminando", false);
            animator.SetBool("Choque", true);
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            bala.parar();
            positionX = transform.position.x;
            cameraPositionX = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            if(saltar){
                saltar = false;
                salto.Play();
                rigidbody2D.AddForce( new Vector2(0.0f, 6f) , ForceMode2D.Impulse);
                animator.SetBool("Saltando", true);
            }
        }
         if(Input.touchCount >0){
            if(Input.GetTouch(0).phase == TouchPhase.Began){
                if(saltar){
                    salto.Play();
                    saltar = false;
                    rigidbody2D.AddForce( new Vector2(0.0f, 6f) , ForceMode2D.Impulse);
                    animator.SetBool("Saltando", true);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Piso"){
            animator.SetBool("Caminando", true);
            animator.SetBool("Saltando", false);
            saltar = true;
        }
        if(other.gameObject.tag == "Enemigo"){
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("Caminando", false);
            animator.SetBool("Muerte", true);
            muerto.Play();
            bala.parar();
            rigidbody2D.AddForce( new Vector2(0.0f, 10f) , ForceMode2D.Impulse);
            Destroy(other.gameObject);
            StartCoroutine(morir());
        }
        if(other.gameObject.tag == "PisoEnemigo"){
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("Caminando", false);
            animator.SetBool("Muerte", true);
            muerto.Play();
            bala.parar();
            rigidbody2D.AddForce( new Vector2(0.0f, 10f) , ForceMode2D.Impulse);
            StartCoroutine(morir());
        }
        if(other.gameObject.tag == "Tubo"){
            cameraPositionX = camara.getPositionX();
            animator.SetBool("Saltando", false);
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        cameraPositionX=0;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemigo"){
             print(transform.position.x);
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("Caminando", false);
            animator.SetBool("Muerte", true);
            muerto.Play();
            rigidbody2D.AddForce( new Vector2(0.0f, 10f) , ForceMode2D.Impulse);
            Destroy(other.gameObject);
            StartCoroutine(morir());
        }
    }
   
    IEnumerator morir(){
        yield return new WaitForSeconds(4);
        controllerGeneral.mostrarPantallaFinal();
    }
    
    public float getPosition(){
        return transform.position.y;
    }
}
