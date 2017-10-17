﻿using System;
using System.Collections.Generic;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public abstract class QuestionListBase<T> : QuestionBase<T>, IConvertToString<T>
    {
        internal QuestionListBase(string message) : base(message)
        {
        }

        public Func<T, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        internal List<T> Choices { get; set; }
    }
}
