using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpCan : MonoBehaviour
{
    [SerializeField] CarModelBehaviour carModel;
    [SerializeField] TMP_Text PickUpText;
    [SerializeField] GameObject afterAlarmDialogue;
    GasCisternBehaviour gasCisternRef;
    GameObject playerRef;
    bool nearCan = false;
    // Start is called before the first frame update
    void Start()
    {
        PickUpText.gameObject.SetActive(false);
        gasCisternRef = FindObjectOfType<GasCisternBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(nearCan == true && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CarAlarm());
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        playerRef = other.gameObject;
        PickUpText.text = "Press E to pick up gas can";
        PickUpText.gameObject.SetActive(true);
        nearCan = true;
    }

    void OnTriggerExit(Collider other) 
    {
        PickUpText.gameObject.SetActive(false);
        nearCan = false;

    }

    IEnumerator CarAlarm()
    {
        gasCisternRef.pickedUpCan = true;
        carModel = FindObjectOfType<CarModelBehaviour>();
        carModel.ExecuteCarAlarm();
        PickUpText.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        afterAlarmDialogue.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
