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
    private void GetObject() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform != null) {
                    if (hit.collider.gameObject.tag == "ComputerForNow") {
                        DisplayObject(hit.transform.gameObject);
                        gameManagerScript.payBills();
                    }
                    if (hit.collider.gameObject.tag == "Telephone") {
                        DisplayObject(hit.transform.gameObject);
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour > 9) {
                            print("It's too late to eat. I won't get enough rest for work tomorrow.");
                        }
                        if (gameManagerScript.eaten == true) {
                            print("I am too full to eat");
                        }
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour < 10){
                            gameManagerScript.eaten = true;
                            gameManagerScript.savings -= 10;
                            print("You ordered food!");
                            gameManagerScript.currentHour += 1;
                        }
                    }
                        if (hit.collider.gameObject.tag == "StoveForNow") {
                        DisplayObject(hit.transform.gameObject);
                        // checks to make sure you havent eaten, and that it's not too late to eat.
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour > 9) {
                            print("It's too late to eat. I won't get enough rest for work tomorrow.");
                        }
                        if (gameManagerScript.eaten == true) {
                            print("I am too full to eat");
                        }
                        if (gameManagerScript.eaten == false && gameManagerScript.currentHour < 10 && gameManagerScript.mealsInFridge > 0) {
                            gameManagerScript.currentHour += 1;
                            gameManagerScript.daysHungry = 0;
                            gameManagerScript.eaten = true;
                            print("You ate: should also print show an emote above him that shows he's eating");
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
                        // print(gameManagerScript.currentHour);
                    }

                }
            }

    }

    private void DisplayObject(GameObject go) {
        print(go.name);
    }
}
