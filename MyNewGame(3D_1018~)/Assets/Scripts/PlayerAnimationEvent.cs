using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// AnimationEvent で呼び出すスキルの効果のコンポーネント
/// </summary>
public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] Collider _skillECollider = default;
    [SerializeField] Collider _skillWCollider = default;
    [SerializeField] AudioSource _audio = default;
    [SerializeField] AudioClip _nomalAtk = default;
    [SerializeField] AudioClip _skillQ = default;
    [SerializeField] AudioClip _skillWSound = default;
    [SerializeField] AudioClip _skillESound = default;
    float _skillQAtkUp = 10;
    float _skillQAtkTime = 10f;
    
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

    void PlayNomalAtkSound()
    {
        AudioSource.PlayClipAtPoint(_nomalAtk, this.transform.position);
    }

    void PlaySkillQSound()
    {
        AudioSource.PlayClipAtPoint(_skillQ, this.transform.position);
    }

    void PlaySkillWSound()
    {
        AudioSource.PlayClipAtPoint(_skillWSound, this.transform.position);
    }

    void PlaySkillESound()
    {
        AudioSource.PlayClipAtPoint(_skillESound, this.transform.position);
    }
}
