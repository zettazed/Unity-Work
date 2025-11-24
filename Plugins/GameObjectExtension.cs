using UnityEngine;

public static class GameObjectExtension
{
    public static Object Find(string name, System.Type type)
    {
        Object[] objects = Resources.FindObjectsOfTypeAll(type);
        foreach (var obj in objects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    public static GameObject Find(string name)
    {
        return Find(name, typeof(GameObject)) as GameObject;
    }
}