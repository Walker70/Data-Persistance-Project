using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;
   public  TMP_InputField nameField;
   public Canvas canvas;

   void Start()
   {
    nameField = GameObject.Find("Canvas/NameField").GetComponent<TMP_InputField>();
   }   
   public void Awake()
   {
    if (Instance != null)
    {
        Destroy(gameObject);
        return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
   }

   // Load the main scene    
   public void StartNew()
   {
    canvas.enabled = false;
    SceneManager.LoadScene(1);
    Debug.Log(nameField.text);
   }
    // Exit from the editor playmode or from application
   public void Exit()
   {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif    
   }




}
