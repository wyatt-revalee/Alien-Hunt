using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSystem : MonoBehaviour
{

    Dictionary<string, Attribute> Attributes;

    // Start is called before the first frame update
    void Start()
    {
        Attributes = new Dictionary<string, Attribute>
        {
        };
    }
}
