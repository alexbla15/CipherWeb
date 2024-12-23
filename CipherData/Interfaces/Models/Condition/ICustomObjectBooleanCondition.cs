﻿using System.Reflection;

namespace CipherData.Interfaces
{
    public interface ICustomCondition
    {
        [HebrewTranslation(typeof(ICustomCondition), nameof(Factory))]
        IObjectFactory? Factory { get; set; }

        [HebrewTranslation(typeof(ICustomCondition), nameof(ObjectCondition))]
        IGroupedBooleanCondition? ObjectCondition { get; set; }
    }

    /// <summary>
    /// Complex boolean condition that is applied to custom 
    /// objects created from an object factory
    /// </summary>
    [HebrewTranslation(nameof(ICustomObjectBooleanCondition))]
    public interface ICustomObjectBooleanCondition : ICipherClass
    {
        /// <summary>
        /// List of object factory specifications and conditions on them
        /// </summary>
        [HebrewTranslation(typeof(CustomObjectBooleanCondition), nameof(Conditions))]
        List<ICustomCondition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean
        /// </summary>
        [HebrewTranslation(typeof(CustomObjectBooleanCondition), nameof(Operator))]
        Operator Operator { get; set; }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    public abstract class BaseCustomObjectBooleanCondition: CipherClass, ICustomObjectBooleanCondition
    {
        public List<ICustomCondition> Conditions { get; set; } = new();

        public Operator Operator { get; set; } = Operator.All;
    }
}
