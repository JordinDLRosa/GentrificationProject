using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rules : MonoBehaviour {
    // Start is called before the first frame update
    string[] months = { "December", "January", "February", "March", "April", "May", "June", "July", "August", "September", "November" };
    int startDate = 020720;
    float ingameTime = 600f;
    float time;

    public float timeStart = 60;
    public Text textBox;

    void Start() {
        textBox.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update() {
        timeStart -= Time.deltaTime;
        textBox.text = Mathf.Round(timeStart).ToString();
    }
}