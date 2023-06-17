using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTyp : MonoBehaviour
{
    [SerializeField] private GemType gemType;

    public GemType GemType => gemType;
}
