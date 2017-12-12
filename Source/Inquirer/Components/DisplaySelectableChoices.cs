﻿using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplaySelectableChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private List<Selectable<TResult>> _choices;

        private IConvertToStringTrait<TResult> _convert;

        public DisplaySelectableChoices(List<Selectable<TResult>> choices, IConvertToStringTrait<TResult> convert)
        {
            _choices = choices;
            _convert = convert;
        }

        public void Render()
        {
            int index = 0;
            foreach (Selectable<TResult> choice in _choices)
            {
                AppConsole2.PositionWriteLine($"     {_convert.Convert.Run(choice.Item)}", 0, index + _CURSOR_OFFSET);
                AppConsole2.PositionWriteLine(choice.IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            AppConsole2.PositionWriteLine($"->   {_convert.Convert.Run(_choices[index].Item)}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
            AppConsole2.PositionWriteLine(_choices[index].IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
