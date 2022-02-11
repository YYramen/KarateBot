using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Texture2D _cursor = default;
    [SerializeField] Texture2D _cursorOnEnemy = default;
    [SerializeField] Texture2D _cursorOnSelect = default;

    CursorMode _cursormode = CursorMode.Auto;
    Vector2 _hotspot = Vector2.zero; 
    void Start()
    {
        // マウスカーソルを消す
        Cursor.visible = false;
        Cursor.SetCursor(_cursor, _hotspot, _cursormode);
    }

    void Update()
    {
        // Camera.main でメインカメラ（MainCamera タグの付いた Camera）を取得する
        // Camera.ScreenToWorldPoint 関数で、スクリーン座標をワールド座標に変換する
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = 0;    // Z 座標がカメラと同じになっているので、リセットする
        //this.transform.position = mousePosition;
        
    }
}
