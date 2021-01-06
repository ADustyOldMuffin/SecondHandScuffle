using UnityEngine;

public static class PixelMovementUtility
{
    public static Vector2 PixelPerfectClamp(Vector2 moveVector, float pixelsPerUnit)
    {
        var vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }
}