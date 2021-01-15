using System;
using UnityEngine;
using UnityEngine.Serialization;

public class KeepToPixelUnits : MonoBehaviour
{
    [SerializeField] private int pixelsPerUnit;
    
    // We do this in late update to that we let everything that's going to move the object, move it.
    private void LateUpdate()
    {
        var position = transform.position;
        var pixelPerfect = (Vector3)PixelMovementUtility.PixelPerfectClamp(position, pixelsPerUnit);

        pixelPerfect.z = position.z;
        
        transform.position = pixelPerfect;
    }
}