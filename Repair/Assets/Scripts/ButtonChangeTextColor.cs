using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ButtonChangeTextColor : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Button button;
    public TMP_Text text;
    private bool currentSelectionState;

    void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();

        button.Select();
    }


    public void OnSelect(BaseEventData eventData)
    {
        text.color = Color.white;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        text.color = Color.black;
    }
}
