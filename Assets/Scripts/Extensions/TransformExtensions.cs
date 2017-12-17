using UnityEngine;

public static class TransformExtensions
{

    public static void AddChild(this Transform parent, Transform child)
    {
        if (!child.IsChildOf(parent))
        {
            child.SetParent(child);
        }

    }
    
    public static void AddChildren(this Transform parent, Transform[] children)
    {
        for (int i = 0; i < children.Length; i++)
        {
            Transform child = children[i];
            if (!child.IsChildOf(parent))
            {
                child.SetParent(parent);
            }
        }
    }

    public static void RemoveFromParent(this Transform child)
    {
        if (child.parent != null)
        {
            child.parent = null;
        }
    }



    public static Transform GetChildWithTag(this Transform parent, string tag)
    {
        foreach (Transform tr in parent)
        {
            if (tr.CompareTag(tag))
            {
                return tr;
            }
        }
        return null;
    }

    public static T FindComponentInChildWithTag<T>(this Transform parent, string tag) where T : Component
    {
        foreach (Transform tr in parent)
        {
            if (tr.CompareTag(tag))
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }

    public static void DestroyChildren(this Transform self)
    {
        foreach (Transform child in self)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void LookAt2D(this Transform self, Transform target)
    {
        self.right = target.position - self.position;
    }
}
