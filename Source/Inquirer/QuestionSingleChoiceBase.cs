﻿using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public abstract class QuestionSingleChoiceBase<TRead, TInput, TResult> : QuestionBase<TResult>
    {
        public QuestionSingleChoiceBase(string question) : base(question)
        {
        }

        internal Func<TRead> ReadFn { get; set; }

        protected Func<TResult, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        protected Func<TInput, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        protected List<Tuple<Func<TInput, bool>, Func<TInput, string>>> ValidatorsTInput { get; set; } = new List<Tuple<Func<TInput, bool>, Func<TInput, string>>>();

        protected List<Tuple<Func<TResult, bool>, Func<TResult, string>>> ValidatorsTResults { get; set; } = new List<Tuple<Func<TResult, bool>, Func<TResult, string>>>();

        public QuestionSingleChoiceBase<TRead, TInput, TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionSingleChoiceBase<TRead, TInput, TResult> Parse(Func<TInput, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionSingleChoiceBase<TRead, TInput, TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public virtual QuestionSingleChoiceBase<TRead, TInput, TResult> WithDefaultValue(TResult defaultValue, Func<TResult, TResult, int> compareFn = null)
        {
            DefaultValue = defaultValue;
            HasDefaultValue = true;
            return this;
        }

        public QuestionSingleChoiceBase<TRead, TInput, TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionSingleChoiceBase<TRead, TInput, TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        internal QuestionSingleChoiceBase<TRead, TInput, TResult> WithInputValidation(Func<TInput, bool> fn, Func<TInput, string> errorMessageFn)
        {
            ValidatorsTInput.Add(new Tuple<Func<TInput, bool>, Func<TInput, string>>(fn, errorMessageFn));
            return this;
        }

        internal QuestionSingleChoiceBase<TRead, TInput, TResult> WithInputValidation(Func<TInput, bool> fn, string errorMessage)
        {
            ValidatorsTInput.Add(new Tuple<Func<TInput, bool>, Func<TInput, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        protected virtual void DisplayQuestion()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                question += $"[{ConvertToStringFn(DefaultValue)}] ";
            }

            ConsoleHelper.Write(question);
        }

        protected bool Validate(TInput value)
        {
            foreach (var validator in ValidatorsTInput)
            {
                if (!validator.Item1(value))
                {
                    ConsoleHelper.WriteError(validator.Item2(value));
                    return false;
                }
            }

            TResult answer = default(TResult);
            try
            {
                answer = ParseFn(value);
            }
            catch
            {
                ConsoleHelper.WriteError($"Cannot parse {value} to {typeof(TResult)}");
                return false;
            }

            foreach (var validator in ValidatorsTResults)
            {
                if (!validator.Item1(answer))
                {
                    ConsoleHelper.WriteError(validator.Item2(answer));
                    return false;
                }
            }

            return true;
        }
    }
}