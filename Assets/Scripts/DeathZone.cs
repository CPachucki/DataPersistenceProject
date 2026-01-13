using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Manager.SubtractLife();
        if (Manager.ReturnGameOver())
        {
            Destroy(other.gameObject);
        }
        
        //Manager.GameOver();
    }
}
