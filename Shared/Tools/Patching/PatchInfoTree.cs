using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Humanizer;

namespace Shared.Patches.Patching;

public class PatchInfoTree
{
    public Dictionary<string, Node> Root { get; } = new();

    public void Add(string name, PatchInfo patchInfo)
    {
        Node node;
        if (patchInfo.Categories is {Length: 0})
        {
            node = new Node(name, null, patchInfo);
            Root.Add(name, node);
            return;
        }

        var rootCategory = patchInfo.Categories[0];
        if (!Root.ContainsKey(rootCategory))
        {
            Root.Add(rootCategory, new(rootCategory, null));
        }

        node = Root[rootCategory];

        var categoryPath = patchInfo.Categories.Skip(1).AddItem(name);
        
        foreach (var current in categoryPath)
        {
            if (node.Children.ContainsKey(current))
            {
                node = node.Children[current];
                continue;
            }

            var newNode = new Node(current, node);
            node.Children.Add(current, newNode);
            node = newNode;
        }

        if (node.PatchInfo is not null)
            throw new ArgumentException($"Node with key {name} already filled");

        node.PatchInfo = patchInfo;
    }

    public IEnumerable<Node> WalkTree(IEnumerable<Node> parent = null)
    {
        foreach (var node in parent ?? Root.Values)
        {
            yield return node;
            
            foreach (var node1 in WalkTree(node.Children.Values))
            {
                yield return node1;
            }
        }
    }

    public record Node(string Key, Node Parent, PatchInfo PatchInfo = null) : INotifyPropertyChanged
    {
        private bool enabled = PatchInfo?.Enabled ?? false;
        private PatchInfo patchInfo = PatchInfo;
        public Dictionary<string, Node> Children { get; } = new();

        public PatchInfo PatchInfo
        {
            get => patchInfo;
            set
            {
                patchInfo = value;
                Enabled = patchInfo.Enabled;
            }
        }

        public string DisplayName { get; } = Key.Humanize(LetterCasing.Sentence); 

        public bool Enabled
        {
            get => enabled;
            set => SetEnabled(value, true, true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void SetEnabled(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == enabled) return;

            enabled = value ?? false;
            if (PatchInfo is not null)
                PatchInfo.Enabled = enabled;

            if (updateChildren && value.HasValue) Children.Values.ForEach(c => c.SetEnabled(enabled, true, false));

            if (updateParent && Parent is not null) Parent.VerifyCheckedState();

            // ReSharper disable once ExplicitCallerInfoArgument
            PropertyChanged?.Invoke(this, new(nameof(Enabled)));
        }

        void VerifyCheckedState()
        {
            bool? state = null;

            var first = true;
            foreach (var current in Children.Values.Select(childrenValue => childrenValue.Enabled))
            {
                if (first)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }

                first = false;
            }

            SetEnabled(state, false, true);
        }
    }
}