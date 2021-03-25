using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UINavigator : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Selectable[] selectables;
    [SerializeField] private KeyCode switchKey = KeyCode.Tab;

    private int selectedIndex = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            if (selectables[i] == eventData.selectedObject)
            {
                selectedIndex = i;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            if(selectedIndex < selectables.Length - 1)
            {
                selectables[selectedIndex + 1].Select();
                selectedIndex++;
            }
            else
            {
                selectables[0].Select();
                selectedIndex = 0;
            }
        }
    }
}
