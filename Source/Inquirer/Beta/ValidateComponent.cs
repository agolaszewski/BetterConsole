﻿using System;
using System.Collections.Generic;

namespace InquirerCS.Beta
{
    public class ValidateComponent<T> : IValidateComponent<T>
    {
        private List<Tuple<Func<T, bool>, Func<T, string>>> _validators = new List<Tuple<Func<T, bool>, Func<T, string>>>();

        public ValidateComponent()
        {
        }

        public string ErrorMessage { get; set; }

        public bool HasError { get; set; }

        public void Run(T value)
        {
            foreach (var validator in _validators)
            {
                if (!validator.Item1(value))
                {
                    HasError = true;
                    ErrorMessage = validator.Item2(value);
                    return;
                }
            }
        }

        public IValidateComponent<T> AddValidator(Func<T, bool> fn, Func<T, string> errorMessageFn)
        {
            _validators.Add(new Tuple<Func<T, bool>, Func<T, string>>(fn, errorMessageFn));
            return this;
        }

        public IValidateComponent<T> AddValidator(Func<T, bool> fn, string errorMessage)
        {
            _validators.Add(new Tuple<Func<T, bool>, Func<T, string>>(fn, value => { return errorMessage; }));
            return this;
        }
    }
}
