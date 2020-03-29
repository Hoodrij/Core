using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class SpriteEx
    {
        public static Sprite GetCroppedSprite(this Sprite baseSprite, float xOffset, float yOffset, float width, float height, Vector2 pivot)
        {
            var baseTexture = baseSprite.texture;
            var textureRect = new Rect(baseSprite.textureRect.position.x + xOffset, baseSprite.textureRect.position.y + yOffset, width, height);
            var newSprite = Sprite.Create(baseTexture, textureRect, pivot, 16);

            return newSprite;
        }

        public static Texture2D ToTexture(this Sprite sprite)
        {
            if (sprite.rect.width != sprite.texture.width)
            {
                var newText = new Texture2D((int) sprite.rect.width, (int) sprite.rect.height);
                var newColors = sprite.texture.GetPixels((int) sprite.textureRect.x,
                    (int) sprite.textureRect.y,
                    (int) sprite.textureRect.width,
                    (int) sprite.textureRect.height);
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
            {
                return sprite.texture;
            }
        }
    }
}