﻿using System.Collections;
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
            Debug.Log("This is your window to pay the rent");
            gameManagerScript.payBills();
        }
    }
    private void checkGasDueDate() {
        if (gameManagerScript.gasPaid == false && gameManagerScript.currentDay >= 5 && gameManagerScript.currentDay <= 12) {
            Debug.Log("This is your window to pay the gas bill");
            gameManagerScript.payBills();
        }
    }
    private void checkElectricityDueDate() {
        if (gameManagerScript.electricityPaid == false && gameManagerScript.currentDay >= 10 && gameManagerScript.currentDay <= 17) {
            Debug.Log("This is your window to pay the electricity bill");
            gameManagerScript.payBills();
        }
    }
    private void checkCellDueDate() {
        if (gameManagerScript.cellPaid == false && gameManagerScript.currentDay >= 16 && gameManagerScript.currentDay <= 23) {
            Debug.Log("This is your window to pay the cell bill");
            gameManagerScript.payBills();
        }
    }
    private void GetObject() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform != null) {
                    DisplayObject(hit.transform.gameObject);
                    checkRentDueDate();
                    checkGasDueDate();
                    checkElectricityDueDate();
                    checkCellDueDate();
                }
            }
    }

    private void DisplayObject(GameObject go) {
        print(go.name);
    }
}
