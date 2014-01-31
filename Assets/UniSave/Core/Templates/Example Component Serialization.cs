/* UniSave - Example Component Serialization
 * 
 * Change ComponentName and Properties
 * 
 * Parts are commented out to prevent errors.
 */

using UnityEngine;
using ProtoBuf;
using System;
using System.Linq;

[ProtoContract]
public sealed class ExampleComponentSerializer
{
    // Example of built-in types
    [ProtoMember(1)] public string Text { get; set; }
    [ProtoMember(2)] public int Number { get; set; }

    // Example of custom types
    [ProtoMember(3)] public Vector3Serializer OneVector3 { get; set; }
    [ProtoMember(4)] public Vector3Serializer[] MultipleVector3 { get; set; }

    // Example of saving materials by name, as opposed to saving the Material
    [ProtoMember(5)] public string[] MaterialNames { get; set; }

    // This constructor gets called during the saving process. The GameObject it gets as a parameter is the object that holds the component that's supposed to be saved. 
    // It will save the data from the component to this class.
    public ExampleComponentSerializer(GameObject gameObject)
    {
        var exampleComponent = gameObject.GetComponent<ExampleComponent>();

        // Built-in data types don't have to be converted to a custom type.
        Text = exampleComponent.Text;
        Number = exampleComponent.Number;
        
        // Because Vector3 is a custom type, and cannot be serialized, it has to be converted to the custom Vector3Serializer type first.
        OneVector3 = (Vector3Serializer) exampleComponent.OneVector3;

        // When saving an array of custom data types, you have to convert each element which can be done easily with one line of code.
        MultipleVector3 = Array.ConvertAll(exampleComponent.MultipleVector3, element => (Vector3Serializer) element);

        // UniSave doesn't save the actual material(s), but saves the name. You can get the name of every material easily by using this LINQ query.
        MaterialNames = (from material in exampleComponent.Materials
                         select material.name).ToArray();
    }

    // This constructor gets called during the loading process. The GameObject it gets as a parameter is the original object that holds the component that was saved. 
    // The component argument holds the saved data. This data will be added back to the original component.
    public ExampleComponentSerializer(GameObject gameObject, ExampleComponentSerializer component)
    {
        var exampleComponent = gameObject.GetComponent<ExampleComponent>();

        // Check if the component already existed or not. If so, update it. If not, add it to the game object.
        // This is because of the differences between game objects made at runtime, and game objects made at design time.
        if (exampleComponent == null)
            exampleComponent = gameObject.AddComponent<ExampleComponent>();


        // Get the saved data from the serialized type, and add it back to the real component.


        exampleComponent.Text = component.Text;
        exampleComponent.Number = component.Number;

        // Convert Vector3Serializer back to Vector3
        exampleComponent.OneVector3 = (Vector3)component.OneVector3;
        exampleComponent.MultipleVector3 = Array.ConvertAll(component.MultipleVector3, element => (Vector3)element);

        // Because we saved the materials by name, we load them back in using that name, straight from the Resources folder. 
        // Note: We use a special UniSave method for this, which is a wrapper for the Resources.Load method.
        exampleComponent.Materials = (from materialName in component.MaterialNames
                                      select (Material) UniSave.TryLoadResource(materialName)).ToArray();
    }

    // Empty constructor required for ProtoBuf.
    private ExampleComponentSerializer()
    {
    }
}
