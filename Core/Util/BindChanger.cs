using System;
using System.Reflection;

namespace Util
{
    internal sealed class BindChanger : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            // Define the new type to bind to
            Type typeToDeserialize = null;

            // Get the current assembly
            string currentAssembly = Assembly.GetExecutingAssembly().FullName;

            // Create the new type and return it
            typeToDeserialize = Type.GetType(string.Format("{0}, {1}", typeName, currentAssembly));

            return typeToDeserialize;
        }
    }
}