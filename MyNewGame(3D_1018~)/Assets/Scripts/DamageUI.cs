using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    [SerializeField] float _deleteTime = 1.0f;
    [SerializeField] float _moveRange = 1.0f;
    [SerializeField] float _endAlpha = 0;

    float _timeCount;
    Text _nowText;

    // Start is called before the first frame update
    void Start()
    {
        _timeCount = 0.0f;
        //Destroy(this.gameObject, _deleteTime);
        _nowText = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
        _timeCount += Time.deltaTime;
        this.gameObject.transform.localPosition += new Vector3(0, _moveRange / _deleteTime * Time.deltaTime, 0);
        this.gameObject.transform.Rotate(0, -180.0f, 0);
        float _alpha = 1.0f - (1.0f - _endAlpha) * (_timeCount / _deleteTime);

        if (_alpha <= 0.0f)
        {
            _alpha = 0.0f;
        }
        _nowText.color = new Color(_nowText.color.r, _nowText.color.g, _nowText.color.b, _alpha);
    }
}
