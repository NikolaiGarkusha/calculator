using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class ParserException : ApplicationException
    {
        public ParserException(string str) : base(str) { }
        public override string ToString()
        {
            return Message;
        }
    }

    class Parser
    {
        enum Types { NONE, DELIMITER, VARIABLE, NUMBER };
        enum Errors { SYNTAX, UNBALPARENS, NOEXP, DIVBYZERO };

        public int fact(int num)
        {
            return (num == 0) ? 1 : num * fact(num - 1);
        }

        string exp;
        int expldx;
        string token;
        Types tokType;
        
        public string Evaluate(string expstr)
        {
            double result;
            exp = expstr;
            expldx = 0;

            try
            {
                GetToken();
                if (token == "")
                {
                    SyntaxErr(Errors.NOEXP);
                    return "0.0";
                }
                EvalExp2(out result);
                if (token != "")
                    SyntaxErr(Errors.SYNTAX);
                return result.ToString();
            } catch (ParserException exc)
            {
                return exc.ToString();
            }
        }

        void EvalExp2(out double result)
        {
            double partialResult;
            string op;
            EvalExp3(out result);
            while ((op = token) == "+" || op == "-")
            {
                GetToken();
                EvalExp3(out partialResult);
                switch (op)
                {
                    case "-":
                        result = result - partialResult;
                        break;
                    case "+":
                        result = result + partialResult;
                        break;
                }
            }
        }

        void EvalExp3(out double result)
        {
            string op;
            double partialResult = 0.0;

            EvalExp4(out result);
            while ((op = token) == "*" || op == "/" || op == "%")
            {
                GetToken();
                EvalExp4(out partialResult);
                switch (op)
                {
                    case "*":
                        result = result * partialResult;
                        break;
                    case "/":
                        if (partialResult == 0.0)
                            SyntaxErr(Errors.DIVBYZERO);
                        result = result / partialResult;
                        break;
                    case "%":
                        if (partialResult == 0.0)
                            SyntaxErr(Errors.DIVBYZERO);
                        result = (int)result % (int)partialResult;
                        break;
                }
            }
        }

        void EvalExp4(out double result)
        {
            string op;
            double partialResult = 0.0;

            EvalExp5(out result);
            while ((op = token) == "^")
            {
                GetToken();
                EvalExp5(out partialResult);
                if (op == "^")
                {
                    result = Math.Pow(result, partialResult);
                }
            }
        }

        void EvalExp5(out double result)
        {
            string op = "";

            if ((tokType == Types.DELIMITER) && token == "+" || token == "-")
            {
                op = token;
                GetToken();
            }
            EvalExp6(out result);
            if (op == "-")
                result = -result;
        }

        void EvalExp6(out double result)
        {
            string op = "";
            EvalExp7(out result);
            if (token == "!")
            {
                op = token;
                GetToken();
                result = fact((int)result);
            }
        }

        void EvalExp7(out double result)
        {
            if ((token == "("))
            {
                GetToken();
                EvalExp2(out result);
                if (token != ")")
                    SyntaxErr(Errors.UNBALPARENS);
                GetToken();
            }
            else Atom(out result);
        }

        void Atom(out double result)
        {
            switch (tokType)
            {
                case Types.NUMBER:
                    try
                    {
                        result = Double.Parse(token);
                    }
                    catch (FormatException)
                    {
                        result = 0.0;
                        SyntaxErr(Errors.SYNTAX);
                    }
                    GetToken();
                    return;
                default:
                    result = 0.0;
                    SyntaxErr(Errors.SYNTAX);
                    break;
            }
        }

        void SyntaxErr(Errors error)
        {
            string[] err = { "Синтаксическая ошибка", "Дисбаланс скобок", "Выражение отсутствует", "Деление на ноль" };
            throw new ParserException(err[(int)error]);
        }

        void GetToken()
        {
            tokType = Types.NONE;
            token = "";
            if (expldx == exp.Length) return; 
            while (expldx < exp.Length && Char.IsWhiteSpace(exp[expldx]))
                ++expldx;
            if (expldx == exp.Length)
                return;
            if (IsDelim(exp[expldx]))
            { 
                token += exp[expldx];
                expldx++;
                tokType = Types.DELIMITER;
            }
            else if (Char.IsLetter(exp[expldx]))
            { 
                while (!IsDelim(exp[expldx]))
                {
                    token += exp[expldx];
                    expldx++;
                    if (expldx >= exp.Length) break;
                }
                tokType = Types.VARIABLE;
            }
            else if (Char.IsDigit(exp[expldx]))
            { 
                while (!IsDelim(exp[expldx]))
                {
                    token += exp[expldx];
                    expldx++;
                    if (expldx >= exp.Length) break;
                }
                tokType = Types.NUMBER;
            }
        }
        bool IsDelim(char с)
        {
            if ((" +-/*%^=()!".IndexOf(с) != -1))
                return true;
            return false;
        }
    }   
}
