using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarModelBehaviour : MonoBehaviour
{
    public bool hasGas = false;
    [SerializeField] TMP_Text fillUpText;
    [SerializeField] GameObject carLights;
    bool isNearCar = false;
    AudioSource audioPlayer;
    Animator anim;
    ExitEnterCar carEnterRef;
    // Start is called before the first frame update
    void Start()
    {
        fillUpText.gameObject.SetActive(false);
        carEnterRef = FindObjectOfType<ExitEnterCar>();
        fillUpText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TMP_Text>();
        anim = carLights.GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearCar && hasGas && Input.GetKeyDown(KeyCode.E))
        {
            fillUpText.gameObject.SetActive(false);
            carEnterRef.ExecuteCarEntry();
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        isNearCar = true;
        if(hasGas)
        {
            fillUpText.text = "Press E to fill up gas and leave";
            fillUpText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        isNearCar = false;
        fillUpText.gameObject.SetActive(false);
    }

    public void ExecuteCarAlarm()
    {
        StartCoroutine(CarAlarm());
    }

    IEnumerator CarAlarm()
    {
        anim.SetBool("alarmIsOn", true);
        audioPlayer.Play();
        yield return new WaitForSeconds(15f);
        anim.SetBool("alarmIsOn", false);
        audioPlayer.Stop();
    }
    
}
