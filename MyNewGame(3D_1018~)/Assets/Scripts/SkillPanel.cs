using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] GameObject _skillPanel;
    [Header ("Skill1")]
    [SerializeField] Text _skillText;
    [SerializeField] string _skillString = "スキル1";
    [Space(10)]
    [Header("Skill2")]
    [SerializeField] Text _skillText2;
    [SerializeField] string _skillString2 = "スキル２";
    [Space(10)]
    [SerializeField] Text _skillText3;
    [SerializeField] string _skillString3 = "スキル３";
    

    // Update is called once per frame
    void Start()
    {
        _skillText.text = _skillString;
        _skillText2.text = _skillString2;
        _skillText3.text = _skillString3;
    }
}
