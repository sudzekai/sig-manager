namespace Shared.Utilities.Mapper
{
    public static class Mapper
    {
        private static readonly Dictionary<(Type, Type), Delegate> _routes = [];

        public static void Add<TFrom, TTo>(Func<TFrom, TTo> map)
        {
            _routes[(typeof(TFrom), typeof(TTo))] = map;
        }

        public static TTo MapTo<TTo>(object value)
        {
            var key = (value.GetType(), typeof(TTo));

            if (!_routes.TryGetValue(key, out var mapper))
                throw new InvalidOperationException($"Mapper {key.Item1.Name} -> {key.Item2.Name} not found.");

            return ((Func<object, TTo>)(x => ((Func<dynamic, TTo>)mapper)((dynamic)x)))(value);
        }

        public static TTo MapTo<TFrom, TTo>(TFrom value)
        {
            if (!_routes.TryGetValue((typeof(TFrom), typeof(TTo)), out var mapper))
                throw new InvalidOperationException();

            return ((Func<TFrom, TTo>)mapper)(value);
        }
    }
}
