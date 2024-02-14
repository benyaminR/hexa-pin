using System;
using System.Reflection;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    private void Awake()
    {
        MonoBehaviour[] sceneActive = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour mono in sceneActive)
        {
            FieldInfo[] objectFields = mono.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            for (int i = 0; i < objectFields.Length; i++)
            {
                var objectField = objectFields[i];

                DoFindGameObjectWithTag(objectField, mono);
                DoFindGameObjectWithName(objectField, mono);
                DoFindGameObjectOfType(objectField, mono);
                DoFindGameObjectsOfType(objectField, mono);
            }
        }
    }

    private void DoFindGameObjectOfType(FieldInfo objectField, MonoBehaviour mono)
    {
        FindObjectOfType attribute =
            Attribute.GetCustomAttribute(objectField, typeof(FindObjectOfType)) as FindObjectOfType;

        if (attribute == null) return;

        objectField.SetValue(mono, FindObjectOfType(attribute.type));
    }

    private void DoFindGameObjectWithTag(FieldInfo objectField, MonoBehaviour mono)
    {
        FindGameObjectWithTag attribute =
            Attribute.GetCustomAttribute(objectField, typeof(FindGameObjectWithTag)) as FindGameObjectWithTag;
        if (attribute != null)
        {
            var component = GameObject.FindWithTag(attribute.tag).GetComponent(objectField.FieldType);
            objectField.SetValue(mono, component);
        }
    }

    private void DoFindGameObjectWithName(FieldInfo objectField, MonoBehaviour mono)
    {
        FindGameObjectWithName attribute =
            Attribute.GetCustomAttribute(objectField, typeof(FindGameObjectWithName)) as FindGameObjectWithName;
        if (attribute != null)
        {
            if (objectField.FieldType == typeof(GameObject))
            {
                var component = GameObject.Find(attribute.name);
                objectField.SetValue(mono, component);
            }
            else
            {
                var component = GameObject.Find(attribute.name).GetComponent(objectField.FieldType);
                objectField.SetValue(mono, component);
            }
        }
    }

    private void DoFindGameObjectsOfType(FieldInfo objectField, MonoBehaviour mono)
    {
        FindGameObjectsOfType attribute =
            Attribute.GetCustomAttribute(objectField, typeof(FindGameObjectsOfType)) as FindGameObjectsOfType;
        if (attribute != null)
        {
            objectField.SetValue(mono, FindObjectsOfType(attribute.type));
        }
    }
}