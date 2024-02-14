using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class FindGameObjectWithTag : Attribute
{
    public string tag;
    
    public FindGameObjectWithTag(String tag)
    {
        this.tag = tag;
    }
}