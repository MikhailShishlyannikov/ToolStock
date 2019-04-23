﻿using System;
using System.Reflection;
using System.Web.Mvc;

namespace Sam.ToolStock.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultipleButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var keyValue = $"{Name}:{Argument}";
            var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

            if (value == null) return false;
            controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;

            return true;
        }
    }
}