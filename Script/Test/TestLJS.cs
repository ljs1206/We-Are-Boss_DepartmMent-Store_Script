using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestLJS : MonoBehaviour
{
    public void Update(){
        if(Keyboard.current.zKey.wasPressedThisFrame){
            EventManager.Instance.HappenUniqueEvent();
        }
    }
}
