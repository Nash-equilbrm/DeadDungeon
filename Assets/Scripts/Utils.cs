using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Utils
{
    // Check if object is in layer mask
    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

    public static void FlipAnimation(GameObject obj, Vector3 moveDelta)
    {
        if (moveDelta.x > 0)
            obj.transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            obj.transform.localScale = new Vector3(-1, 1, 1);
    }

    public static void Flip (Transform transform)
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
