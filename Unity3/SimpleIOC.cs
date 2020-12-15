using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unity3
{
    public class SimpleIOC
    {
        private readonly Dictionary<Type, Type> typeMaps = new Dictionary<Type, Type>();
        public void RegisterType<TFrom, TTo>() where TTo : TFrom, new()
        {
            Type interfaceType = typeof(TFrom);
            Type type = typeof(TTo);

            if (typeMaps.ContainsKey(interfaceType))
                typeMaps[interfaceType] = type;
            else
                typeMaps.Add(interfaceType, type);
        }

        public void RegisterType<T>() where T : new()
        {
            if (typeMaps.ContainsKey(typeof(T)))
                typeMaps[typeof(T)] = typeof(T);
            else
                typeMaps.Add(typeof(T), typeof(T));
        }


        private object CreateInstance(Type type)
        {
            if (typeMaps.ContainsKey(type))
                type = typeMaps[type];

            if (type.IsInterface || type.IsAbstract)
                return null;

            var ctorInfos = type.GetConstructors();//默认使用参数最多的构造函数

            List<ParameterInfo> maxLengthParamterInfos = new List<ParameterInfo>();
            foreach (var ctor in ctorInfos)
            {
                var paramsInfos = ctor.GetParameters();
                if (paramsInfos.Length > maxLengthParamterInfos.Count)
                    maxLengthParamterInfos.AddRange(paramsInfos);
            }

            List<object> list = new List<object>();
            foreach (var param in maxLengthParamterInfos)
            {
                Type pType = param.ParameterType;

                list.Add(CreateInstance(pType));
            }

            return Activator.CreateInstance(type, list.ToArray());
        }

        public T Resolve<T>()
        {
            return (T)CreateInstance(typeof(T));
        }
    }
}
