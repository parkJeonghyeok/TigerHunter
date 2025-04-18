using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System;

public class HUD : MonoBehaviour
{
    
    private static HUD instance;

    public static HUD Instance{
        get{
            if(null == instance){
                return null;
            }
            return instance;
        }
    } 

    public GameObject CanvasUI;
    public GameObject StatusUI;
    bool statusActive = false;
   //public GameObject HpBar;

    private void Awake()
    {
        // 싱글톤 구현
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)){
            statusActive = !statusActive;
            StatusUI.SetActive(statusActive);
        }
    }

    public void OnclickClose(){
        if (StatusUI.activeSelf){
            StatusUI.SetActive(false);
        }
        
    }
}
