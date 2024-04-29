using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitEnterCar : MonoBehaviour
{
    [SerializeField] TMP_Text exitCarText;
    [SerializeField] GameObject screen;
    [SerializeField] GameObject car;
    [SerializeField] GameObject playerAvatar;
    [SerializeField] Transform carSpawnPoint;
    [SerializeField] Transform playerSpawnPoint;

    [SerializeField] GameObject playerCarPrefab;
    
    Animator anim;
    public bool enterTrigger = false;
    public bool exitedCar = false;
    GameObject playerCar;
    GameObject playerRef;
    GameObject carModelRef;
    AudioSource audioPlayer;
    
    [SerializeField] AudioClip enterCar;
    [SerializeField] AudioClip exitCar;

    // Start is called before the first frame update
    void Start()
    {
        exitCarText.gameObject.SetActive(false);
        anim = screen.GetComponent<Animator>(); 
        playerCar = FindObjectOfType<CarController>().gameObject;
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enterTrigger == true && Input.GetKeyDown(KeyCode.E) && exitedCar == false)
        {
            exitedCar = true;
            exitCarText.gameObject.SetActive(false);
            StartCoroutine(ExitCar());
            
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<CarController>())
        {
            exitCarText.text = "Press E to park car";
            exitCarText.gameObject.SetActive(true);

            enterTrigger = true;
        }
    }

    void OnTriggerExit(Collider other) {
        
         exitCarText.gameObject.SetActive(false);

    }

    IEnumerator ExitCar()
    {
        anim.SetBool("isFade",true);
        yield return new WaitForSeconds(2);
        audioPlayer.PlayOneShot(exitCar);
        yield return new WaitForSeconds(3);
        Destroy(playerCar);
        playerRef = Instantiate(playerAvatar, playerSpawnPoint.position, Quaternion.identity);
        carModelRef = Instantiate(car, carSpawnPoint.position, car.transform.rotation);
        anim.SetBool("isFade",false);
    }

    public void ExecuteCarEntry()
    {
        StartCoroutine(EnterCar());
    }

    IEnumerator EnterCar()
    {
        anim.SetBool("isFade",true);
        yield return new WaitForSeconds(2);
        audioPlayer.PlayOneShot(enterCar);
        yield return new WaitForSeconds(3);
        Destroy(playerRef);
        Destroy(carModelRef);
        Instantiate(playerCarPrefab, carSpawnPoint.position, car.transform.rotation);
        anim.SetBool("isFade",false);
        Destroy(this);
    }
}
