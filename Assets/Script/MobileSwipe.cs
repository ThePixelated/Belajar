using UnityEngine;

public class MobileSwipe
{
    private static Vector3 startPosTouch = Vector3.zero;

    public static short CalcHorizontal(float margin = 0f)
    {
        if (Input.GetMouseButtonDown(0))
            startPosTouch = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 direction = Input.mousePosition;

            if (startPosTouch.x > direction.x) return -1; // left
            if (startPosTouch.x < direction.x) return 1; // right
        }

        return 0;
    }

    public static bool isReturnLeftSwipe(float margin = 0f) { if (CalcHorizontal(margin) == -1) return true; return false; }
    public static bool isReturnRightSwipe(float margin = 0f) { if (CalcHorizontal(margin) == 1) return true; return false; }
}
