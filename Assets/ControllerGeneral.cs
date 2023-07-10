using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ControllerGeneral : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] escenarios;
    public List<string> escenariosActuales;
    private int monedas = 0;
    public TMP_Text textoPuntuacion;
    // public TMP_Text textoPuntuacionFinal;
    public GameObject gameOver;
    public GameObject pausePantalla;
    public AudioSource moneda;

    void Start()
    {
        var escenarioAux = GameObject.Find("Escenario1");
        escenarioAux.name = "Escenario0";
        escenariosActuales.Add(escenarioAux.name);
    }

    // Update is called once per frame
    void Update()
    {
       textoPuntuacion.text =  monedas.ToString();
    //    textoPuntuacionFinal.text =  monedas.ToString();
    }

    public void sumarMonedas(){
        moneda.Play();
        monedas += 1;
    }

    public void instanceNewEscenario(){
        var respawnEscenario = GameObject.FindGameObjectsWithTag("Respawn");
        var my_spawned = Instantiate(escenarios[Random.Range(0, 3)],respawnEscenario[0].transform.position,respawnEscenario[0].transform.rotation);
        my_spawned.name = "Excenario"+(escenariosActuales.Count);
        escenariosActuales.Add(my_spawned.name);
        Destroy(respawnEscenario[0]);
        pool();
    }

    public void pool(){
        if(escenariosActuales.Count>2){
             var escenarioAux = GameObject.Find(escenariosActuales[0]);
             Destroy(escenarioAux);
            escenariosActuales.RemoveAt(0);
        }
    }
    public void mostrarPantallaFinal(){
        gameOver.SetActive(true);
    }

    public void reiniciar(){
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public void pause(){
       Time.timeScale = 0;
       pausePantalla.SetActive(true);
    }
    public void continuar(){
        Time.timeScale = 1;
        pausePantalla.SetActive(false);
    }



}
