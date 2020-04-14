using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCall : MonoBehaviour
{
  public GameObject uiObject;
    // Start is called before the first frame update
    void Start()
    {
      //unticks UI
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
      if(other.tag == "Player")
      {
        uiObject.SetActive(true);
        Debug.Log("Something");
      }
    }
    void OnTriggerExit(Collider other)
    {
      if(other.tag == "Player")
      {
        Destroy(uiObject);
        Destroy(gameObject);
      }
    }
}
