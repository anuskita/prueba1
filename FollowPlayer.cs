using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{ 
    [Tooltip("para que la camara siga al player a la distancia especificada")]
    public GameObject player;
    [Tooltip("Vector que indica la distancia que dejamos entre la camara y el player")]
    private Vector3 offset = new Vector3(0f, 0.497f, -1.7f);
    
    [Tooltip("para que la camara gire derecha/izquierda utilizando para ello el movimiento del raton")]
    public float mouseSensitivity;
    private float yRotation;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //para que la camara gire de derecha/izquierda
        float mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //transform.Rotate(Vector3.up * mouseY); es la forma sencilla, sin limitar el angulo de giro
        yRotation += mouseY;
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        transform.localRotation=Quaternion.Euler(0,yRotation,0);
        
        //Con esto conseguimos que la camara siga al player a una distancia Vector3 declarado arriba
        transform.position = player.transform.position + offset;
    }
    
}
