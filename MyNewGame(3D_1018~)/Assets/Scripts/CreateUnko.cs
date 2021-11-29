using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUnko : MonoBehaviour
{
    [SerializeField] GameObject _unkoPanel;
    [SerializeField] Text _unkoText;
    [SerializeField] string _unkoString = "ウンチブリブリ";
    bool isMenu = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isMenu)
        {
            _unkoPanel.SetActive(true);
            _unkoText.text = _unkoString;
            isMenu = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isMenu)
        {
            _unkoPanel.SetActive(false);
            isMenu = false;
        }
    }
}
