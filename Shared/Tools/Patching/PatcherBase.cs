using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Shared.Patches.Patching;

public abstract class PatcherBase<TAttribute> : PatcherBase where TAttribute : PatchKeyAttributeBase
{
    protected PatcherBase()
    {
        PatchInfos = typeof(PatcherBase<>).Assembly.GetTypes().Where(b => b.HasAttribute<TAttribute>())
            .ToImmutableSortedDictionary(type => type.GetCustomAttribute<TAttribute>().Key,
                type => CreatePatchInfo(type, type.GetCustomAttribute<TAttribute>().Categories));
    }
    
    protected abstract PatchInfo CreatePatchInfo(Type type, string[] categories);
}

public abstract class PatcherBase : IXmlSerializable
{
    public IReadOnlyDictionary<string, PatchInfo> PatchInfos { get; protected init; }

    public abstract void ApplyEnabled();

    XmlSchema IXmlSerializable.GetSchema() => null;

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && PatchInfos.TryGetValue(reader.Name, out var patchInfo) &&
                ParsingTools.TryParseBool(reader.ReadString(), out var enabled))
                patchInfo.Enabled = enabled;
        }
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
        foreach (var (key, patchInfo) in PatchInfos)
        {
            writer.WriteStartElement(key);
            writer.WriteValue(patchInfo.Enabled);
            writer.WriteEndElement();
        }
    }
}