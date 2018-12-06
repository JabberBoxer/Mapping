using System;
using System.Collections;

namespace JabberBoxer.Mapping.Lib
{
    public static class MappingCache
    {
        static readonly Hashtable _hastable = new Hashtable();

        static readonly CacheMapCommandValidator cacheMapCommandValidator = new CacheMapCommandValidator();
        static readonly RetieveMapCommandValidator retrieveMapCommandValidator = new RetieveMapCommandValidator();

        static Action<object, object> _currentAction = null;
        static int _currentKey = 0;

        public static bool HasKey(int key) => _hastable.ContainsKey(key);

        public static Action<CacheMapCommand> AddMap = (cmd) 
            => { 
                if (cacheMapCommandValidator.IsValid(cmd) && !HasKey(cmd.Key))
                    _hastable[cmd.Key] = cmd.Action;
            };
        
        public static Action<object,object> GetMap(RetrieveMapCommand cmd)
        {
            if (!retrieveMapCommandValidator.IsValid(cmd)) return null;

            if (_currentKey == cmd.Key && _currentAction != null)
                return _currentAction;

            _currentKey = cmd.Key;

            if (HasKey(_currentKey))
                _currentAction = (Action<object,object>)_hastable[_currentKey];

            return _currentAction;
        }

    }
}
