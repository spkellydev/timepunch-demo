using System;
using System.ComponentModel;
using System.Reflection;

namespace timepunch.Exceptions
{
    [Serializable]
    public class TimepunchException : Exception
    {
        public TimepunchException() { }
        public TimepunchException(string message) : base(message) { }
        public TimepunchException(string message, Exception inner) : base(message, inner) { }
        protected TimepunchException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public TimepunchException(Enum ex) : base(ex.GetDescription()) { }
    }

    /// <summary>
    /// Static class for enum extension methods.
    /// </summary>
    public static class EnumHelper
    {

        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}