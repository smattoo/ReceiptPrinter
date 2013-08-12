using System;
using System.Linq;

namespace ReciptPrinter
{
    public class AutomaticFactory
    {
        public static object GetMeOne(Type type)
        {
            var constructor = type.GetConstructors().Single();
            var parameters = constructor.GetParameters();
            
            if (!parameters.Any()) return Activator.CreateInstance(type);

            return Activator.CreateInstance(type, parameters.Select(parameter => GetMeOne(parameter.ParameterType)).ToArray());  
        }
    }
}