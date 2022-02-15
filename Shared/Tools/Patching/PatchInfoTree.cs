using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HarmonyLib;

namespace Shared.Patches.Patching
{
    public class PatchInfoTree
{
    public Dictionary<string, Node> Root { get; } = new Dictionary<string, Node>();

    public void Add(string name, PatchInfo patchInfo)
    {
        Node node;
        if (patchInfo.Categories.Length == 0)
        {
            node = new Node(name, null, patchInfo);
            Root.Add(name, node);
            return;
        }

        var rootCategory = patchInfo.Categories[0];
        if (!Root.ContainsKey(rootCategory))
        {
            Root.Add(rootCategory, new Node(rootCategory, null));
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

        if (node.PatchInfo != null)
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

    public class Node : INotifyPropertyChanged
    {
        private bool enabled;
        private PatchInfo patchInfo;

        public Node(string key, Node parent, PatchInfo patchInfo = null)
        {
            Key = key;
            enabled = patchInfo?.Enabled ?? false;
            Parent = parent;
            this.patchInfo = patchInfo;
            DisplayName = Key.Humanize(LetterCasing.Sentence);
        }

        public Dictionary<string, Node> Children { get; } = new Dictionary<string, Node>();

        public PatchInfo PatchInfo
        {
            get => patchInfo;
            set
            {
                patchInfo = value;
                Enabled = patchInfo.Enabled;
            }
        }

        public string Key { get; }
        public Node Parent { get; }

        public string DisplayName { get; } 

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
            if (PatchInfo != null)
                PatchInfo.Enabled = enabled;

            if (updateChildren && value.HasValue) Children.Values.ForEach(c => c.SetEnabled(enabled, true, false));

            if (updateParent && Parent != null) Parent.VerifyCheckedState();

            // ReSharper disable once ExplicitCallerInfoArgument
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enabled)));
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
}