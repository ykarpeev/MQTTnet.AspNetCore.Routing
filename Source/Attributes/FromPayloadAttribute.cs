using System;
using System.Text.Json;

namespace MQTTnet.AspNetCore.Routing.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class FromPayloadAttribute : Attribute
{
    public FromPayloadAttribute()
    {
        
    }
}