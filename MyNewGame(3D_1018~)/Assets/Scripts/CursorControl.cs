using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorControl : MonoBehaviour,IPointerEnterHandler //IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] Texture2D _cursor = default;
    [SerializeField] Texture2D _cursorOnEnemy = default;
    [SerializeField] Texture2D _cursorOnSelect = default;

    CursorMode _cursormode = CursorMode.Auto;
    Vector2 _hotspot = Vector2.zero; 
    void Start()
    {
        // �}�E�X�J�[�\��������
        //Cursor.visible = false;
        Cursor.SetCursor(_cursor, _hotspot, _cursormode);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (data.Equals(gameObject.CompareTag("Enemy")))
        {
            Cursor.SetCursor(_cursorOnEnemy, _hotspot, _cursormode);
        }
    }

    void Update()
    {
        // Camera.main �Ń��C���J�����iMainCamera �^�O�̕t���� Camera�j���擾����
        // Camera.ScreenToWorldPoint �֐��ŁA�X�N���[�����W�����[���h���W�ɕϊ�����
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;    // Z ���W���J�����Ɠ����ɂȂ��Ă���̂ŁA���Z�b�g����
        this.transform.position = mousePosition;
        
    }
}
