using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;
using IGenericEntity = PrintPDV.Utility.Models.IGenericEntity;

namespace PrintPDV.Utility
{
    public static class ExtensionUtility
    {
        public static List<string> ValidateAnnotations<T>(this T value) where T : class, IGenericEntity
        {
            var context = new ValidationContext(value, null, null);
            IList<ValidationResult> errors = new List<ValidationResult>();

            return !Validator.TryValidateObject(value, context, errors, true) ? errors.Select(x => x.ErrorMessage).ToList() : null;
        }

        public static string SerializeToXml<T>(this T value)
        {
            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }

        public static T DeserializeFromXml<T>(this string value)
        {
            var returnValue = default(T);

            var serial = new XmlSerializer(typeof(T));
            var reader = new StringReader(value);
            var result = serial.Deserialize(reader);

            if (result is T)
                returnValue = (T)result;

            reader.Close();

            return returnValue;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string ToJSON<T>(this T value)
        {
            return JsonConvert.SerializeObject(value, (Newtonsoft.Json.Formatting) Formatting.None);
        }

        public static T FromJSON<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
