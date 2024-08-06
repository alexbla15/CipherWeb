﻿using CipherData;
using CipherData.Models;
using CipherWeb.Data;
using System.Reflection;
using System.Security.Cryptography;

namespace CipherWeb
{
    public class CommonFuncs
    {
        public static string[] GetFields(Type a)
        {
            FieldInfo[] fields = a.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            List<string> fields_names = new List<string> { };
            foreach (FieldInfo field in fields)
            {
                fields_names.Add(field.Name.Replace("k__BackingField", "").Replace("<","").Replace(">",""));
            }
            return fields_names.ToArray();
        }   

        public static List<Tuple<string, string>> GetTranslatedFields(Type a)
        {
            string[] fields = GetFields(a);
            List<Tuple<string, string>> hebFields = new List<Tuple<string, string>> { };
            foreach (Tuple<string, string> h in HebrewDictionary.Headers)
            {
                if (fields.Contains(h.Item1))
                {
                    hebFields.Add(h);
                }
            }
            return hebFields;
        }

        public static string DeTranslateField(string heb_field)
        {
            foreach (Tuple<string, string> h in HebrewDictionary.Headers)
            {
                if (h.Item2 == heb_field)
                {
                    return h.Item1;
                }
            }
            return null;
        }

        public static string? FindImproperChar(string value)
        {
            string foundChar = (value.ToCharArray().Any(x => Constants.ImproperChars.Contains(x))) ? value.ToCharArray().
                Where(x => Constants.ImproperChars.Contains(x)).ToList()[0].ToString() : null;
            return foundChar;
        }

    }
}