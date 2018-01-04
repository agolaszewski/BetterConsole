﻿using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;

namespace ConsoleApp1
{
    internal class TestClass
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    internal class Program : InquirerProgram
    {
        private static Inquirer _test = new Inquirer();

        private static void Main(string[] args)
        {
            Does(() =>
            {
                var derp = Question.Input("Name").Prompt();
            });

            Does(() =>
            {
                var derp = Question.Input("Name").Prompt();
            });

            Does(() =>
            {
                var derp = Question.Input("Name").Prompt();
            });

            Menu("asdasd");
            AddOption("dasdasd", () => { PagingCheckboxTest } );
            AddOption("asdasdasda");
            AddOption("dasdasd");
            AddOption("asdasdasda");
            AddOption("dasdasd");
            AddOption("asdasdasda");
            AddOption("asdasdasdasdasd");

            Console.ReadKey();
        }

        private static void Menu(string name)
        {

        }

        private static void AddOption(string name)
        {
            Does()
        }

        private static void MenuTest()
        {
            _test.Menu("Choose")
               .AddOption("PagingCheckboxTest", () => { PagingCheckboxTest(); })
               .AddOption("PagingRawListTest", () => { PagingRawListTest(); })
               .AddOption("PagingListTest", () => { PagingListTest(); })
               .AddOption("InputTest", () => { InputTest(); })
               .AddOption("PasswordTest", () => { PasswordTest(); })
               .AddOption("ListTest", () => { ListTest(); })
               .AddOption("ListRawTest", () => { ListRawTest(); })
               .AddOption("ListCheckboxTest", () => { ListCheckboxTest(); })
               .AddOption("ListExtendedTest", () => { ListExtendedTest(); })
               .AddOption("ConfirmTest", () => { ConfirmTest(); }).Prompt();

            _test.Next(() => Test());
        }

        private static void Test()
        {
            var test = new TestClass()
            {
                Name = _test.Prompt(Question.Input("Name")),
                IsActive = Question.Confirm("Is Active").Prompt()
            };

            Console.WriteLine(test.Name + " " + test.IsActive);
        }

        private static void PagingCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
                .Page(3)
                .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
                .WithConfirmation()
                .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black"));
        }

        private static void PagingRawListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.RawList("Choose favourite color", colors)
                .Page(3)
                .WithDefaultValue(ConsoleColor.DarkCyan)
                .WithConfirmation()
                .WithValidation(item => item == ConsoleColor.Black, "Choose black"));
        }

        private static void PagingListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.List("Choose favourite color", colors)
                .Page(3)
                .WithDefaultValue(ConsoleColor.DarkCyan)
                .WithConfirmation()
                .WithValidation(item => item == ConsoleColor.Black, "Choose black"));
        }

        private static void InputTest2()
        {
            _test.Prompt(Question.Input<int>("2 + 2").WithDefaultValue(4).WithConfirmation().WithValidation(value => value == 4, "Answer not equal 4"));
        }

        private static void InputTest()
        {
            string answer = _test.Prompt(Question.Input("How are you?")
                .WithDefaultValue("fine")
                .WithConfirmation()
                .WithValidation(value => value == "fine", "You cannot be not fine!"));
        }

        private static void ConfirmTest()
        {
            var answer = _test.Prompt(Question.Confirm("Are you sure?")
                .WithDefaultValue(false));
        }

        private static void PasswordTest()
        {
            string answer = _test.Prompt(Question.Password("Type password")
                .WithDefaultValue("123456789")
                .WithConfirmation()
                .WithValidation(value => value.Length >= 8 && value.Length <= 10, "Password length must be between 8-10 characters"));
        }

        private static void ListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.List("Choose favourite color", colors)
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black"));
        }

        private static void ListRawTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.RawList("Choose favourite color", colors)
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black"));
        }

        private static void ListCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
                .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
                .WithConfirmation()
                .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black"));
        }

        private static void ListExtendedTest()
        {
            var colors = new Dictionary<ConsoleKey, ConsoleColor>();
            colors.Add(ConsoleKey.B, ConsoleColor.Black);
            colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
            colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

            ConsoleColor answer = _test.Prompt(Question.ExtendedList("Choose favourite color", colors)
                .WithDefaultValue(ConsoleColor.Black)
                .WithConfirmation()
                .WithValidation(values => values == ConsoleColor.Black, "Choose black"));
        }
    }
}