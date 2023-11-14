using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
	[SerializeField] private Texture2D cursorImg;

	private void Awake()
	{
		Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
	}
}
