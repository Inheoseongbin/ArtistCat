using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
	//[SerializeField] private Texture2D cursorImg;
	[SerializeField] private Sprite cursor;

	private void Awake()
	{
		Cursor.SetCursor(textureFromSprite(cursor), Vector2.zero, CursorMode.ForceSoftware);
	}

    public static Texture2D textureFromSprite(Sprite sprite) // 스프라이트를 텍스쳐로
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}
