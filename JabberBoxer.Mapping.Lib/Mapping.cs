using System;

namespace JabberBoxer.Mapping.Lib
{
    public sealed class Mapping
    {
        public int Key { get; private set; }
        public Action<object, object> Map { get; private set; }
        public Mapping(Binding binding)
        {
            this.Key = binding.GetRelationship().Id;
            this.Map = binding.GetAction();
        }
        public override int GetHashCode()
        {
            return this.Key;
        }

        public static Mapping Create(Binding binding)
        {
            return new Mapping(binding);
        }
    }
}
