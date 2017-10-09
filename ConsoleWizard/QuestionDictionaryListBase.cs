﻿using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionDictionaryListBase<TDictionary, T> : QuestionBase<T> where TDictionary : Dictionary<ConsoleKey, T>
    {
        protected QuestionDictionaryListBase(string message) : base(message)
        {
        }

        internal TDictionary Choices { get; set; }
    }
}