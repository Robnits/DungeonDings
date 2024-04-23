using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PhaseTrackScript : MonoBehaviour
{
    public int phasen = 0;
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

    private CircleCollider2D startFight;
    private void Start()
    {
        Healthleiste.alpha = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerIsAllowedToMove = true;
        LevelLoaderAnim = GameObject.Find("Transition").GetComponentInChildren<Animator>();
        startFight = GetComponent<CircleCollider2D>();
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
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && phasen == 1)
            player.transform.position = gameObject.transform.position;
    }
    private IEnumerator MovePlayerTowardsNeeko()
    {

        player.transform.position = new Vector3(0, -15, 0);
        while (player.transform.position.y < -1.3f)
        {
            player.GetComponent<PlayerBehaviour>().Movement(0, 1);
            yield return null;
        }
        player.GetComponent<PlayerBehaviour>().Movement(0, 0);
        Sprechblase();
    }

    private void Sprechblase()
    {
        StartCoroutine(Neeko.Sprechblaseninhalt("Wie hast du es hierher geschafft?", 3));
        StartCoroutine(DunkelHell());
    }

    IEnumerator DunkelHell()
    {
        yield return new WaitForSeconds(3);
        LevelLoaderAnim.SetTrigger("Start");
        yield return new WaitForSeconds(0.6f);
        globalLight.intensity = 0.01f;
        yield return new WaitForSeconds(1);
        normalMusic.mute = true;
        Rennala.Play();
        NeekoBattlePhase1();
        yield return new WaitForSeconds(2);
        //yield return new WaitForSeconds();
        playerIsAllowedToMove = true;
        startFight.radius = 5;
        globalLight.intensity = 0.8f;
        LevelLoaderAnim.SetTrigger("End");
    }

    public IEnumerator PhaseChange(){
        phasen = 2;
        playerIsAllowedToMove = false;
        player.transform.position = new Vector2(transform.position.x, transform.position.y - 2);
        LevelLoaderAnim.SetTrigger("Start");
        StartCoroutine(Loadhealthbar());
        yield return new WaitForSeconds(2);
        
    }

    private IEnumerator Loadhealthbar()
    {
        Neeko.maxlife = 50;
        while (Neeko.life < Neeko.maxlife){
            Neeko.life += 0.25f;
            yield return new WaitForSeconds(0.01f);
            Neeko.SetHealthbar();
        }
        LevelLoaderAnim.SetTrigger("End");
        Neeko.talkIsOver = true;
        playerIsAllowedToMove = true;
        
    }

    private void NeekoBattlePhase1()
    {
        SpawnNeekos(15);
    }

    private void SpawnNeekos(int anzahl)
    {
        for (int i = 0; i < anzahl; i++)
        {
            GameObject neekoClone = Instantiate(neekoFakePrefab, player.transform.position, quaternion.identity);
            neekoClone.GetComponentInChildren<NeekoClones>().StartRotate(RotationPoints[i], phasen);
        }
        Neeko.StartRotate(RotationPoints[15], phasen);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 35 * Time.deltaTime);
    }
}