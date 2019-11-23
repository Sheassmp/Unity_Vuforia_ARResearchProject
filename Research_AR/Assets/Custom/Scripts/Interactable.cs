using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected abstract void OnMouseDown();

    protected abstract void Respond();

}
