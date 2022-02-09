using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �_���[�W��\������R���|�[�l���g
/// </summary>
public class LevelUpUI : MonoBehaviour
{
	[SerializeField] Text _damageText;
	float _fadeOutSpeed = 1f; //�@�t�F�[�h�A�E�g����X�s�[�h
	[SerializeField] float _moveSpeed = 0.4f; //�@�ړ��l

	private void LateUpdate()
	{
		transform.rotation = Camera.main.transform.rotation;
		transform.position += Vector3.up * _moveSpeed * Time.deltaTime;

		_damageText.color = Color.Lerp(_damageText.color, new Color(1f, 1f, 1f, 0f), _fadeOutSpeed * Time.deltaTime);

		if (_damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
}
