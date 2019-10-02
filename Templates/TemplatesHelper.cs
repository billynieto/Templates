using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Templates.Framework;

namespace Templates
{
    public enum PrintVariableType
    {
        Reference,
        InstanceName,
        Variable
    }

    public static class TemplatesHelper
    {
        public static IList<char> Characters = new List<char>(){
        	'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        	'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static string DefaultSeperator = ", ";
        public static Random Random = new Random(DateTime.Now.Millisecond);
        public static string WhiteSpace = @" \t\r\n ";

        public static string ColumnCounter(string prefix, string _value)
        {
            string counter = string.Empty;

            if (_value.Length > 10)
            {
                counter = "//";
                for (int i = 2; i < prefix.Length; i++)
                    counter += " ";
                for (int i = 0; i < (_value.Length + (10 - _value.Length % 10)); i++)
                {
                    string stamp = (i + 1).ToString();

                    if ((i + 1) % 10 == 0)
                    {
                        counter += stamp;
                        i += stamp.Length - 1;
                    }
                    else
                    {
                        counter += ".";
                    }
                }
            }
            else if (_value.Length > 5)
            {
                counter = "//";
                for (int i = 2; i < prefix.Length; i++)
                    counter += " ";
                for (int i = 0; i < _value.Length; i++)
                    counter += i.ToString();
            }

            return counter;
        }

        public static IGenericVariableType EnumerableOf(IVariableType _type)
        {
            return new TemplateGenericVariableType("IEnumerable", new List<IVariableType>() { _type });
        }

        public static string FormatBool(bool value)
        {
            if (value)
                return "true";

            return "false";
        }

        public static string FormatDate(DateTime value)
        {
            return new StringBuilder()
                .Append("new DateTime(")
                .Append(value.Year).Append(DefaultSeperator)
                .Append(value.Month).Append(DefaultSeperator)
                .Append(value.Day)
                .Append(")").ToString();
        }

        public static string FormatDateTime(DateTime value)
        {
            return new StringBuilder()
                .Append("new DateTime(")
                .Append(value.Year).Append(DefaultSeperator)
                .Append(value.Month).Append(DefaultSeperator)
                .Append(value.Day).Append(DefaultSeperator)
                .Append(value.Hour).Append(DefaultSeperator)
                .Append(value.Minute).Append(DefaultSeperator)
                .Append(value.Second)
                .Append(")").ToString();
        }

        public static string FormatDouble(double value)
        {
            if (value == double.MaxValue)
                return "double.MaxValue";
            if (value == double.MinValue)
                return "double.MinValue";

            return value.ToString(".#####################################################################################################################################################################################################################################################################################################################################") + "d";
        }

        public static string FormatEnumeration(IEnumerationItem item)
        {
            IEnumeration _enumeration = item.Enumeration;

            StringBuilder stringBuilder = new StringBuilder();

            if (TemplateClass.SystemClasses.Any(_systemClass => _systemClass.Name == _enumeration.Name))
                stringBuilder.Append(_enumeration.Namespace.Name).Append(".");

            stringBuilder.Append(_enumeration.Name).Append(".").Append(item.Name);

            return stringBuilder.ToString();
        }

        public static string FormatInt(int value)
        {
            return value.ToString();
        }

        public static string FormatString(string value)
        {
            if (value == null)
                return "null";

            if (value == string.Empty)
                return "string.Empty";

            return new StringBuilder().Append("\"").Append(value).Append("\"").ToString();
        }

        public static string FormatTimeSpan(TimeSpan value)
        {
            if (value == null)
                return "null";

            if (value == TimeSpan.MaxValue)
                return "TimeSpan.MaxValue";
            if (value == TimeSpan.MinValue)
                return "TimeSpan.MinValue";
            if (value == TimeSpan.Zero)
                return "TimeSpan.Zero";

            return new StringBuilder()
                .Append("new TimeSpan(")
                .Append(value.Days).Append(DefaultSeperator)
                .Append(value.Hours).Append(DefaultSeperator)
                .Append(value.Minutes).Append(DefaultSeperator)
                .Append(value.Seconds).Append(DefaultSeperator)
                .Append(value.Milliseconds)
                .Append(")").ToString();
        }

        public static IGenericVariableType ListOf(IVariableType _type)
        {
            return new TemplateGenericVariableType("List", new List<IVariableType>() { _type });
        }

        public static string ListOutParameters(IList<IVariable> parameters)
        {
            return TemplatesHelper.ListOutParameters(parameters, TemplatesHelper.DefaultSeperator);
        }

        public static string ListOutParameters(IList<IVariable> parameters, string seperator)
        {
            string parametersListed = string.Empty;

            if (parameters != null && parameters.Count > 0)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (i > 0)
                        parametersListed += seperator;

                    parametersListed += parameters[i].VariableType.Name + " " + parameters[i].InstanceName;
                }
            }

            return parametersListed;
        }

        public static string ListOutSortedParameters(IList<IVariable> parameters)
        {
            SortedList<string, IVariable> sortedParameters = new SortedList<string, IVariable>();

            if (parameters != null)
                foreach (IVariable parameter in parameters)
                    sortedParameters.Add(parameter.InstanceName, parameter);

            return ListOutParameters(sortedParameters.Values);
        }

        public static string ListOutSortedVariables(IList<IVariable> variables)
        {
            return ListOutSortedVariables(null, variables, null);
        }

        public static string ListOutSortedVariables(string prefix, IList<IVariable> variables)
        {
            return ListOutSortedVariables(prefix, variables, null);
        }

        public static string ListOutSortedVariables(IList<IVariable> variables, string suffix)
        {
            return ListOutSortedVariables(null, variables, suffix);
        }

        public static string ListOutSortedVariables(string prefix, IList<IVariable> variables, string suffix)
        {
            SortedList<string, IVariable> sortedVariables = new SortedList<string, IVariable>();

            if (variables != null)
                foreach (IVariable variable in variables)
                    sortedVariables.Add(variable.InstanceName, variable);

            return ListOutVariables(prefix, sortedVariables.Values, suffix, TemplatesHelper.DefaultSeperator, PrintVariableType.InstanceName);
        }

        public static string ListOutVariables(IList<IVariable> variables)
        {
            return ListOutVariables(null, variables, null, TemplatesHelper.DefaultSeperator, PrintVariableType.InstanceName);
        }

        public static string ListOutVariables(string prefix, IList<IVariable> variables)
        {
            return ListOutVariables(prefix, variables, null, TemplatesHelper.DefaultSeperator, PrintVariableType.InstanceName);
        }

        public static string ListOutVariables(string prefix, IList<IVariable> variables, PrintVariableType print)
        {
            return ListOutVariables(prefix, variables, null, TemplatesHelper.DefaultSeperator, print);
        }

        public static string ListOutVariables(IList<IVariable> variables, string seperator)
        {
            return ListOutVariables(null, variables, null, seperator, PrintVariableType.InstanceName);
        }

        public static string ListOutVariables(IList<IVariable> variables, string seperator, PrintVariableType print)
        {
            return ListOutVariables(null, variables, null, seperator, print);
        }

        public static string ListOutVariables(string prefix, IList<IVariable> variables, string seperator, PrintVariableType print)
        {
            return ListOutVariables(prefix, variables, null, seperator, print);
        }

        public static string ListOutVariables(string prefix, IList<IVariable> variables, string suffix, string seperator, PrintVariableType print)
        {
            string variablesListed = string.Empty;

            if (variables != null && variables.Count > 0)
            {
                for (int i = 0; i < variables.Count; i++)
                {
                    if (i > 0)
                        variablesListed += seperator;

                    if (!string.IsNullOrWhiteSpace(prefix))
                        variablesListed += prefix;

                    if (print == PrintVariableType.InstanceName)
                        variablesListed += variables[i].InstanceName;
                    else if(print == PrintVariableType.Reference)
                        variablesListed += variables[i].Reference;
                    else if (print == PrintVariableType.Variable)
                        variablesListed += variables[i].VariableType.Name;

                    if (!string.IsNullOrWhiteSpace(suffix))
                        variablesListed += suffix;
                }
            }

            return variablesListed;
        }

        public static string NewOf(IVariableType variableType)
        {
            return "new " + variableType + "()";
        }

        public static string Pluralize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            if (value.EndsWith("ey"))
                return value + "s";
            else if (value.EndsWith("y"))
                return value.Substring(0, value.Length - 1) + "ies";
            else if (value.EndsWith("ss"))
                return value + "es";
            else if (value.EndsWith("s"))
                return value + "es";
            else
                return value + "s";
        }

        public static bool RandomBool()
        {
            if (TemplatesHelper.RandomInt(0, 1) == 0)
                return false;

            return true;
        }

        public static DateTime RandomDateTime()
        {
            int year = TemplatesHelper.Random.Next(1901, 2078);
            int month = TemplatesHelper.Random.Next(1, 12);
            int day = TemplatesHelper.Random.Next(1, DateTime.DaysInMonth(year, month));
            int hour = TemplatesHelper.Random.Next(23);
            int minute = TemplatesHelper.Random.Next(59);
            int second = TemplatesHelper.Random.Next(59);

            return new DateTime(year, month, day, hour, minute, second);
        }

        public static double RandomDouble(double minimum, double maximum)
        {
            if (minimum == maximum)
                return minimum;

            double span = maximum - minimum;

            return TemplatesHelper.Random.NextDouble() * span + minimum;
        }

        public static IEnumerationItem RandomEnumerationItem(IEnumeration enumeration)
        {
            int randomIndex = TemplatesHelper.Random.Next(1, enumeration.Items.Count) - 1;

            return enumeration.Items[randomIndex];
        }

        public static int RandomInt(int minimum, int maximum)
        {
            if (minimum == maximum)
                return minimum;

            if (minimum == int.MinValue && maximum == int.MaxValue)
            {
                if (TemplatesHelper.RandomBool())
                    return -TemplatesHelper.Random.Next();
                else
                    return TemplatesHelper.Random.Next();
            }

            int span = maximum - minimum;

            return TemplatesHelper.Random.Next(span) + minimum;
        }

        public static string RandomString(int length)
        {
            return RandomStringMaxedOut(TemplatesHelper.Random.Next(1, length));
        }

        public static string RandomStringMaxedOut(int length)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
                stringBuilder.Append(TemplatesHelper.Characters[TemplatesHelper.Random.Next(TemplatesHelper.Characters.Count)]);

            return stringBuilder.ToString();
        }

        public static TimeSpan RandomTimeSpan()
        {
            int days = TemplatesHelper.Random.Next(10);
            int hours = TemplatesHelper.Random.Next(1, 24);
            int minutes = TemplatesHelper.Random.Next(59);
            int seconds = TemplatesHelper.Random.Next(59);
            int milliseconds = TemplatesHelper.Random.Next(999);

            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }
    }
}
