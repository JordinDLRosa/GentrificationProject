using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour {
    private Transform container;
    private Transform shopItemTemplate;
    private void Awake() {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        CreateItemButton("Weekly Card", 33, 0);
        CreateItemButton("Monthly Card", 127, 1);
    }
    private void CreateItemButton(string itemName, int itemCost, int positionIndex) {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        shopItemTransform.gameObject.SetActive(true);

        float shopItemHeight = 1f;

        shopItemRectTransform.anchoredPosition = new Vector2(0, - shopItemHeight * positionIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshPro>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshPro>().SetText("$"+itemCost.ToString());

    }
}
