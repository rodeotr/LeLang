﻿using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learning;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabAddExpressionCommand : CommandBase
    {
        TabAddExpressionViewModel _vm;
        public TabAddExpressionCommand(TabAddExpressionViewModel vm)
        {
            _vm = vm;
        }

        public override void Execute(object parameter)
        {
            if(_vm.Expression.Length > 0)
            {
                createExpression();
                _vm.Expression = "";
            }
            
            
        }
        private void createExpression()
        {
            IdiomsAndExpressions expression = new IdiomsAndExpressions()
            {
                Text = _vm.Expression
            };
            WordServices.addExpression(expression);
        }
    }
}
