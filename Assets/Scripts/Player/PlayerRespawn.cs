using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;
    private Transform currentCheckPoint;
    private Health playerhealth;
    private UiManager uiManagers;


    private void Awake()
    {
        playerhealth = GetComponent<Health>();
        uiManagers = FindObjectOfType<UiManager>();

    }

    public void CheckRespawn()
    {
        if(currentCheckPoint == null)
        {
            uiManagers.GameOver();
            return;
        }
        transform.position = currentCheckPoint.position;
        playerhealth.Respawn();

        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckPoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckPoint = collision.transform;
            SoundManager.instance.PlaySound(checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
