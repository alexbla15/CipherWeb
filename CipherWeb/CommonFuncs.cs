﻿using CipherData.Models;
using CipherWeb.Data;
using CipherWeb.Shared.Components;
using Radzen;
using System;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherWeb
{
    public class CommonFuncs
    {
        /// <summary>
        /// Get a list of all available field within a Cipher data model.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a translation list (english,hebrew) of all available field of a Cipher data model.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get english-translation of a hebrew word.
        /// </summary>
        /// <param name="heb_field"></param>
        /// <returns></returns>
        public static string? DeTranslateField(string heb_field)
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
    }
}
