using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameField : MonoBehaviour
{
    public static NameField Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
     
    if (Instance != null)
    {
        Destroy(gameObject);
        return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
   
    }
}
