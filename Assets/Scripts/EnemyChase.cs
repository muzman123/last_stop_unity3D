using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

    public GameObject player;
    GameObject playerParent;
    public GameObject screen;
    public GameObject videoScreen;
    public GameObject videoPlayer;
    Transform tr_Player;
    [SerializeField] AudioClip scream;
    [SerializeField] float distanceForScream;
    AudioSource audioPlayer;

    private Vector3 _velocity;
    private NavMeshAgent enemy;
    bool screamNoise = true;

 // Use this for initialization
    void Start () 
    {
        enemy = GetComponent<NavMeshAgent>();
        audioPlayer = GetComponent<AudioSource>();
        videoScreen = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        videoPlayer = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
    }
    
    // Update is called once per frame
    void Update () {
        //player = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject;
        //tr_Player = player.transform;
        /*Vector3 direction = tr_Player.position - transform.position;
        direction.Normalize();
        direction.y = 0;
        Quaternion fastlook = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, fastlook, Time.deltaTime*3);
        _velocity = direction * f_MoveSpeed;

        enemyCC.Move(_velocity * Time.deltaTime);*/

        enemy.SetDestination(tr_Player.position);

        if(Vector3.Distance(transform.position, tr_Player.position) < distanceForScream && screamNoise)
        {
            Debug.Log("screaams");
            audioPlayer.PlayOneShot(scream, 1);
            screamNoise = false;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        StartCoroutine(KillPlayer());
        
    }

    public void SetTarget(GameObject target)
    {
        Debug.Log("ran");
        player = target.transform.GetChild(0).gameObject;
        tr_Player = player.transform;
    }

    IEnumerator KillPlayer()
    {
        audioPlayer.Stop();
        //player.GetComponent<Rigidbody>().isKinematic = true;
        videoScreen.SetActive(true);
        videoPlayer.SetActive(true);
        yield return new WaitForSeconds(4);
        videoScreen.SetActive(false);
        videoPlayer.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
