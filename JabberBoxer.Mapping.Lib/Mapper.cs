using System;
using System.Collections.Generic;
using System.Linq;

namespace JabberBoxer.Mapping.Lib
{
    using static F;

    public static class Mapper
    {

        public static void Bind<TSource, TTarget>() where TTarget : class, new()
        {
            IRelationship relationship = Relate<TSource, TTarget>();

            var binding = Binding(relationship);

            CacheMap(CacheMapCommand(binding.Id, binding.GetAction()));
        }

        public static TTarget Map<TSource, TTarget>(TSource source) where TTarget : class, new()
        {
            if (source.Equals(null))
                throw new NullReferenceException("Unable to map source to target. Mapping source is null");

            IRelationship relationship = Relate<TSource, TTarget>();

            var map = GetMap(GetMapCommand(relationship.Id));

            TTarget target = FastActivator<TTarget>.Create();

            map.Invoke(source, target);

            return target;
        }

        public static TTarget Map<TTarget>(object source) where TTarget : class, new()
        {
            if (source.Equals(null))
                throw new NullReferenceException("Unable to map source to target. Mapping source is null");

            IRelationship relationship = Relate(source.GetType(), typeof(TTarget));

            var map = GetMap(GetMapCommand(relationship.Id));

            TTarget target = FastActivator<TTarget>.Create();

            map.Invoke(source, target);

            return target;
        }

        public static IEnumerable<TTarget> Map<TTarget>(object[] sources) where TTarget : class, new()
        {
            return sources.AsParallel().Select(x => Map<TTarget>(x));
        }

    }

}
