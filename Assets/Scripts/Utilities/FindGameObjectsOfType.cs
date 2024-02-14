using System;

[AttributeUsage(AttributeTargets.Field)]
public class FindGameObjectsOfType: Attribute
{
    public Type type;
    
    public FindGameObjectsOfType(Type type)
    {
        this.type = type;
    }
}