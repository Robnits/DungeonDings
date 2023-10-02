using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    
    public int openingdirection;
    //1 == need top door
    //2 == need right door
    //3 == need bot door
    //4 == need left door

    private RoomTemplates templates;
    private bool spawned;
    void Start()
    {

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }
    void Spawn(){
        if(templates.begrenzung() <= 10)
        {
            if (!spawned)
            {
                if (openingdirection == 3)
                {
                    int rand = Random.Range(0, templates.toproom.Length);
                    Instantiate(templates.toproom[rand], transform.position, Quaternion.identity);
                }else if (openingdirection == 4)
                {
                    int rand = Random.Range(0, templates.rightrooms.Length);
                    Instantiate(templates.rightrooms[rand], transform.position, Quaternion.identity);

                }else if(openingdirection == 1)
                {
                    int rand = Random.Range(0, templates.botrooms.Length);
                    Instantiate(templates.botrooms[rand], transform.position, Quaternion.identity);

                }else if(openingdirection == 2)
                {
                    int rand = Random.Range(0, templates.leftrooms.Length);
                    Instantiate(templates.leftrooms[rand], transform.position, Quaternion.identity);

                }
                spawned = true;
                templates.begrenzung();
            }
        }else{
            if (!spawned)
            {
                if(openingdirection == 3){
                    Instantiate(templates.endrooms[0], transform.position, Quaternion.identity);
                    spawned = true;
                }
                else if(openingdirection == 4){
                    Instantiate(templates.endrooms[1], transform.position, Quaternion.identity);
                    spawned = true;
                }
                else if(openingdirection == 1){
                    Instantiate(templates.endrooms[2], transform.position, Quaternion.identity);
                    spawned = true;
                }
                else if(openingdirection == 2){
                    Instantiate(templates.endrooms[3], transform.position, Quaternion.identity);
                    spawned = true;
                }
                spawned = true;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RoomSpawner roomSpawner = other.GetComponent<RoomSpawner>();
        if (other.CompareTag("Spawnpoint") == true)
        {
            if (other.GetComponent<RoomSpawner>() == true)
            {
                if (spawned == true)
                {
                    Destroy(gameObject);
                    spawned = true;
                }
            }else{
                Destroy(gameObject);
                spawned = true;

            }
        }  
    }
}
