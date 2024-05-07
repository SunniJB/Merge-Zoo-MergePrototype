using UnityEngine;
using UnityEngine.UI;
public static class Tools
{
    public static void CalculateImageSize(Image image, Sprite sprite)
    {
        Vector2 imageSize = image.rectTransform.sizeDelta;
        Vector2 size = sprite.rect.size;
        float aspect;
        if (size.x < size.y)
        {
            aspect = size.y / size.x;
            image.rectTransform.sizeDelta = new Vector2(imageSize.x, imageSize.x * aspect);
        }
        else
        {
            aspect = size.x / size.y;
            image.rectTransform.sizeDelta = new Vector2(imageSize.y * aspect, imageSize.y);
        }

    }
}
