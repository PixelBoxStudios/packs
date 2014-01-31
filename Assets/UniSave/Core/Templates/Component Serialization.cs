/* UniSave - Component Serialization File Template
 * 
 * Change ComponentName and Properties
 * 
 * Parts are commented out to prevent errors.
 */

using UnityEngine;
using ProtoBuf;
using System;
using System.Linq;
using System.Collections;

[ProtoContract]
public sealed class ComponentNameSerializer
{
    // [ProtoMember(1)] Property1
    // [ProtoMember(2)] Property2
    // [ProtoMember(3)] Property3
    // [ProtoMember(4)] Property4

    public ComponentNameSerializer(GameObject gameObject)
    {
        // var componentName = gameObject.GetComponent<ComponentName>();

        // Property1 = componentName.Property1;
        // Property2 = componentName.Property2;
        // etc.
    }

    public ComponentNameSerializer(GameObject gameObject, ComponentNameSerializer component)
    {
        // var componentName = gameObject.GetComponent<ComponentName>();

        // if (componentName == null)
            //componentName = gameObject.AddComponent<ComponentName>();

       // componentName.Property1 = component.Property1;
       // componentName.Property2 = component.Property2;
       // etc.
    }

    // Empty constructor required for ProtoBuf.
    private ComponentNameSerializer()
    {
    }
}
