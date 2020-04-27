using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayManager : MonoBehaviour {

    // These will be for monitoring the date, time, day of the week, month and year.
    // These will be for monitoring the day of the week
    private string[] daysWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    private int currentDayOfTheWeek = 4;

    // these are the time and date parts of the game
    public bool gameFirstDay = true;
    public int currentMonth = 04;
    private int previousMonth;
    private int nextMonth;
    public int currentDay = 30;
    // only need thsese public for access to other scripts
    private int currentYear = 20;
    public int currentHour = 6;
    public float timeStart = 0f;
    private int lastDayOfTheMonth;
    private int lastHourOfTheDay = 11;

    // this will be the part of the game that monitors your emotions and your hunger
    public int daysHungry = 0;
    public bool eaten = false;
    private string[] hungerLevel = { "Hungry: Full", "Hungry: Yes", "Hungry: Very Hungry" };
    public int mealsInFridge = 9;
    private int stressState = 0;
    private int relaxedState = 5;

    // This will be used to manage the stress bars color.
    SpriteRenderer stressBar;

    // This will be part of the game that monitors savings, bills and late fee penalties.
    public int savings = 1500;
    private int minSavings = 100;
    private bool paid = true;
    private bool onceAMonth = true;

    private int monthsBehind = 1; // If months behind rent is equal to 2 you are evicted / lose the game.

    // This will be the part of the game that monitors the upcoming bills.
    private string billsDisplayed = "";
    private string dueBy = " due by: ";
    private int lastDayPayRent = 5;
    private int lastDayPayGas = 12;
    private int lastDayPayElectric = 15;
    private int lastDayPayCell = 23;
    private int totalBillsDues = 0;
    private string[] bills = { "Rent $1300", "Gas Bill $50", "Electricity Bill $75", "Phone Bill $75" };
    public List<string> dueBills = new List<string>();
    private int[] livingCost = { 1300, 50, 75, 75 };
    private int lateFee = 100; // For Rent, For gas / 10. For electricty & Phone / 5.
                               // cost of living for these bills is 1500.
                               // Daily food purchase will be 10 dollars so average 10*30 = 300. Total is 1800
                               // Monthly MetroCard will be 130, so 2130! You went over budget by 130 if you choose to not make food

    [Header("Bool Settings, Leave As If")]
    // booleans for upcoming bills need public to access in other scripts.
    public bool rentPaid = false;
    public bool gasPaid = false;
    public bool electricityPaid = false;
    public bool cellPaid = false;
    public bool updateBill = false;
    public bool displayedBills = false;

    // bools for triggering the auto pay / preventing the bills from paying by themselves
    public bool autoRent = false;
    public bool autoGas = false;
    public bool autoElectric = false;
    public bool autoCell = false;

    float targetAmount;
    [Header("Text Settings")]
    // These are the text objects
    public Text textDate;
    public Text textTime;
    public Text textSavings;
    public Text textBills;
    [SerializeField] Text textHunger;
    [SerializeField] GameObject StressBarBox;
    public Text currentMealsInFridge;

    // Start is called before the first frame update

    MainMenu menu;
    void Start() {
        stressBar = StressBarBox.GetComponent<SpriteRenderer>();
        updateBill = true;
        onceAMonth = false;
    }

    // Update is called once per frame
    void Update() {
        gameOver();
        monitorTime();
        monitorDate();
        monitorHunger();
        stressBarChange();
        monitorPayDay();
        addToBills();
        if (updateBill) {
            displayBills();
            updateBill = false;
        }
    }
    /// <summary>
    /// The Following Code has not been implemented or requires adjustment
    /// </summary>
    private void gameOver() {
        if (monthsBehind == 2) {
            // end the game, because you were evicted.
            menu.QuitGame();
        }
    }
    // Will monitor StressBarChange
    private void stressBarChange() {
        if (rentPaid == false && gasPaid == false && electricityPaid == false && cellPaid == false && gameFirstDay == false) {
            stressBar.color = new Color(255, 0, 0);
        }
        if (savings - totalBillsDues < minSavings) {
            stressBar.color = new Color(240, 51, 0);
        }
        else {
            stressBar.color = new Color(0, 255, 0);
        }
    }
    // monitorTime is bug free now
    private void monitorTime() {
        textTime.text = "Time: " + currentHour + ":" + (Mathf.Round(timeStart) + " pm".ToString());
        float speedUp = 200; // speedUp Time, will adjust for final game
        timeStart += Time.deltaTime * speedUp;
        if (currentHour > lastHourOfTheDay - 1) {
            gameFirstDay = false;
            if (eaten != true) {
                daysHungry++;
            }
            if (currentDayOfTheWeek >= 6) {
                currentDayOfTheWeek = 0;
            }
            else {
                currentDayOfTheWeek++;
            }
            currentHour = 6;
            currentDay++;
            if (eaten == true && currentHour == 6) {
                eaten = false;
            }
        }
        if (timeStart >= 60f) {
            timeStart = 0f;
            currentHour++;
        }
    }
    //monitorHunger is bugFree
    private void monitorHunger() {
        if (daysHungry > 4) {
            textHunger.text = hungerLevel[2];
        }
        else if (daysHungry > 0) {
            textHunger.text = hungerLevel[1];
        }
        else if (daysHungry == 0 && eaten == true) {
            textHunger.text = hungerLevel[0];
        }
        else {
            textHunger.text = hungerLevel[1];
        }
    }
    private void monitorDate() {
        textDate.text = "Date: " + daysWeek[currentDayOfTheWeek] + " " + currentMonth + " / " + currentDay + " / " + currentYear.ToString();

        if (currentMonth == 01) {
            lastDayOfTheMonth = 31;
            previousMonth = 12;
            nextMonth = 2;
        }
        if (currentMonth == 02 && currentYear % 4 == 0) {
            lastDayOfTheMonth = 29;
            previousMonth = 1;
            nextMonth = 3;
        }
        else {
            if (currentMonth == 02) {
                lastDayOfTheMonth = 28;
                previousMonth = 1;
                nextMonth = 3;
            }
        }
        if (currentMonth == 03) {
            lastDayOfTheMonth = 31;
            previousMonth = 2;
            nextMonth = 4;
        }
        if (currentMonth == 04) {
            lastDayOfTheMonth = 30;
            previousMonth = 3;
            nextMonth = 5;
        }
        if (currentMonth == 05) {
            lastDayOfTheMonth = 31;
            previousMonth = 4;
            nextMonth = 6;
        }
        if (currentMonth == 06) {
            lastDayOfTheMonth = 30;
            previousMonth = 5;
            nextMonth = 7;
        }
        if (currentMonth == 07) {
            lastDayOfTheMonth = 31;
            previousMonth = 6;
            nextMonth = 8;
        }
        if (currentMonth == 08) {
            lastDayOfTheMonth = 31;
            previousMonth = 7;
            nextMonth = 9;
        }
        if (currentMonth == 09) {
            lastDayOfTheMonth = 30;
            previousMonth = 8;
            nextMonth = 10;
        }
        if (currentMonth == 10) {
            lastDayOfTheMonth = 31;
            previousMonth = 9;
            nextMonth = 11;
        }
        if (currentMonth == 11) {
            lastDayOfTheMonth = 30;
            previousMonth = 10;
            nextMonth = 12;
        }
        if (currentMonth == 12) {
            lastDayOfTheMonth = 31;
            previousMonth = 11;
            nextMonth = 1;
        }
        // Very Special Condition for end of the year
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
    // monitorPayDay is now bug free
    private void monitorPayDay() { // ensures you get a single payment
        // You get paid every Sunday
        if (currentDayOfTheWeek == 6) {
            paid = false;
        }
        if (currentDayOfTheWeek == 0 && paid == false) {
            savings += 450;
            paid = true;
        }
    }
    // addToBills is now bugFree
    private void addToBills() {
        if (currentDay == lastDayOfTheMonth && onceAMonth == true && gameFirstDay == false) {
            onceAMonth = false;
        }
        if (currentMonth != 2 && currentDay > 26 && displayedBills == false) {
            updateBill = true;
            dueBills.Add(bills[0] + dueBy + nextMonth + "/" + lastDayPayRent.ToString()); // adds rent to upComingBills from bills[0].
            totalBillsDues += livingCost[0]; // adds rent to the totalBillsDue.
        }
        else {
            if (currentMonth == 2 && currentDay > 23 && displayedBills == false) {
                updateBill = true;
                dueBills.Add(bills[0] + dueBy + nextMonth + "/" + lastDayPayRent.ToString()); // adds rent to upComingBills from bills[0].
                totalBillsDues += livingCost[0]; // adds rent to the totalBillsDue.
            }
        }
        if (currentDay == 4 && displayedBills == true) {
            displayedBills = false;
        }
        if (currentDay > 4 && rentPaid == false && gameFirstDay == false) {
            if (onceAMonth == false) {
                monthsBehind++;
            }
        }
        if (currentDay == 5 && displayedBills == false && gasPaid == false) {
            updateBill = true;
            dueBills.Add(bills[1] + dueBy + currentMonth + "/" + lastDayPayGas.ToString()); // adds Gas Bill to upComingBills from bills[1].
            totalBillsDues += livingCost[1]; // adds Gas Bill to the totalBillsDue.
        }
        if (currentDay == 9 && displayedBills == true) {
            displayedBills = false;
        }
        if (currentDay == 10 && displayedBills == false && electricityPaid == false) {
            updateBill = true;
            dueBills.Add(bills[2] + dueBy + currentMonth + "/" + lastDayPayElectric.ToString()); // adds Electricity Bill to upComingBills from bills[2].
            totalBillsDues += livingCost[2]; // adds Electricity Bill to the totalBillsDue.
        }
        if (currentDay == 15 && displayedBills == true) {
            displayedBills = false;
        }
        if (currentDay == 16 && displayedBills == false && cellPaid == false) {
            updateBill = true;
            dueBills.Add(bills[3] + dueBy + currentMonth + "/" + lastDayPayCell.ToString()); // adds Phone Bill to upComingBills from bills[3].
            totalBillsDues += livingCost[3]; // adds Phone Bill to the totalBillsDue.
        }
    }
    //displayBills is now bugFree
    private void displayBills() {
        billsDisplayed = "";
        foreach (string msg in dueBills) {

            billsDisplayed = billsDisplayed + msg.ToString() + "\n";
        }
        textBills.text = billsDisplayed + "\n" + "Total Due: " + totalBillsDues.ToString();
        displayedBills = true;
    }
    // This method is used to pay the bills.
    // payBills is now bugFree
    public void payBills() {
        // prepares the bills to be paid for the following month
        if (currentDay == lastDayOfTheMonth) {
            electricityPaid = false;
            cellPaid = false;
            rentPaid = false;
            gasPaid = false;
        }
        if (rentPaid == false && currentDay > 5 && gameFirstDay == false) {
            savings -= livingCost[0] + lateFee;
            rentPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[0] + dueBy + currentMonth + "/" + lastDayPayRent, "");
            dueBills.Remove(bills[0] + dueBy + currentMonth + "/" + lastDayPayRent.ToString());
            totalBillsDues -= livingCost[0];
            updateBill = true;
        }
        if (rentPaid == false && currentDay > 0 && currentDay < 6) {
            savings -= livingCost[0];
            rentPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[0] + dueBy + currentMonth + "/" + lastDayPayRent, "");
            dueBills.Remove(bills[0] + dueBy + currentMonth + "/" + lastDayPayRent.ToString());
            totalBillsDues -= livingCost[0];
            updateBill = true;
        }
        if (gasPaid == false && currentDay >= 5 && currentDay <= 12) {
            savings -= livingCost[1];
            gasPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[1] + dueBy + currentMonth + "/" + lastDayPayGas.ToString(), "");
            dueBills.Remove(bills[1] + dueBy + currentMonth + "/" + lastDayPayGas.ToString());
            totalBillsDues -= livingCost[1];
            updateBill = true;
        }
        if (gasPaid == false && currentDay > 12 && gameFirstDay == false) {
            savings -= livingCost[1] + lateFee;
            gasPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[1] + dueBy + currentMonth + "/" + lastDayPayGas.ToString(), "");
            dueBills.Remove(bills[1] + dueBy + currentMonth + "/" + lastDayPayGas.ToString());
            totalBillsDues -= livingCost[1];
            updateBill = true;
        }
        if (electricityPaid == false && currentDay >= 10 && currentDay <= 17) {
            savings -= livingCost[2];
            electricityPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[2] + dueBy + currentMonth + "/" + lastDayPayElectric.ToString(), "");
            dueBills.Remove(bills[2] + dueBy + currentMonth + "/" + lastDayPayElectric.ToString());
            totalBillsDues -= livingCost[2];
            updateBill = true;
        }
        if (electricityPaid == false && currentDay > 17 && gameFirstDay == false) {
            savings -= livingCost[2] + lateFee;
            electricityPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[2] + dueBy + currentMonth + "/" + lastDayPayElectric.ToString(), "");
            dueBills.Remove(bills[2] + dueBy + currentMonth + "/" + lastDayPayElectric.ToString());
            totalBillsDues -= livingCost[2];
            updateBill = true;
        }
        if (cellPaid == false && currentDay >= 16 && currentDay <= 23) {
            savings -= livingCost[3];
            cellPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[3] + dueBy + currentMonth + "/" + lastDayPayCell.ToString(), "");
            dueBills.Remove(bills[3] + dueBy + currentMonth + "/" + lastDayPayCell.ToString());
            totalBillsDues -= livingCost[3];
            updateBill = true;
        }
        if (cellPaid == false && currentDay > 23 && gameFirstDay == false) {
            savings -= livingCost[3] + lateFee;
            cellPaid = true;
            billsDisplayed = billsDisplayed.Replace(bills[3] + dueBy + currentMonth + "/" + lastDayPayCell.ToString(), "");
            dueBills.Remove(bills[3] + dueBy + currentMonth + "/" + lastDayPayCell.ToString());
            totalBillsDues -= livingCost[3];
            updateBill = true;
        }
        else {
        }
    }
    public string getBillsDisplay() {
        string billsTransfer = billsDisplayed;
        return billsTransfer;
    }
    public void monitorSavings() {
        textSavings.text = "Savings: $ " + savings.ToString();
    }
} // end of class
