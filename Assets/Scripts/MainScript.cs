using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    
    private void Start()
    {
        // Create one brick
        Instantiate(brickPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    void Update()
    {

    }
}
