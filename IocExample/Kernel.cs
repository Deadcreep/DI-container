using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocExample
{
    class Kernel
    {
        Dictionary<Type, Type> dictionaryForTypeToType;
        Dictionary<Type, object> dictionaryForTypeToObject;

        public Kernel()
        {
            dictionaryForTypeToType = new Dictionary<Type, Type>();
            dictionaryForTypeToObject = new Dictionary<Type, object>();
        }

        public void BindToType<T>(Type type)
        {
            var keyType = typeof(T);
            dictionaryForTypeToType.Add(keyType, type);
        }

        public void BindToObject<T>(object obj)
        {
            var keyType = typeof(T);
            dictionaryForTypeToObject.Add(keyType, obj);
        }

        public T Get<T>()
        {
            var keyType = typeof(T);
            if (dictionaryForTypeToType.ContainsKey(keyType))
            {
                var bindType = dictionaryForTypeToType[keyType];
                var ctorInfo = Utils.GetSingleConstructor(bindType);
                var parameters = ctorInfo.GetParameters();
                if (!(parameters is null))
                {
                    var parametersObjects = new List<object>();
                    foreach (ParameterInfo parameter in parameters)
                    {
                        var type = parameter.ParameterType;
                        parametersObjects.Add(Get(type));
                    }
                    return (T)Utils.CreateInstance(bindType, parametersObjects);
                }
                return (T)Utils.CreateInstance(bindType);
            }
            else if (dictionaryForTypeToObject.ContainsKey(keyType))
                return (T)dictionaryForTypeToObject[keyType];
            else throw new ArgumentNullException("This type has not been binded yet.");
        }

        private object Get(Type keyType)
        {
            if (dictionaryForTypeToType.ContainsKey(keyType))
            {
                var bindType = dictionaryForTypeToType[keyType];
                var ctor = Utils.GetSingleConstructor(bindType);
                var parameters = ctor.GetParameters();
                if (!(parameters is null))
                {
                    var parametersObjects = new List<object>();
                    foreach (ParameterInfo parameter in parameters)
                    {
                        var type = parameter.ParameterType;
                        parametersObjects.Add(Get(type));
                    }
                    return Utils.CreateInstance(bindType, parametersObjects);
                }
                return Utils.CreateInstance(bindType);
            }
            else if (dictionaryForTypeToObject.ContainsKey(keyType))
                return dictionaryForTypeToObject[keyType];
            else throw new ArgumentNullException("This type has not been binded yet.");
        }

    }
}