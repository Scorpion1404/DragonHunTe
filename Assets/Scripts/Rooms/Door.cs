using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour

{
    [SerializeField]private Transform perivousRoom;
    [SerializeField]private Transform nextRoom;
    [SerializeField]private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.tag == "Player"){
            if(collision.transform.position.x < transform.position.x){

                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                perivousRoom.GetComponent<Room>().ActivateRoom(false);

            }
            else{
                cam.MoveToNewRoom(perivousRoom);
                perivousRoom.GetComponent<Room>().ActivateRoom(true);
                nextRoom.GetComponent<Room>().ActivateRoom(false);

            }
        }        
    }

}
