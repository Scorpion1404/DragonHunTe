using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public int SceneBuilderIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            SceneManager.LoadScene(SceneBuilderIndex, LoadSceneMode.Single);
        }
        
    }
}
