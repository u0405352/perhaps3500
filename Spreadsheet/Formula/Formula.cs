// Skeleton written by Joe Zachary for CS 3500, January 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  Provides a means to evaluate Formulas.  Formulas can be composed of
    /// non-negative floating-point numbers, variables, left and right parentheses, and
    /// the four binary operator symbols +, -, *, and /.  (The unary operators + and -
    /// are not allowed.)
    /// </summary>
    public class Formula
    {
        /// <summary>
        /// Creates a Formula from a string that consists of a standard infix expression composed
        /// from non-negative floating-point numbers (using standard C# syntax for double/int literals), 
        /// variable symbols (one or more letters followed by one or more digits), left and right
        /// parentheses, and the four binary operator symbols +, -, *, and /.  White space is
        /// permitted between tokens, but is not required.
        /// 
        /// An example of a valid parameter to this constructor is "2.5e9 + x5 / 17".
        /// Examples of invalid parameters are "x", "-5.3", and "2 5 + 3";
        /// 
        /// If the formula is syntacticaly invalid, throws a FormulaFormatException with an 
        /// explanatory Message.
        /// </summary>
        public Formula(String formula)
        {
            int leftParen = 0;
            int rightParen = 0;
            string prevToken = null;

            //try
            //{
            if (formula.Length < 1)
            {
                throw new FormulaFormatException("The Formula Must Have At Least One Token");
            }

            foreach (string s in GetTokens(formula))
            {
                if (s == "!" || s == "@" || s == "#" || s == "$" || s == "%" || s == "^" || s == "&" || s == "_"
                    || s == "=" || s == "[" || s == "}" || s == "{" || s == "]" || s == "<" || s == ">" || s == "?"
                    || s == "," || s == "." || s == "\"" || s == "\\" || s == ":" || s == ";" || s == "`" || s == "~")
                {
                    throw new FormulaFormatException("Illegal Token");
                }

                if (prevToken == null)
                {
                    if (s == ")" || s == "*" || s == "+" || s == "-" || s == "/")
                    {
                        throw new FormulaFormatException("The Formula Must Begin With Either an Opening Parentheses, A Variable, Or A Number");
                    }

                }
                else if(prevToken == "(" || prevToken == "+" || prevToken == "-" || prevToken == "/" || prevToken == "*")
                {
                    if (s == ")" || s == "*" || s == "+" || s == "-" || s == "/")
                    {
                        throw new FormulaFormatException("Tokens following an open parenthesis or an operator may not be a closed parenthesis or another operator");
                    }
                }
                else
                {
                    if (!(s == ")" || s == "*" || s == "+" || s == "-" || s == "/"))
                    {
                        throw new FormulaFormatException("Tokens following a number, variable or closing parenthises must be either another closing parenthesis or an operator");
                    }
                }
                
                if (s == "(")
                {
                    leftParen++;
                }
                if (s == ")")
                {
                    rightParen++;
                }
                if (rightParen > leftParen)
                {
                    throw new FormulaFormatException("The Formula Is Incorrectly Parenthesized");
                }

                prevToken = s;
            }

            if (prevToken != null && prevToken == "(" || prevToken == "*" || prevToken == "+" || prevToken == "-" || prevToken == "/")
            {
                throw new FormulaFormatException("The Formula Must End With Either a Closing Parentheses, A Variable, Or A Number");
            }

            if (leftParen != rightParen)
            {
                throw new FormulaFormatException("The Formula Has An Unequal Number Of Parenthesis");
            }

            // }
            //catch (Exception e)
            //{
            //    Debug.Indent();
            //    Debug.WriteLine(e.Message);
            //}
        }

        /// <summary>
        /// A Lookup function is one that maps some strings to double values.  Given a string,
        /// such a function can either return a double (meaning that the string maps to the
        /// double) or throw an IllegalArgumentException (meaning that the string is unmapped.
        /// Exactly how a Lookup function decides which strings map to doubles and which
        /// don't is up to the implementation of that function.
        /// </summary>
        public delegate double Lookup(string s);

        /// <summary>
        /// Evaluates this Formula, using lookup to determine the values of variables.  
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, throw a FormulaEvaluationException  
        /// with an explanatory Message.
        /// </summary>
        public double Evaluate(Lookup lookup)
        {
            return 0;
        }

        /// <summary>
        /// Given a formula, enumerates the tokens that compose it.  Tokens are left paren,
        /// right paren, one of the four operator symbols, a string consisting of one or more
        /// letters followed by one or more digits, a double literal, and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z]+\d+";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used to report errors that occur when evaluating a Formula.
    /// </summary>
    public class FormulaEvaluationException : Exception
    {
        /// <summary>
        /// Constructs a FormulaEvaluationException containing the explanatory message.
        /// </summary>
        public FormulaEvaluationException(String message)
            : base(message)
        {
        }
    }


}
