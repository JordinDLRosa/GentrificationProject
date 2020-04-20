using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInteraction : MonoBehaviour {
    private GamePlayManager gameManagerScript;
    private void Awake() {
        gameManagerScript = GameObject.FindObjectOfType<GamePlayManager>();
    }
    private void Update() {
        GetObject();
    }
    private void checkRentDueDate() {
        if (gameManagerScript.rentPaid == false && gameManagerScript.currentDay >= 1 && gameManagerScript.currentDay <= 5) {
            gameManagerScript.payBills();
        }
    }
    private void checkGasDueDate() {
        if (gameManagerScript.gasPaid == false && gameManagerScript.currentDay >= 5 && gameManagerScript.currentDay <= 12) {
            gameManagerScript.payBills();
        }
    }
    private void checkElectricityDueDate() {
        if (gameManagerScript.electricityPaid == false && gameManagerScript.currentDay >= 10 && gameManagerScript.currentDay <= 17) {
            gameManagerScript.payBills();
        }
    }
    private void checkCellDueDate() {
        if (gameManagerScript.cellPaid == false && gameManagerScript.currentDay >= 16 && gameManagerScript.currentDay <= 23) {
            gameManagerScript.payBills();
        }
    }
    private void GetObject() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform != null) {
                    if (hit.collider.gameObject.tag == "ComputerForNow") {
                        DisplayObject(hit.transform.gameObject);
                        checkRentDueDate();
                        checkGasDueDate();
                        checkElectricityDueDate();
                        checkCellDueDate();
                    }
                    if (hit.collider.gameObject.tag == "StoveForNow") {
                        DisplayObject(hit.transform.gameObject);
                        if (gameManagerScript.currentHour < 10) {
                            gameManagerScript.currentHour += 1;
                            print("You ate");
                            gameManagerScript.eaten = true;

                            if (gameManagerScript.daysHungry > 0) {
                                gameManagerScript.daysHungry--;
                            }
                            else {
                                gameManagerScript.daysHungry = 0;
                            }
                        }
                        else {
                            print("too late to eat");
                        }
                        print(gameManagerScript.health);
                    }
                    else {
                        gameManagerScript.eaten = false;
                    }
                    if (hit.collider.gameObject.tag == "Door") {
                        //gameManagerScript.savings
                        gameManagerScript.currentHour = gameManagerScript.currentHour + 8;
                        print(gameManagerScript.currentHour);
                    }

                }
            }

    }

    private void DisplayObject(GameObject go) {
        print(go.name);
    }
}
