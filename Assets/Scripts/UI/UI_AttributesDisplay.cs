using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AttributesDisplay : MonoBehaviour
{
    public Player player;
    public GameObject attributeBase;
    void Start()
    {
        AttributeSystem attributeSystem = player.GetComponent<AttributeSystem>();

        foreach(KeyValuePair<string, Attribute> kvp in attributeSystem.Attributes)
        {
            GameObject attribute = Instantiate(attributeBase);
            attribute.GetComponent<UI_Attribute>().SetAttributeInfo(kvp.Value);
            attribute.transform.SetParent(this.transform);
            attribute.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
