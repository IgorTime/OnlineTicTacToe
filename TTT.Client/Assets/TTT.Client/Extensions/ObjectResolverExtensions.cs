using VContainer;

namespace TTT.Client.Extensions
{
    public static class ObjectResolverExtensions
    {
        public static T ActivateInstance<T>(this IObjectResolver resolver)
        {
            var type = typeof(T);
            var constructorInfo = type.GetConstructors()[0];
            var parameters = constructorInfo.GetParameters();
            var resolvedParameters = new object[parameters.Length];

            for (var i = 0; i < resolvedParameters.Length; i++)
            {
                resolvedParameters[i] = resolver.Resolve(parameters[i].ParameterType);
            }

            return (T) constructorInfo.Invoke(resolvedParameters);
        }
    }
}