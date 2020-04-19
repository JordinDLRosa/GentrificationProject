using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInteraction : MonoBehaviour
{

    private void Update()
    {
      GetObject();
    }
    private void GetObject()
    {

      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Input.GetMouseButtonDown(0))
      if (Physics.Raycast(ray, out hit, 100.0f))
      {
        if (hit.transform != null)
        {
          DisplayObject(hit.transform.gameObject);
        }
      }
    }

    private void DisplayObject(GameObject go)
    {
      print(go.name);
    }
}
