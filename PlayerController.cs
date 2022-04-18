using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip ("Velocidad a la que me muevo derecha/izquierda"), Range (0,20)]
    public float speed = 10f;
    
    private float horizontalInput;
    
    [Tooltip("Limite en eje x para que no se salga de la pista la bola")]
    private float xRange = 0.5f;
    [Tooltip("Limite en eje z para que no se salga de la pista la bola")]
    private float zRange = 4.95f;
    
    [Tooltip("Fuerza con la que lanzo la bola"), Range (0,100)]
    public float impulseForce = 50f;

    private Rigidbody _rigidbody;

    public bool isShoot;

    public Camera camera;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isShoot)
        {
            _rigidbody.AddForce(camera.transform.forward*impulseForce, ForceMode.Impulse);
            isShoot = true;
        }
        
    }
}
