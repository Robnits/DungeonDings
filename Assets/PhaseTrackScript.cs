using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PhaseTrackScript : MonoBehaviour
{
    private int phasen = 0;
    private GameObject player;
    private Animator LevelLoaderAnim;
    public Neeko Neeko;
    public CanvasGroup Healthleiste;
    public GameObject neekoFakePrefab;

    public bool playerIsAllowedToMove = true;
    public Light2D globalLight;

    public AudioSource normalMusic;
    public AudioSource Rennala;

    public List<GameObject> RotationPoints;

    private void Start()
    {
        Healthleiste.alpha = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerIsAllowedToMove = true;
        LevelLoaderAnim = GameObject.Find("Transition").GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && phasen == 0)
        {
            phasen++;
            playerIsAllowedToMove = false;
            StartCoroutine(MovePlayerTowardsNeeko());
        }
    }
    private IEnumerator MovePlayerTowardsNeeko()
    {

        player.transform.position = new Vector3(0, -15, 0);
        while (player.transform.position.y < -2)
        {
            player.GetComponent<Player_behjaviour>().Movement(new Vector2(0, 1));
            yield return null;
        }
        player.GetComponent<Player_behjaviour>().Movement(new Vector2(0, 0));
        Sprechblase();
    }

    private void Sprechblase() //still no movement
    {
        
        StartCoroutine(DunkelHell());
    }

    IEnumerator DunkelHell()
    {
        LevelLoaderAnim.SetTrigger("Start");
        yield return new WaitForSeconds(0.6f);
        globalLight.intensity = 0.01f;
        yield return new WaitForSeconds(1);
        normalMusic.mute = true;
        Rennala.Play();
        NeekoBattlePhase1();
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        playerIsAllowedToMove = true;
        LevelLoaderAnim.SetTrigger("End");
        Healthleiste.alpha = 1;
        globalLight.intensity = 0.8f;
    }
    private void NeekoBattlePhase1()
    {
        SpawnNeekos(15);
        //Neeko clone Spawnen rotieren im uhrzeigersinn
    }

    private void SpawnNeekos(int anzahl)
    {
        for (int i = 0; i < anzahl; i++)
        {
            GameObject neekoClone = Instantiate(neekoFakePrefab, player.transform.position, quaternion.identity);
            neekoClone.GetComponentInChildren<NeekoClones>().StartRotate(RotationPoints[i]);
        }
        Neeko.StartRotate(RotationPoints[15]);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 35 * Time.deltaTime);
    }
}