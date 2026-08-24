using UnityEngine;

public static class TransformExtensions
{
    public static void SetWorldScale(this Transform target, Vector3 worldScale)
    {
        Vector3 parentScale = target.parent != null ? target.parent.lossyScale : Vector3.one;

        target.localScale = new Vector3(
            parentScale.x != 0f ? worldScale.x / parentScale.x : worldScale.x,
            parentScale.y != 0f ? worldScale.y / parentScale.y : worldScale.y,
            parentScale.z != 0f ? worldScale.z / parentScale.z : worldScale.z
        );
    }

    public static void CopyWorldTransform(this Transform target, Transform source, bool copyScale = true)
    {
        target.SetPositionAndRotation(source.position, source.rotation);
        if (copyScale) target.SetWorldScale(source.lossyScale);
    }
}