using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSlider : MonoBehaviour
{
    [SerializeField] Slider _missionSlider = default;
    [SerializeField] GameObject _clearZone = default;
    [SerializeField] Transform _clearZonePos = default;
    [SerializeField] Text _missionText = default;
    [SerializeField] Transform _textpos = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_missionSlider.value == 1)
        {
            Instantiate(_clearZone, _clearZonePos);
            Instantiate(_missionText, _textpos);
            Destroy(gameObject);
        }
    }
}
