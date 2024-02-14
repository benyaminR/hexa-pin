using System;

[AttributeUsage(AttributeTargets.Field)]
public class FindGameObjectWithName : Attribute
{
    public string name;
    
    public FindGameObjectWithName(String name)
    {
        this.name = name;
    }
}