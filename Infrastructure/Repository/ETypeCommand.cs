using System.Reflection;

namespace Infrastructure.Repository
{
    public enum ETypeCommand
    {
        [StringValue("update")]
        Update,
        [StringValue("delete")]
        Delete,
        [StringValue("insert")]
        Insert,
    }

    public class StringValue : Attribute
    {
        public string Value { get; private set; }
        public StringValue(string value)
        {
            Value = value;
        }
    }

    public static class Extension
    {
        public static string GetStringValue(this Enum value)
        {
            string stringValue = value.ToString();
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString())!;
            StringValue[]? attrs = fieldInfo.
                GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs!.Length > 0)
            {
                stringValue = attrs[0].Value;
            }
            return stringValue;
        }
    }
}
