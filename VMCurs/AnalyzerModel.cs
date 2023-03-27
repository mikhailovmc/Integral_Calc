using System;
using System.Collections.Generic;
using System.Text;

namespace VMCurs
{
    class AnalyzerModel
    {
        private enum State { S, E, F,                                                   //перечисление состояний синтаксического анализатора
            B, C, D, L, G, I, K, M, N, T, U, V, W, X, H,
            J, O, Z, P, B1, C1
        }

        private int position;                                                           //позиция курсора
        private string chain;                                                           //цепочка
        private int errorposition;                                                      //позиция ошибки
        private string errormessage;                                                    //сообщение ошибки
        private LinkedList<string> functionlist;                                        //список функций
        private LinkedList<string> operationlist;                                       //список операций
        private int brackets;                                                           //счетчик скобок
        private char perem;                                                             //переменная интегрирования

        public AnalyzerModel(string chain)
        {
            this.chain = chain;
            position = 0;
            functionlist = new LinkedList<string>();
            operationlist = new LinkedList<string>();
            if (chain.Length != 0)
            {
                perem = chain[chain.Length - 1];
            }
        }

        public int GetErrorPosition { get { return errorposition; } }                   //свойство получения позиции ошибки
        public string GetErrorMessage { get { return errormessage; } }                  //свойство получения сообщения ошибки
        public LinkedList<string> GetFunctionList { get { return functionlist; } }      //свойство получения списка функций
        public string GetVariable { get { return perem.ToString(); } }

        private bool CheckBrackets()                                                    //метод проверки количества скобок
        {
            return brackets == 0;
        }

        public bool AnalyzeChain()                                                      //метод анализа цепочки
        {
            State state = State.S;
            string func = "";
            string oper = "";
            brackets = 0;

            while (state != State.E && state != State.F)
            {
                if (position < chain.Length)
                {
                    char ch = char.ToLower(chain[position]);
                    switch (state)
                    {
                        case State.S:
                            {
                                if (ch == 'a')
                                {
                                    state = State.B;
                                    func += ch;
                                }
                                else if (ch == 'c')
                                {
                                    state = State.L;
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.K;
                                    func += ch;
                                }
                                else if (ch == 's')
                                {
                                    state = State.T;
                                    func += ch;
                                }
                                else if (ch == 'l')
                                {
                                    state = State.W;
                                    func += ch;
                                }
                                else if (ch == '+' || ch == '-')
                                {
                                    state = State.J;
                                    oper += ch;
                                    operationlist.AddLast(oper);
                                    functionlist.AddLast("0");
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == perem)
                                {
                                    state = State.O;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch == 'e')
                                {
                                    state = State.B1;
                                    func += ch;
                                    //functionlist.AddLast(func);
                                    //func = "";
                                }
                                else if (Char.IsDigit(ch))
                                {
                                    state = State.B1;
                                    func += ch;
                                }
                                else if (ch == 'p')
                                {
                                    state = State.C1;
                                    func += ch;
                                }
                                else if (ch == 'd')
                                {
                                    state = State.Z;
                                    functionlist.AddLast("1");
                                }
                                else if (ch == '(')
                                {
                                    brackets++;
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch != ' ')
                                {
                                    errormessage = "Ожидалась константа или функция";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.B:
                            {
                                if (ch == 'r')
                                {
                                    state = State.C;
                                    func += ch;
                                }
                                else
                                { 
                                    errormessage = "Ожидалось arc";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.C:
                            {
                                if (ch == 'c')
                                {
                                    state = State.D;
                                    func += ch;
                                }
                                else
                                {
                                    state = State.E;
                                    errormessage = "Ожидалось arc";
                                }
                                break;
                            }
                        case State.D:
                            {
                                if (ch == 'c')
                                {
                                    state = State.L;
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.K;
                                    func += ch;
                                }
                                else if (ch == 's')
                                {
                                    state = State.M;
                                    func += ch;
                                }
                                else 
                                {
                                    errormessage = "Ожидалось sin, cos, tg, ctg";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.L:
                            {
                                if (ch == 'o')
                                {
                                    state = State.G;
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.I;
                                    func += ch;
                                }
                                else
                                {
                                    errormessage = "Ожидалось cos, ct";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.G:
                            {
                                if (ch == 's')
                                {
                                    state = State.H;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else
                                {
                                    errormessage = "Ожидалось cos";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.I:
                            {
                                if (ch == 'g')
                                {
                                    state = State.H;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else
                                {
                                    errormessage = "Ожидалось ctg";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.K:
                            {
                                if (ch == 'g')
                                {
                                    state = State.H;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else 
                                {
                                    errormessage = "Ожидалось tg";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.M:
                            {
                                if (ch == 'i')
                                {
                                    state = State.N;
                                    func += ch;
                                }
                                else
                                {
                                    errormessage = "Ожидалось sin";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.N:
                            {
                                if (ch == 'n')
                                {
                                    state = State.H;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else
                                {
                                    errormessage = "Ожидалось sin";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.T:
                            {
                                if (ch == 'i')
                                {
                                    state = State.N;
                                    func += ch;
                                }
                                else if (ch == 'q')
                                {
                                    state = State.U;
                                    func += ch;
                                }
                                else
                                {
                                    errormessage = "Ожидалось sin или sqrt";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.U:
                            {
                                if (ch == 'r')
                                {
                                    state = State.V;
                                    func += ch;
                                }
                                else
                                {
                                    errormessage = "Ожидалось sqrt";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.V:
                            {
                                if (ch == 't')
                                {
                                    state = State.H;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else
                                {
                                    errormessage = "Ожидалось sqrt";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.W:
                            {
                                if (ch == 'n')
                                {
                                    state = State.H;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch == 'o')
                                {
                                    state = State.X;
                                    func += ch;
                                }
                                else 
                                {
                                    errormessage = "Ожидалось log или ln";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.X:
                            {
                                if (ch == 'g')
                                {
                                    state = State.H;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else 
                                {
                                    errormessage = "Ожидалось log";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.J:
                            {
                                if (ch == ' ')
                                    state = State.S;
                                else if (ch == '(')
                                {
                                    brackets++;
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                    state = State.S;
                                }
                                else if (ch == 'a')
                                {
                                    func += ch;
                                    state = State.B;
                                }
                                else if (ch == 'c')
                                {
                                    state = State.L;
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.K;
                                    func += ch;
                                }
                                else if (ch == 's')
                                {
                                    state = State.T;
                                    func += ch;
                                }
                                else if (ch == 'l')
                                {
                                    state = State.W;
                                    func += ch;
                                }
                                else if (ch == perem)
                                {
                                    state = State.O;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch == 'e')
                                {
                                    state = State.B1;
                                    func += ch;
                                    //functionlist.AddLast(func);
                                    //func = "";
                                }
                                else if (Char.IsDigit(ch))
                                {
                                    state = State.B1;
                                    func += ch;
                                }
                                else if (ch == 'p')
                                {
                                    state = State.C1;
                                    func += ch;
                                }
                                else if (ch == 'd')
                                {
                                    state = State.Z;
                                    functionlist.AddLast("1");
                                }
                                else
                                {
                                    errormessage = "Ожидалась функция или кшнстанта";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.C1:
                            {
                                if (ch == 'i')
                                {
                                    state = State.B1;
                                    func += ch;
                                    //functionlist.AddLast(func);
                                    //func = "";
                                }
                                else
                                {
                                    errormessage = "Ожидалось pi";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.B1:
                            {
                                if (ch == 'p')
                                {
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    state = State.C1;
                                    func += ch;
                                }
                                else if (ch == '(')
                                {
                                    state = State.S;
                                    brackets++;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                }
                                //else if (ch == ' ')
                                //{
                                //    state = State.S;
                                //    functionlist.AddLast(func);
                                //    func = "";
                                //}
                                else if (ch == 'a')
                                {
                                    state = State.B;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'c')
                                {
                                    state = State.L;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.K;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 's')
                                {
                                    state = State.T;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'l')
                                {
                                    state = State.W;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == perem)
                                {
                                    state = State.O;
                                    //func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch == 'd')
                                {
                                    state = State.Z;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch == '+' || ch == '-' || ch == '/' || ch == '*' || ch == '^')
                                {
                                    state = State.H;
                                    oper += ch;
                                    operationlist.AddLast(oper);
                                    oper = "";
                                    functionlist.AddLast(func);
                                    func = "";
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == ')')
                                {
                                    state = State.P;
                                    brackets--;
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast(")");
                                }
                                else if (ch == 'e')
                                {
                                    functionlist.AddLast(func);
                                    func = "";
                                    functionlist.AddLast("*");
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (Char.IsDigit(ch) || ch == ' ')
                                {
                                    func += ch;
                                }
                                else
                                {
                                    errormessage = "Ожидалось функция, константа, операция";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.H:
                            {
                                if (ch == '(')
                                {
                                    state = State.S;
                                    brackets++;
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == ' ')
                                {
                                    state = State.S;
                                }
                                else if (ch == 'a')
                                {
                                    state = State.B;
                                    func += ch;
                                }
                                else if (ch == 'c')
                                {
                                    state = State.L;
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.K;
                                    func += ch;
                                }
                                else if (ch == 's')
                                {
                                    state = State.T;
                                    func += ch;
                                }
                                else if (ch == 'l')
                                {
                                    state = State.W;
                                    func += ch;
                                }
                                else if (ch == perem)
                                {
                                    state = State.O;
                                    func += ch;
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch == 'e')
                                {
                                    state = State.B1;
                                    func += ch;
                                    //functionlist.AddLast(func);
                                    //func = "";
                                }
                                else if (Char.IsDigit(ch))
                                {
                                    state = State.B1;
                                    func += ch;
                                }
                                else if (ch == 'p')
                                {
                                    state = State.C1;
                                    func += ch;
                                }
                                else 
                                {
                                    errormessage = "Ожидалось функция, константа, операнд";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.O:
                            {
                                if (ch == '+' || ch == '-' || ch == '/' || ch == '*' || ch == '^')
                                {
                                    state = State.H;
                                    oper += ch;
                                    operationlist.AddLast(oper);
                                    oper = "";
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == '(')
                                {
                                    state = State.S;
                                    brackets++;
                                    oper += ch;
                                    functionlist.AddLast("*");
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == ')')
                                {
                                    state = State.P;
                                    brackets--;
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == 'd')
                                {
                                    state = State.Z;
                                }
                                else if (ch == 'e')
                                {
                                    state = State.B1;
                                    functionlist.AddLast("*");
                                    func += ch;
                                    //functionlist.AddLast(func);
                                    //func = "";
                                }
                                else if (Char.IsDigit(ch))
                                {
                                    state = State.B1;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'p')
                                {
                                    state = State.C1;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'a')
                                {
                                    state = State.B;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'c')
                                {
                                    state = State.L;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.K;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 's')
                                {
                                    state = State.T;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == perem)
                                {
                                    func += ch;
                                    functionlist.AddLast("*");
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch != ' ')
                                {
                                    errormessage = "Ожидалось функция, константа, операция";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.P:
                            {
                                if (ch == '(')
                                {
                                    state = State.S;
                                    brackets++;
                                    oper += ch;
                                    functionlist.AddLast("*");
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == 'a')
                                {
                                    state = State.B;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'c')
                                {
                                    state = State.D;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 't')
                                {
                                    state = State.K;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 's')
                                {
                                    state = State.T;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'l')
                                {
                                    state = State.W;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == '+' || ch == '-' || ch == '/' || ch == '*' || ch == '^')
                                {
                                    state = State.H;
                                    oper += ch;
                                    operationlist.AddLast(oper);
                                    oper = "";
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch == 'p')
                                {
                                    state = State.C1;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == perem)
                                {
                                    state = State.O;
                                    func += ch;
                                    functionlist.AddLast("*");
                                    functionlist.AddLast(func);
                                    func = "";
                                }
                                else if (ch == 'e')
                                {
                                    state = State.B1;
                                    functionlist.AddLast("*");
                                    func += ch;
                                    //functionlist.AddLast(func);
                                    //func = "";
                                }
                                else if (Char.IsDigit(ch))
                                {
                                    state = State.B1;
                                    functionlist.AddLast("*");
                                    func += ch;
                                }
                                else if (ch == 'd')
                                {
                                    state = State.Z;
                                }
                                else if (ch == ')')
                                {
                                    brackets--;
                                    oper += ch;
                                    functionlist.AddLast(oper);
                                    oper = "";
                                }
                                else if (ch != ' ')
                                {
                                    errormessage = "Ожидалось функция, константа, операция";
                                    state = State.E;
                                }
                                break;
                            }
                        case State.Z:
                            {
                                if (ch == perem)
                                {
                                    state = State.F;
                                }
                                else
                                {
                                    errormessage = "Ожидалась переменная интегрирования";
                                    state = State.E;
                                }
                                break;
                            }
                        default:
                            {
                                errormessage = "Неизвестная ошибка";
                                state = State.E;
                                break;
                            }
                    }
                }
                else
                {
                    errormessage = "Курсор вышел за пределы строки";
                    state = State.E;
                }
                position++;
            }
            if (state == State.E)
                errorposition = position--;
            return state == State.F && CheckBrackets();
        }
    }
}