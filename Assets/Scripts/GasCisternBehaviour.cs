using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GasCisternBehaviour : MonoBehaviour
{
    public bool pickedUpCan = false;

    [SerializeField] TMP_Text fillUpText;
    public CarModelBehaviour carModel;
    bool isNearCistern = false;
    GameObject playerModel;

    AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        fillUpText.gameObject.SetActive(false);
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearCistern && pickedUpCan && Input.GetKeyDown(KeyCode.E))
        {
            carModel = FindObjectOfType<CarModelBehaviour>();
            carModel.hasGas = true;
            fillUpText.gameObject.SetActive(false);
            StartCoroutine(FillUpGas());
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        isNearCistern = true;
        playerModel = other.gameObject;
        if (pickedUpCan)
        {
            fillUpText.text = "Press E to fill up gas can";
            fillUpText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        fillUpText.gameObject.SetActive(false);
        isNearCistern = false;
    }

    IEnumerator FillUpGas()
    {
        playerModel.GetComponent<Rigidbody>().isKinematic = true;
        audioPlayer.Play();
        yield return new WaitForSeconds(5);
        playerModel.GetComponent<Rigidbody>().isKinematic = false;
    }
}