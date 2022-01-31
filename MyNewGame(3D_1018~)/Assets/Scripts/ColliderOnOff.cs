using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderOnOff : MonoBehaviour
{
    [SerializeField] Collider _skillECollider = default;
    // Start is called before the first frame update
    void SkillEColliderOn()
    {
        _skillECollider.gameObject.SetActive(true);
    }

    void SkillEColliderOff()
    {
        _skillECollider.gameObject.SetActive(false);
    }
}
