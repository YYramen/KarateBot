using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotate : MonoBehaviour
{
    /// <summary>
    /// UIの向きを常にカメラに向けるよう調整するためのコンポーネント
    /// </summary>
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
