using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathEvent : MonoBehaviour
{

    [SerializeField] AudioClip carStop;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text instructionText;
    [SerializeField] GameObject screen;
    [SerializeField] GameObject playerAvatar;
    [SerializeField] GameObject car;
    [SerializeField] GameObject enemyAI;
    [SerializeField] Transform enemySpawnPoint;
    public AudioSource carAudio;
    GameObject playerCar;
    Animator anim;
    Rigidbody carRB;

    GameObject playerRef;
    GameObject enemy;
    EnemyChase enemyai;
    bool carStopped = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogueText = GameObject.Find("Canvas").transform.GetChild(3).GetComponent<TMP_Text>();
        instructionText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TMP_Text>();
        screen = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        anim = screen.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carStopped && Input.GetKeyDown(KeyCode.E))
        {
            instructionText.gameObject.SetActive(false);
            carStopped = false;
            StartCoroutine(CarLeave());
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        
        if (other.tag == "Car")
        {
            playerCar = other.gameObject;
            carRB = other.gameObject.GetComponent<Rigidbody>();
            carAudio = other.gameObject.GetComponent<AudioSource>();
            StartCoroutine(CarStop());
        }
    }

    IEnumerator CarStop()
    {
        carAudio.clip = carStop;
        carAudio.loop = false;
        carAudio.Play();
        yield return new WaitForSeconds(2f);
        carRB.isKinematic = true;
        yield return new WaitForSeconds(1f);
        dialogueText.text = "Shit. The car's busted. Gotta walk back to the gas station and get help.";
        dialogueText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        dialogueText.gameObject.SetActive(false);
        instructionText.text = "Press E to exit car";
        instructionText.gameObject.SetActive(true);
        carStopped = true;
    }

    IEnumerator CarLeave()
    {
        anim.SetBool("isFade",true);
        yield return new WaitForSeconds(5);
        playerRef = Instantiate(playerAvatar, transform.position, Quaternion.identity);
        Instantiate(car, playerCar.transform.position, playerCar.transform.rotation);
        Destroy(playerCar);
        anim.SetBool("isFade",false);
        enemy = Instantiate(enemyAI, enemySpawnPoint.position, Quaternion.identity);
        enemyai = GetComponent<EnemyChase>();
        //enemyai.SetTarget(playerRef);

    }
}
