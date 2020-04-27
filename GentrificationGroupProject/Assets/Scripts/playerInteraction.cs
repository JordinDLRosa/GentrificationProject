using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInteraction : MonoBehaviour {

    private GamePlayManager gameManagerScript;
    public GameObject uiObjectDate;
    public GameObject uiObjectTime;
    public Text paidBills;
    public Text textAlerts;
    private string[] alerts = { "Rent is coming up. \n I should check my book to see the due date and the amount due",
    "The gas bill is coming up. \n I should check my book to see the due date and the amount due",
    "The electricity bill is coming up. \n I should check my book to see the due date and the amount due",
    "The cell bill is coming up. \n I should check my book to see the due date and the amount due"
    };


    float turnOff = 0f;
    private void Awake() {
        gameManagerScript = GameObject.FindObjectOfType<GamePlayManager>();
    }
    private void start() {
        gameManagerScript.textSavings.enabled = false;
        gameManagerScript.textDate.enabled = false;
        gameManagerScript.textTime.enabled = false;
        gameManagerScript.textBills.enabled = false;
        textAlerts.enabled = false;
    }
    private void Update() {
        GetObject();
        alertUpdates();

        turnOff += Time.deltaTime;
        if (turnOff >= 2f) {
            textAlerts.enabled = false;
        }
    }
    private void alertUpdates() {
        if (gameManagerScript.rentPaid == false && gameManagerScript.currentDay > 25 && gameManagerScript.currentHour == 6 && gameManagerScript.timeStart < 2f) {
            textAlerts.enabled = true;
            textAlerts.text = alerts[0];
            turnOff = 0f;
        }
        if (gameManagerScript.rentPaid == false && gameManagerScript.currentDay < 4 && gameManagerScript.currentHour == 6 && gameManagerScript.timeStart < 2f) {
            textAlerts.enabled = true;
            textAlerts.text = alerts[0];
            turnOff = 0f;
        }
        if (gameManagerScript.gameFirstDay != true && gameManagerScript.gasPaid == false && gameManagerScript.currentDay > 4 && gameManagerScript.currentHour == 6 && gameManagerScript.timeStart < 2f) {
            textAlerts.enabled = true;
            textAlerts.text = alerts[1];
            turnOff = 0f;
        }
        if (gameManagerScript.gameFirstDay != true && gameManagerScript.electricityPaid == false && gameManagerScript.currentDay > 10 && gameManagerScript.currentHour == 6 && gameManagerScript.timeStart < 2f) {
            textAlerts.enabled = true;
            textAlerts.text = alerts[2];
            turnOff = 0f;
        }
        if (gameManagerScript.gameFirstDay != true && gameManagerScript.cellPaid == false && gameManagerScript.currentDay > 15 && gameManagerScript.currentHour == 6 && gameManagerScript.timeStart < 2f) {
            textAlerts.enabled = true;
            textAlerts.text = alerts[3];
            turnOff = 0f;
        }
        if (gameManagerScript.daysHungry > 2) {
            textAlerts.enabled = true;
            textAlerts.text = "I should eat, I haven't eaten in " + gameManagerScript.daysHungry + "days.";
            turnOff = 0f;

        }
    }
    private void GetObject() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform != null) {
                    if (hit.collider.gameObject.tag == "Wallet") {
                        DisplayObject(hit.transform.gameObject);
                        gameManagerScript.monitorSavings();
                        gameManagerScript.textSavings.enabled = true;
                        StartCoroutine(WaitForSec());

                    }
                    if (hit.collider.gameObject.tag == "ComputerForNow") {
                        DisplayObject(hit.transform.gameObject);
                        gameManagerScript.payBills();
                        gameManagerScript.textBills.enabled = true;
                        StartCoroutine(WaitForSec());
                        if (gameManagerScript.getBillsDisplay() == "") {

                            paidBills.text = "No Bills to pay";
                        }
                        else {
                            paidBills.text = gameManagerScript.getBillsDisplay();
                        }
                    }
                    if (hit.collider.gameObject.tag == "Telephone") {
                        DisplayObject(hit.transform.gameObject);
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour > 9) {
                            print("It's too late to eat. I won't get enough rest for work tomorrow.");
                        }
                        if (gameManagerScript.eaten == true) {
                            print("I am too full to eat");
                        }
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour < 10) {
                            gameManagerScript.currentHour += 1; // this is just whats supposed to happen. It should lock you out of things until you are done eating.
                            gameManagerScript.eaten = true;
                            gameManagerScript.daysHungry = 0;
                            gameManagerScript.savings -= 10;
                            print("You ordered food!");
                        }
                    }
                    if (hit.collider.gameObject.tag == "StoveForNow") {
                        DisplayObject(hit.transform.gameObject);
                        if (gameManagerScript.eaten == true) {
                            print("I am too full to eat");
                        }
                        // checks to make sure you havent eaten, and that it's not too late to eat.
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour > 9) {
                            print("It's too late to eat. I won't get enough rest for work tomorrow.");
                        }
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour < 10 && gameManagerScript.mealsInFridge > 0) {
                            gameManagerScript.currentHour += 1;
                            gameManagerScript.daysHungry = 0;
                            gameManagerScript.eaten = true;
                            Debug.Log(gameManagerScript.eaten);
                            Debug.Log(gameManagerScript.mealsInFridge);
                            print("You ate");
                        }
                    }
                    if (hit.collider.gameObject.tag == "Fridge") {
                        if (gameManagerScript.mealsInFridge >= 10) {
                            Debug.Log(gameManagerScript.mealsInFridge);
                            Debug.Log("Your Fridge Is Stocked!");

                        }
                        if (gameManagerScript.mealsInFridge < 10) {
                            gameManagerScript.mealsInFridge++;
                            gameManagerScript.savings -= 5;
                            Debug.Log("Purchased Some Food");
                            Debug.Log(gameManagerScript.mealsInFridge);
                        }
                    }
                    if (hit.collider.gameObject.tag == "Door") {
                        // I think that the door should be used to go buy things groceries, or personal effects or go to do an "activity" outside
                        // your activity outside will adjust time though
                        // print(gameManagerScript.currentHour);
                    }
                    if (hit.collider.gameObject.tag == "Bed") {
                        gameManagerScript.currentHour = (11 - gameManagerScript.currentHour) + gameManagerScript.currentHour;
                        gameManagerScript.timeStart = 0f;
                        print("You went to sleep");
                        // print(gameManagerScript.currentHour);
                    }
                    if (hit.collider.gameObject.tag == "Calendar") {
                        gameManagerScript.textDate.enabled = true;
                        print("THIS DAY");
                        StartCoroutine(WaitForSec());
                    }
                    if (hit.collider.gameObject.tag == "Clock") {
                        gameManagerScript.textTime.enabled = true;
                        print("THIS TIME");
                        StartCoroutine(WaitForSec());
                    }
                    if (hit.collider.gameObject.tag == "Notebook") {
                        DisplayObject(hit.transform.gameObject);
                        gameManagerScript.textBills.enabled = true;
                        StartCoroutine(WaitForSec());
                    }
                    IEnumerator WaitForSec() {
                        yield return new WaitForSeconds(5);
                        gameManagerScript.textSavings.enabled = false;
                        gameManagerScript.textDate.enabled = false;
                        gameManagerScript.textTime.enabled = false;
                        gameManagerScript.textBills.enabled = false;
                    }
                }
            }
    }
    private void DisplayObject(GameObject go) {
        print(go.name);
    }
}
