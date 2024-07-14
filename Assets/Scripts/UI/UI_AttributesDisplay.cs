using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AttributesDisplay : MonoBehaviour
{
    public Player player;
    public GameObject attributeBase;
    Dictionary<string, GameObject> displayedAttributes = new Dictionary<string, GameObject>();
    void Start()
    {
        AttributeSystem attributeSystem = player.GetComponent<AttributeSystem>();
        player.GetComponent<AttributeSystem>().attributeValueChanged += AttributeChanged;

        foreach(KeyValuePair<string, Attribute> kvp in attributeSystem.attributes)
        {
            GameObject attribute = Instantiate(attributeBase);
            attribute.GetComponent<UI_Attribute>().SetAttributeInfo(kvp.Value);
            attribute.transform.SetParent(this.transform);
            attribute.transform.localScale = new Vector3(1, 1, 1);

            displayedAttributes.Add(kvp.Key, attribute);
        }
    }

    void AttributeChanged(Attribute attribute)
    {
            displayedAttributes[attribute.aName].GetComponent<UI_Attribute>().SetAttributeInfo(attribute);
    }

}
