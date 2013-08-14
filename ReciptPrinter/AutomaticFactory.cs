using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReciptPrinter
{
    //Could have used depenency resolvers like ninject or autofac.. but for now this is okay.
    public class AutomaticFactory
    {
        public static object GetMeOne(Type type)
        {
            if (type.IsInterface)
            {
                var instance = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.GetInterfaces().Contains(type)
                          && t.GetConstructor(Type.EmptyTypes) != null
                    select t;
                return  Activator.CreateInstance(instance.FirstOrDefault());
            }

            var constructor = type.GetConstructors().Single();
            
            var parameters = constructor.GetParameters();
            
            if (!parameters.Any()) return Activator.CreateInstance(type);

            return Activator.CreateInstance(type, parameters.Select(parameterInfo => GetMeOne(parameterInfo.ParameterType)).ToArray());  
        }
    }
}