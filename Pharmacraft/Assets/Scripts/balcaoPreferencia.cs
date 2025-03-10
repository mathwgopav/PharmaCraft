using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class balcaoPreferencia : MonoBehaviour
{
    public QueueManager fila;
    private bool playerNearby = false;
    public TMPro.TextMeshPro scoreText;

    public GameObject remedio;

    public GameObject label;


    private void Start()
    {
        label = transform.GetChild(0).gameObject;
    }

    void updateCurrency(int value)
    {
        fila.money += value;
        scoreText.text = fila.money.ToString();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            remedio = other.transform.gameObject.GetComponent<GrabDetecter>().GetHeldItem();
            
            label.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            label.SetActive(false);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            EntregarRemédio();
        }
    }

    void EntregarRemédio()
    {
        if(fila.priorityQueueSize() > 0 && remedio){
            remedio.transform.parent = fila.priorityTop().transform;
            int value = fila.priorityTop().GetComponent<Cliente>().validRecepy(remedio.GetComponent<Recepy>().recepy);

            if(value <= 10){
                // Audio remedio errado
            }else{
                // Audio remedio certo
            }

            fila.priorityTop().GetComponent<Cliente>().sucesso = true;
            fila.RemoverClientePrioridade();

            Destroy(remedio);
            remedio = null;
            updateCurrency(value);
            if(value >= 150){
                SceneManager.LoadScene("Vitoria Tutorial");
            }
        }
        
    }
}
