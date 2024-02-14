using System;

[AttributeUsage(AttributeTargets.Field)]
public class FindObjectOfType : Attribute
{
    public Type type;
    
    public FindObjectOfType(Type type)
    {
        this.type = type;
    }
}