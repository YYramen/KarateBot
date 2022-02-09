using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ダメージを表示するコンポーネント
/// </summary>
public class LevelUpUI : MonoBehaviour
{
	[SerializeField] Text _damageText;
	float _fadeOutSpeed = 1f; //　フェードアウトするスピード
	[SerializeField] float _moveSpeed = 0.4f; //　移動値

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
