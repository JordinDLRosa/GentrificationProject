using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayManager : MonoBehaviour {

    // These will be for monitoring the day of the week
    // Will test this later...
    private string[] daysWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    private int currentDayOfTheWeek = 0;
    private int maxDayOfTheWeek = 6;

    // These are the part of the game that will show a life event
    // total life events so far: 5 : so 0 - 4
    System.Random randomEvent = new System.Random();
    private string[] lifeEvent = { "Got into an accident at work, I have to pay medical bills",
        "The toilet got clogged... called a plumber. Have to pay this repair bill",
        "Rodents in the house, had to call an exterminator", "Family called asking for money", "Friend called asking me for money"};
    private int[] lifeEventCost = { 200, 50, 50, 100, 75};
    private bool luckDecidedAlready = true;

    // these are the time and date parts of the game
    public int currentMonth = 04;
    public int currentDay = 25;
    // only need thsese public for access to other scripts
    private int currentYear = 20;
    private int currentHour = 6;
    private float timeStart = 0f;
    private int lastDayOfTheMonth;
    private int lastHourOfTheDay = 11;

    // these are the money parts of the game
    private int savings = 1500;
    private bool paid = true;

    // this will be the part of the game that monitors your health
    private string[] status = { "Normal", "Hungry", "Sick", "Sick & Hungry" };
    private bool eaten = false;
    private int daysHungry = 0;
    private string health;

    // This will be used to manage the stress bars color.
    SpriteRenderer stressBar;


    // This will be the part of the game that monitors the upcoming bills.
    private string billsDisplayed = "";
    private int totalBillsDues = 0;
    private string[] bills = { "Rent $1300", "Gas Bill $50", "Electricity Bill $75", "Phone Bill $75" };
    private List<string> dueBills = new List<string>();
    private int[] livingCost = { 1300, 50, 75, 75 };
    // cost of living for these bills is 1500.
    // Daily food purchase will be 10 dollars so average 10*30 = 300. Total is 1800
    // Monthly MetroCard will be 130, so 2130! You went over budget by 130 if you choose to not make food


    // booleans for upcoming bills need public to access in other scripts.
    public bool rentPaid = false;
    public bool gasPaid = false;
    public bool electricityPaid = false;
    public bool cellPaid = false;
    public bool updateBill = false;
    public bool displayedBills = false;

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
        textBills.text = "None";
    }

    // Update is called once per frame
    void Update() {
        textSavings.text = "Savings: $ " + savings.ToString();
        monitorTime();
        monitorDate();
        stressBarChange();
        monitorPayDay();
        monitorHealth();
        billsPaid();
        addToBills();
        if (updateBill) {
            displayBills();
            updateBill = false;
        }
    }
    private void monitorTime() {
        textTime.text = "Time: " + currentHour + ":" + (Mathf.Round(timeStart) + " pm".ToString());
        // remove this:
        float speedUp = 10;
        //
        timeStart += Time.deltaTime * speedUp;

        if (currentHour >= lastHourOfTheDay) {
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
        textStatus.text = "Status: " + health.ToString();
        if (daysHungry == 0) {
            health = status[0];
        }
        if (daysHungry > 0) {
            // this puts you at hungry
            health = status[1];
        }
        if (daysHungry > 2 & health == status[1]) {
            // this puts you at hungry and sick
            health = status[2];
        }
    }
    private void monitorDate() {
        textDate.text = "Date: " + currentMonth + " / " + currentDay + " / " + currentYear.ToString();
        if (currentMonth == 01) {
            lastDayOfTheMonth = 31;
        }
        if (currentMonth == 02 && currentYear % 4 == 0) {
            lastDayOfTheMonth = 29;
        }
        else {
            lastDayOfTheMonth = 28;
        }
        if (currentMonth == 03) {
            lastDayOfTheMonth = 31;
        }
        if (currentMonth == 04) {
            lastDayOfTheMonth = 30;
        }
        if (currentMonth == 05) {
            lastDayOfTheMonth = 31;
        }
        if (currentMonth == 06) {
            lastDayOfTheMonth = 30;
        }
        if (currentMonth == 07) {
            lastDayOfTheMonth = 31;
        }
        if (currentMonth == 08) {
            lastDayOfTheMonth = 31;
        }
        if (currentMonth == 09) {
            lastDayOfTheMonth = 30;
        }
        if (currentMonth == 10) {
            lastDayOfTheMonth = 31;
        }
        if (currentMonth == 11) {
            lastDayOfTheMonth = 30;
        }
        if (currentMonth == 12) {
            lastDayOfTheMonth = 31;
        }
        // Very Special Condition for next Year
        if (currentDay > lastDayOfTheMonth && currentMonth == 12) {
            currentMonth = 1;
            currentDay = 1;
            currentYear++;
        }
        if (currentDay > lastDayOfTheMonth) {
            currentMonth++;
            currentDay = 1;
        }
    }
    private void monitorPayDay() {
        // ensures you get a single payment
        if (currentDay == 6 || currentDay == 13 || currentDay == 20 || currentDay == 27) {
            paid = false;
            luckDecidedAlready = false;
        }
        // on the 7th it should be payday all day. but you will actually be paid tomorrow
        if (currentDay % 7 == 0 && paid == false) {
            savings += 450;
            paid = true;
        }
    }

    private void addToBills() {
        if (currentMonth == 2 && currentDay == 25 && displayedBills == false) {
            updateBill = true;
            dueBills.Add(bills[0].ToString()); // adds rent to upComingBills from bills[0].
            totalBillsDues += livingCost[0]; // adds rent to the totalBillsDue.

        }
        else if (currentMonth != 2 && currentDay == 26 && displayedBills == false) {
            updateBill = true;
            dueBills.Add(bills[0]); // adds rent to upComingBills from bills[0].
            totalBillsDues += livingCost[0]; // adds rent to the totalBillsDue.
        }
        else {

        }
        if (currentDay == 4 && displayedBills == true) {
            displayedBills = false;
        }
        if (currentDay == 5 && displayedBills == false) {
            updateBill = true;
            dueBills.Add(bills[1]); // adds Gas Bill to upComingBills from bills[1].
            totalBillsDues += livingCost[1]; // adds Gas Bill to the totalBillsDue.
        }
        if (currentDay == 9 && displayedBills == true) {
            displayedBills = false;
        }
        if (currentDay == 10 && displayedBills == false) {
            updateBill = true;
            dueBills[2] = bills[2]; // adds Electricity Bill to upComingBills from bills[2].
            totalBillsDues += livingCost[2]; // adds Electricity Bill to the totalBillsDue.
        }
        if (currentDay == 15 && displayedBills == true) {
            displayedBills = false;
        }
        if (currentDay == 16 && displayedBills == false) {
            updateBill = true;
            dueBills[3] = bills[3]; // adds Phone Bill to upComingBills from bills[3].
            totalBillsDues += livingCost[3]; // adds Phone Bill to the totalBillsDue.
        }
    }
    public void billsPaid() {
        if (currentDay < 2) {
            rentPaid = false;
            gasPaid = false;
            electricityPaid = false;
            cellPaid = false;
        }
        // Forcefully Takes money from your savings account.

        // Last day to Pay your Rent.
        if (currentDay == 5 && rentPaid == false) {
            savings -= 1300;
            rentPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[0], "");
            dueBills.Remove(bills[0]);
            totalBillsDues -= livingCost[0];
            updateBill = true;
        }
        // Last Day to Pay Your Gas Bill, unless we add the ability to manually pay
        // If so will have to code so that if you don't pay this bill by this date.
        // The price will go up by $penalty fee everyday ?
        if (currentDay == 12 && gasPaid == false) {
            savings -= livingCost[1];
            gasPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[1], "");
            totalBillsDues -= livingCost[1];
            updateBill = true;
        }
        // Last Day to Pay Your Electricity Bill, unless we add the ability to manually pay
        // If so will have to code so that if you don't pay this bill by this date.
        // The price will go up by $penalty fee everyday ?
        if (currentDay == 17 && electricityPaid == false) {
            savings -= livingCost[2];
            electricityPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[2], "");
            totalBillsDues -= livingCost[2];
            updateBill = true;
        }
        // Last Day to Pay Your Cell Bill, unless we add the ability to manually pay
        // If so will have to code so that if you don't pay this bill by this date.
        // The price will go up by $penalty fee everyday ?
        if (currentDay == 23 && cellPaid == false) {
            savings -= livingCost[3];
            cellPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[3], "");
            totalBillsDues -= livingCost[3];
            updateBill = true;
        }
    }
    private void displayBills() {
        foreach (string msg in dueBills) {
            billsDisplayed = billsDisplayed + msg.ToString() + "\n";
        }
        textBills.text = billsDisplayed + "\n" + "Total Due: " + totalBillsDues.ToString();
        displayedBills = true;
    }
    private void lifeSucks() {
        if(currentDay % 14 == 0 && luckDecidedAlready == false) {
            int luck = randomEvent.Next(1, 100000);
            int lifeEventHappened = randomEvent.Next(1,6);
            if (luck < 5 && luck > 1) {
                Debug.Log(lifeEvent[lifeEventHappened]);
            }
        }
        luckDecidedAlready = true;
    }
    private void stressBarChange() {
        if (health == "Normal") {
            stressBar.color = Color.white;
        }
        if (health == "Hungry") {
            stressBar.color = Color.yellow;
        }
        if (health == "Sick") {
            stressBar.color = Color.green;
        }
    }

} // end of class
