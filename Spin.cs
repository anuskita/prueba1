using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [Tooltip("Velocidad de rotacion"), Range (0,180)]
    public float rotateSpeed = 60f;
    public float translateSpeed = 1f;
    private PlayerController _playerController;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        
    }
}
