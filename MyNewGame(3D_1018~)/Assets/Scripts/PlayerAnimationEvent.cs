using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] Collider _skillECollider = default;
    [SerializeField] Collider _skillWCollider = default;
    float _skillQAtkUp = 10;
    float _skillQAtkTime = 10f;
    // Start is called before the first frame update
    void SkillEColliderOn()
    {
        _skillECollider.gameObject.SetActive(true);
    }

    void SkillEColliderOff()
    {
        _skillECollider.gameObject.SetActive(false);
    }

    void SkillQOn()
    {
        PlayerController.Instance.AddAttack(_skillQAtkUp);
        DOVirtual.DelayedCall(_skillQAtkTime, () => 
        {
           PlayerController.Instance.AddAttack(-(_skillQAtkUp));
        }
        );
    }

    void SkillWColliderOn()
    {
        _skillWCollider.gameObject.SetActive(true);
    }

    void SkillWColliderOff()
    {
        _skillWCollider.gameObject.SetActive(false);
    }
}
