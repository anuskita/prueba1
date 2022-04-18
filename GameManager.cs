using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SocialPlatforms.Impl;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private GameObject Player;
    [Tooltip("lo declaramos asi porque es una lista de objetos, una array y se declara con [] ")]
    private GameObject[] bolos;
    
    [Tooltip("estos Vector3 los declaramos para volver a colocar al player y los bolos")]
    private Vector3 playerPosition;
    private Vector3[] bolosPosition;
    
    [Tooltip("para contar las tiradas que tenemos antes de que se acabe el juego")]
    private int roundCount = 0;
    [Tooltip("numero maximo de rondas")]
    private int maxRounds = 5;
    
    private bool count;
    
    //me dice si es la primera tirada
    private bool firstShoot = true;
    //contabiliza los bolos tirados
    private int bolosCounted = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        count = true;
        //Determinamos los objetos y las posiciones tanto del player como de la array de bolos
        //que van a estar relacionados/as con las acciones que programemos
        bolos = GameObject.FindGameObjectsWithTag("Bolo");
        
        Player = GameObject.FindGameObjectWithTag("Player");
        
        playerPosition = Player.transform.position;
        
        bolosPosition = new Vector3[bolos.Length];
        for (int i = 0; i < bolos.Length; i++)
        {
            bolosPosition[i] = bolos[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Si las rondas que llevamos son menores al numero maximo de rondas sigue dejandome tirar y cuenta
        //los bolos para que se acumule la puntuacion
        if (roundCount < maxRounds)
        {
            
        //Con esta linea estamos programamos que los bolos se cuenten o bien dando al space o bien cuando
        //nuestro player llegue a una posicion determinada
            if (Input.GetKeyDown(KeyCode.Return) || Player.transform.position.z >= 4.7f)
            {
                if (count)
                {
                    count = false;
                    Invoke("CountBolosDown", 1f);
                    
                }
            }
        }
        else
        {
            Player.GetComponent<PlayerController>().isShoot = true;
            //Aviso de game over en la consola
            Debug.Log("GAME OVER");
            //El simbolo del dolar es para poner un texto con una variable dentro del texto, le dice
            //a UNITY que el mensaje que tiene que poner en la consola es el texto más la variable ya
            //"calculada"
            Debug.Log($"YOUR SCORE {score}");
            //Debug.Log("YOUR SCORE "+score+"); Esta linea hace lo mismo que la anterior
        }
    } 
    private void CountBolosDown()
    {
        //con esta linea chequeamos que cuente todos los bolos de la array
        for (int i = 0; i < bolos.Length; i++)
        {
            if ((bolos[i].transform.eulerAngles.z > 5f || bolos[i].transform.eulerAngles.z < -5f)
                && bolos[i].activeSelf)
            {
                //Con estas lineas damos un valor de puntuacion por bolo caido y le decimos que cuando lo 
                //cuente desactive el bolo, asi evitamos que contabilice varias veces un mismo bolo
                score += 10;
                bolos[i].SetActive(false);
                //Con esta linea le decimos al programa que cuando se tire un bolo tiene que contar
                //el bolo tirado
                bolosCounted++;
            }
        }
        //mensaje en la consola que nos dice la puntuacion obtenida en la tirada
        Debug.Log("Puntos: " + score);
        
        //si no es primera tirada o he tirado todos los bolos
        if (!firstShoot || bolosCounted == bolos.Length)
        {
            bolosCounted = 0;
            roundCount++;
            Invoke("ResetPlayer", 1.5f);
            Invoke("ResetBolos", 1.5f);
            firstShoot = true;
        }
        else
        {
            //si no es la primera vez que tiro pero no he tirado los diez bolos solo
            //reseteamos la bola
            Invoke("ResetPlayer", 1.5f);
            firstShoot = false;
        }
    }
    //para volver a colocar los bolos en su posicion inicial.
    private void ResetBolos()
    {
        for (int i = 0; i < bolos.Length; i++)
        {
            bolos[i].SetActive(true);
            bolos[i].transform.position = bolosPosition[i];
            bolos[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            bolos[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            bolos[i].transform.rotation=Quaternion.identity; 
        }
    }

    //Para volver a colocar al player en su posicion inicial y que se quede parado y sin girar. La ultima
    //linea es para que nos vuelva a dejar lanzar la bola, es una variable del script PlayerController
    private void ResetPlayer()
    {
        Player.transform.position = playerPosition;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Player.transform.rotation=Quaternion.identity;
        Player.GetComponent<PlayerController>().isShoot = false;
        count = true;
    }
}
