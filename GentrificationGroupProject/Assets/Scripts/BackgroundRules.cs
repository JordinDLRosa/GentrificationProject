using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundRules : MonoBehaviour {

    // these are the time and date parts of the game
    private int currentMonth = 12;
    private int currentDay = 31;
    private int currentYear = 19;
    private int currentHour = 6;
    private float timeStart = 0f;
    private int lastDay;
    private int lastHour = 11;

    // these are the money parts of the game
    private int savings = 1000;
    private bool paid = true;

    // this will be the part of the game that monitors your health
    private string[] status = { "Sick", "Normal", "Hungry", "Sick & Hungry"};
    private bool eaten = false;
    private int daysHungry = 0;
    private string health;

    // This will be used to manage the stress bars color.
    SpriteRenderer stressBar;


    // This will be the part of the game that monitors the upcoming bills.

    private string[] bills = {"Rent", "Gas Bill", "Electricity Bill", "Phone Bill"};


    private string[] upcomingBills = { };
    private int[] costLiving = {600, 50, 100, 100};
    private bool rentPaid = false;
    private bool gasPaid = false;
    private bool electricityPaid = false;
    private bool cellPaid = false;


    // These are the text objects
    public Text textDate;
    public Text textTime;
    public Text textSavings;
    public Text textStatus;
    public Text textBills;
    public GameObject StressBarBox;

    // Start is called before the first frame update
    void Start() {
        health = status[1];
        stressBar = StressBarBox.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        stressBar.color = Color.black;
        monitorDate();
        monitorTime();
        monitorPayDay();
        monitorHealth();
        billsPaid();

        textStatus.text = "Health: " + health.ToString();
        //textBills.text = "Upcoming Bills: " + upcomingBills.ToString();
        textTime.text = "Time: " + currentHour + ":" + (Mathf.Round(timeStart) + " pm".ToString());
        textDate.text = "Date: " + currentMonth + " / " + currentDay + " / " + currentYear.ToString();
        textSavings.text = "Savings: $ " + savings.ToString();
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
        if (daysHungry > 2 & health == status[2]) {
            // this puts you at hungry and sick
            health = status[3];
        }
    }

    private void monitorDate() {
        if (currentDay > lastDay && currentMonth == 12) {
            currentMonth = 1;
            currentDay = 1;
            currentYear++;
        }
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

    private void billsPaid() {
        if (currentDay == 1) {
            rentPaid = false;
            gasPaid = false;
            electricityPaid = false;
            cellPaid = false;
        }
        if(currentDay == 28) {
           // upcomingBills[0] = bills[0];
        }

        if(currentDay >= 3 && rentPaid == false) {
            savings -= 600;
            rentPaid = true;
        }
        if (currentDay >= 12 && gasPaid == false) {
            savings -= costLiving[1];
            gasPaid = true;
        }
        if (currentDay >= 13 && electricityPaid == false) {
            savings -= costLiving[2];
            electricityPaid = true;
        }
        if (currentDay >= 15 && cellPaid == false) {
            savings -= costLiving[3];
            cellPaid = true;
        }


    }

} // end of class
