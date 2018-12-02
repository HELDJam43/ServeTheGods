using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    static Global Instance;
    public Sprite transparentImage;
    public GameObject timerPrefab;
    public Waypoints.Waypoint entranceWaypoint;
    protected virtual void Awake()
    {
        Instance = this;
    }
    public static Sprite LoadKey(KeyCode k)
    {
        string key = k.ToString();
        if (key.Contains("Alpha"))
            key = key.Substring(key.Length - 1);
        return Resources.Load<Sprite>("KeyboardImages\\Keyboard_Black_" + key);
    }
    public static Sprite TransparentImage
    {
        get
        {
            if (Instance)
                return Instance.transparentImage;
            else
                return null;
        }

    }
    public static GameObject GetResource(string folder, string prefabName)
    {
        return Resources.Load<GameObject>(folder + "\\" + prefabName);
    }

    public static Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += Mathf.Sin(t * Mathf.PI) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirecteion = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirecteion);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += (Mathf.Sin(t * Mathf.PI) * height) * up.normalized;
            return result;
        }
    }

    public static Vector2 SampleParabolaDerivative2D(Vector3 start, Vector3 end, float height, float t, float delta)
    {
        if (t - delta < 0)
            return Vector2.zero;
        Vector3 v2 = SampleParabola(start, end, height, t);
        Vector3 v1 = SampleParabola(start, end, height, t - delta);
        return v2 - v1;
    }
    public static void RunCoroutine(IEnumerator routine)
    {
        Instance.StartCoroutine(routine);
    }

    public static GameObject TimerPrefab
    {
        get
        {
            return Instance.timerPrefab;
        }
    }
    public static Waypoints.Waypoint EntranceWaypoint
    {
        get
        {
            return Instance.entranceWaypoint;
        }
    }

}
public static class Vector2Extension
{
    public static Vector2 RotateDEG(this Vector2 input, float degrees)
    {
        return input.RotateRAD(degrees * Mathf.Deg2Rad);
    }
    public static Vector2 RotateRAD(this Vector2 input, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(cos * input.x - sin * input.y, sin * input.x + cos * input.y);
    }
}
public static class TransformDeepChildExtension
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        return aParent.FindDeepChild<Transform>(aName);
    }

    public static T FindDeepChild<T>(this Transform aParent, string aName)
    {
        Transform result = aParent.Find(aName);
        if (result != null)
            return result.GetComponent<T>();
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild<Transform>(aName);
            if (result != null)
                return result.GetComponent<T>();
        }
        return default(T);
    }
}
static class LevenshteinDistance
{
    /// <summary>
    /// Compute the distance between two strings.
    /// </summary>
    public static int Compute(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // Step 1
        if (n == 0)
        {
            return m;
        }

        if (m == 0)
        {
            return n;
        }

        // Step 2
        for (int i = 0; i <= n; d[i, 0] = i++)
        {
        }

        for (int j = 0; j <= m; d[0, j] = j++)
        {
        }

        // Step 3
        for (int i = 1; i <= n; i++)
        {
            //Step 4
            for (int j = 1; j <= m; j++)
            {
                // Step 5
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                // Step 6
                d[i, j] = Mathf.Min(
                    Mathf.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        // Step 7
        return d[n, m];
    }
}

