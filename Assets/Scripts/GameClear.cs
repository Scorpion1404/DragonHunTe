using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{

    private UiGameCllear uiGameclear;


    private void Awake()
    {
        uiGameclear= FindAnyObjectByType<UiGameCllear>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            uiGameclear.GameClear();
        }
    }
}
