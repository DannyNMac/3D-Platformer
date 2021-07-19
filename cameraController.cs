using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance;

    public CinemachineBrain theCMBrain;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
