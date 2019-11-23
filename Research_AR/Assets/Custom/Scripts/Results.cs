using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{


    public void Example()
    {
        bool isIdentical = IsColorIdentical(Color.black, Color.red);
    }

        public bool IsColorIdentical(Color _providedColor, Color _targetColor)
    {
        return (
            Mathf.Approximately(_providedColor.r, _targetColor.r)
        && Mathf.Approximately(_providedColor.g, _targetColor.g)
        && Mathf.Approximately(_providedColor.b, _targetColor.b)
        );
    }

    public static void Shuffle(GameObject[] gameObject)
    {
        for (int i = 0; i < gameObject.Length; i++)
        {
            // Find a random index
            int destIndex = UnityEngine.Random.Range(0, gameObject.Length);
            GameObject source = gameObject[i];
            GameObject dest = gameObject[destIndex];
            // If is not identical
            if (source != dest)
            {
                // Swap the position
                source.transform.position = dest.transform.position;
                // Swap the array item
                gameObject[i] = gameObject[destIndex];
            }
        }
    }

}
