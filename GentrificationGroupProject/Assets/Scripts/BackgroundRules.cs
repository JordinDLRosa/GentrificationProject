using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundRules : MonoBehaviour {

    // these are the time and date parts of the game
    private int currentHour = 6;
    private float timeStart = 0f;
    private int currentMonth = 01;
    private int currentDay = 27;
    private int lastDay;
    private int lastHour = 11;

    // these are the money parts of the game
    private int savings = 1000;
    private bool paid = true;

    // this will be the part of the game that monitors your health
    private string[] status = { "sick", "normal", "hungry", "sick & hungry" };
    private bool eaten = false;
    private int daysHungry = 0;
    private string health = "";


    // These are the text objects
    public Text textDate;
    public Text textTime;
    public Text textSavings;
    public Text textStatus;

    // Start is called before the first frame update
    void Start() {
        health = status[1];
    }

    // Update is called once per frame
    void Update() {
        monitorDate();
        monitorTime();
        monitorPayDay();
        monitorHealth();

        textStatus.text = health.ToString();
        textTime.text = currentHour + ":" + (Mathf.Round(timeStart).ToString());
        textDate.text = (currentMonth + "/" + currentDay).ToString();
        textSavings.text = ("$" + savings).ToString();
    }

    private void monitorTime() {
        // remove this:
        float speedUp = 10;
        //
        timeStart += Time.deltaTime * speedUp;


        if (currentHour >= lastHour) {
            currentHour = 6;
            currentDay++;
            if (eaten != true) {
                daysHungry++;
            }
        }
        if (timeStart >= 60f) {
            timeStart = 0f;
            currentHour++;
        }
    }

    private void monitorHealth() {
        if (daysHungry == 0) {
            health = status[1];
        }
        if (daysHungry > 0) {
            // this puts you at hungry
            health = status[2];
        }
        if (daysHungry > 7 & health == status[2]) {
            // this puts you at hungry
            health = status[3];
        }
    }

    private void monitorDate() {
        if (currentMonth == 01) {
            lastDay = 31;
        }
        if (currentMonth == 02) {
            lastDay = 29;
        }
        if (currentMonth == 03) {
            lastDay = 31;
        }
        if (currentMonth == 04) {
            lastDay = 30;
        }
        if (currentMonth == 05) {
            lastDay = 31;
        }
        if (currentMonth == 06) {
            lastDay = 30;
        }
        if (currentMonth == 07) {
            lastDay = 31;
        }
        if (currentMonth == 08) {
            lastDay = 31;
        }
        if (currentMonth == 09) {
            lastDay = 30;
        }
        if (currentMonth == 10) {
            lastDay = 31;
        }
        if (currentMonth == 11) {
            lastDay = 30;
        }
        if (currentMonth == 12) {
            lastDay = 31;
        }

        if (currentDay > lastDay) {
            currentMonth++;
            currentDay = 1;
        }
    }

    private void monitorPayDay() {

        // ensures you get a single payment
        if (currentDay == 6 || currentDay == 13 || currentDay == 20 || currentDay == 27) {
            paid = false;
        }
        // on the 7th it should be payday all day. but you will actually be paid tomorrow
        if (currentDay % 7 == 0 && paid == false) {
            savings += 600;
            paid = true;
        }
    }

} // end of class

