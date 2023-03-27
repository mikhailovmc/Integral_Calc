using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VMCurs
{
    public class MathModel
    {
        public class Tree                                      //внутренний класс - дерево, для работы преобразователя
        {
            public class PNode                                 //внутренний класс - узел дерева
            {
                private string data;                             //значение в вершине
                private PNode left;                              //левый потомок
                private PNode right;                             //правый потомок

                internal PNode()                                 //конструктор
                {
                    data = "0";
                    left = null;
                    right = null;
                }

                internal string Data { get { return data; } set { data = value; } }                  //свойства
                internal PNode Left { get { return left; } set { left = value; } }
                internal PNode Right { get { return right; } set { right = value; } }
            }

            private PNode head;                                   //вершина дерева
            LinkedList<string> functionlist;                      //массив элементов
            Stack<PNode> treestack = new Stack<PNode>();          //стек для построения дерева
            string variable;                                      //переменная интегрирования
            List<string> operations = new List<string>() { "+", "-", "*", "/", "^" };           //операции
            List<string> functions = new List<string>() { "sin", "cos", "tg", "ctg", "arcsin", "arccos", "arctg", "arcctg", "log", "ln", "sqrt" }; //функции

            public Tree(LinkedList<string> functionlist, string variable)          //конструктор
            {
                Head = new PNode();
                this.functionlist = functionlist;
                this.variable = variable;
                Head = CreateTree();
            }

            internal PNode Head { get { return head; } set { head = value; } }                     //свойство

            internal PNode CreateTree()         //метод создания дерева
            {
                int count = 0;
                while (count < functionlist.Count)
                {
                    double num = 0;
                    if (Double.TryParse(functionlist.ElementAt(count), out num) || functionlist.ElementAt(count) == "pi" || functionlist.ElementAt(count) == "e"
                        || functionlist.ElementAt(count) == variable)
                    {
                        PNode node = new PNode();
                        node.Data = functionlist.ElementAt(count);
                        treestack.Push(node);
                    }
                    else if (operations.Contains(functionlist.ElementAt(count)))
                    {
                        PNode node = new PNode();
                        node.Data = functionlist.ElementAt(count);
                        node.Right = treestack.Pop();
                        node.Left = treestack.Pop();
                        treestack.Push(node);
                    }
                    else if (functions.Contains(functionlist.ElementAt(count)))
                    {
                        PNode node = new PNode();
                        node.Data = functionlist.ElementAt(count);
                        node.Left = treestack.Pop();
                        treestack.Push(node);
                    }
                    count++;
                }
                return treestack.Pop();
            }
        }

        LinkedList<string> functionlist;                //список математических функций, операндов и операций из исходного выражения
        string variable, result;                        //переменная интегрирования, вычисленный результат
        Tree functiontree;                              //дерево

        public MathModel(LinkedList<string> functionlist, string variable, string a, string b, string e)
        {
            this.functionlist = functionlist;
            this.variable = variable;
            InverseFunctionList();
            functiontree = new Tree(polizfunctionlist, variable);
            result = CalculateIntegral(a, b, e);
        }

        public Tree GetTree { get { return functiontree; } }                   //получение дерева
        public string GetResult { get { return result; } }                     //получение ответа 
        public LinkedList<double> GetHs { get { return hs; } }                 //получение точек для графика
        public LinkedList<double> GetEps { get { return eps; } }

        LinkedList<string> polizfunctionlist = new LinkedList<string>();       //преобразованный в постфиксную запись список
        Stack<string> opStack = new Stack<string>();                           //стек для преобразования выражения
        List<string> operations = new List<string>() { "+", "-", "*", "/", "^" };  
        List<string> functions = new List<string>() { "sin", "cos", "tg", "ctg", "arcsin", "arccos", "arctg", "arcctg", "log", "ln", "sqrt" };

        public void InverseFunctionList()             //преобразование исходного списка в постфиксный
        {
            int count = 0;

            while (count < functionlist.Count)
            {
                double num = 0;
                if (Double.TryParse(functionlist.ElementAt(count), out num) || functionlist.ElementAt(count) == "pi" || functionlist.ElementAt(count) == "e")
                {
                    polizfunctionlist.AddLast(functionlist.ElementAt(count));
                }
                else if (functionlist.ElementAt(count) == variable)
                {
                    polizfunctionlist.AddLast(functionlist.ElementAt(count));
                }
                else if (functions.Contains(functionlist.ElementAt(count)))
                {
                    opStack.Push(functionlist.ElementAt(count));
                }
                else if (operations.Contains(functionlist.ElementAt(count)))
                {
                    if (opStack.Count != 0)
                    {
                        while(opStack.Count > 0 && operations.IndexOf(opStack.Peek()) >= operations.IndexOf(functionlist.ElementAt(count)))
                        {
                            polizfunctionlist.AddLast(opStack.Pop());
                        }
                    }
                    opStack.Push(functionlist.ElementAt(count));
                }
                else if (functionlist.ElementAt(count) == "(")
                {
                    opStack.Push(functionlist.ElementAt(count));
                }
                else if (functionlist.ElementAt(count) == ")")
                {
                    if (opStack.Count != 0)
                    {
                        while (opStack.Count > 0 && opStack.Peek() != "(")
                            polizfunctionlist.AddLast(opStack.Pop());
                        opStack.Pop();
                        if (opStack.Count > 0 && functions.Contains(opStack.Peek()))
                            polizfunctionlist.AddLast(opStack.Pop());
                    }
                }
                count++;
            }
            while (opStack.Count != 0)
                polizfunctionlist.AddLast(opStack.Pop());
        }

        Stack<double> calcstack = new Stack<double>();      //стек для вычисления выражения

        public double CalculateFunction(double x)           //вычисление значения функции в заданной точке
        {
            int count = 0;
            while (count < polizfunctionlist.Count)
            {
                double num = 0;
                if (Double.TryParse(polizfunctionlist.ElementAt(count), out num))
                    calcstack.Push(num);
                else if (polizfunctionlist.ElementAt(count) == "pi")
                    calcstack.Push(Math.PI);
                else if (polizfunctionlist.ElementAt(count) == "e")
                    calcstack.Push(Math.E);
                else if (polizfunctionlist.ElementAt(count) == variable)
                    calcstack.Push(x);
                else if (operations.Contains(polizfunctionlist.ElementAt(count)))
                {
                    double op1 = calcstack.Pop();
                    double op2 = calcstack.Pop();
                    if (polizfunctionlist.ElementAt(count) == "+") calcstack.Push(op1 + op2);
                    else if (polizfunctionlist.ElementAt(count) == "-") calcstack.Push(op2 - op1);
                    else if (polizfunctionlist.ElementAt(count) == "*") calcstack.Push(op1 * op2);
                    else if (polizfunctionlist.ElementAt(count) == "/") calcstack.Push(op2 / op1);
                    else if (polizfunctionlist.ElementAt(count) == "^") calcstack.Push(Math.Pow(op2, op1));
                }
                else if (functions.Contains(polizfunctionlist.ElementAt(count)))
                {
                    double arg = calcstack.Pop();
                    if (polizfunctionlist.ElementAt(count) == "sin") calcstack.Push(Math.Sin(arg));
                    else if (polizfunctionlist.ElementAt(count) == "cos") calcstack.Push(Math.Cos(arg));
                    else if (polizfunctionlist.ElementAt(count) == "tg") calcstack.Push(Math.Tan(arg));
                    else if (polizfunctionlist.ElementAt(count) == "ctg") calcstack.Push(1 / Math.Tan(arg));
                    else if (polizfunctionlist.ElementAt(count) == "arcsin") calcstack.Push(Math.Asin(arg));
                    else if (polizfunctionlist.ElementAt(count) == "arccos") calcstack.Push(Math.Acos(arg));
                    else if (polizfunctionlist.ElementAt(count) == "arctg") calcstack.Push(Math.Atan(arg));
                    else if (polizfunctionlist.ElementAt(count) == "arcctg") calcstack.Push(1 / Math.Atan(arg));
                    else if (polizfunctionlist.ElementAt(count) == "log") calcstack.Push(Math.Log2(arg));
                    else if (polizfunctionlist.ElementAt(count) == "ln") calcstack.Push(Math.Log(arg));
                    else if (polizfunctionlist.ElementAt(count) == "sqrt") calcstack.Push(Math.Sqrt(arg));
                }
                count++;
            }
            return calcstack.Pop();
        }

        LinkedList<double> hs = new LinkedList<double>();             //списки для точек графика
        LinkedList<double> eps = new LinkedList<double>();

        public string CalculateIntegral(string astr, string bstr, string estr)    //вычисление интеграла методом парабол Симпсона
        {
            double a = 0, b = 0;
            double e = 0;
            if (Double.TryParse(astr, out a) && Double.TryParse(bstr, out b) && Double.TryParse(estr, out e))
            {
                int count = 0;
                if(estr.Length > 2) count = estr.Length - 2;
                double delta = e + 1;
                double odelta = delta;
                int n = 2;
                double fh = 0;
                bool err = false;
                while (delta >= e && n < 101)
                {
                    fh = 0;
                    double fx = 0;
                    double xi = 0, x2i = 0;
                    double f2h = 0;
                    double h = (b - a) / n;
                    double h2 = (b - a) / (2*n);
                    fx = CalculateFunction(a);
                    if (Double.IsNormal(fx) || fx == 0)
                    {
                        fh += fx;
                        f2h += fx;
                    }
                    else err = true;
                    for (int i = 1; i < n; i++)
                    {
                        xi = a + h * i;
                        if (i % 2 == 0)
                        {
                            fx = 2 * CalculateFunction(xi);
                            if (Double.IsNormal(fx) || fx == 0) fh += fx;
                            else err = true;
                        }
                        else
                        {
                            fx = 4 * CalculateFunction(xi);
                            if (Double.IsNormal(fx) || fx == 0) fh += fx;
                            else err = true;
                        }
                    }
                    for (int i = 1; i < 2*n; i++)
                    {
                        x2i = a + h2 * i;
                        if (i % 2 == 0)
                        {
                            fx = 2 * CalculateFunction(x2i);
                            if (Double.IsNormal(fx) || fx == 0) f2h += fx;
                            else err = true;
                        }
                        else
                        {
                            fx = 4 * CalculateFunction(x2i);
                            if (Double.IsNormal(fx) || fx == 0) f2h += fx;
                            else err = true;
                        }
                    }
                    fx = CalculateFunction(a + h * n);
                    if (Double.IsNormal(fx) || fx == 0)
                    {
                        fh += fx;
                        f2h += fx;
                    }
                    else err = true;
                    fh = (fh * h) / 3;
                    f2h = (f2h * h2) / 3;
                    delta = Math.Abs(fh - f2h) / 15;
                    if (odelta < delta && !err)
                    {
                        delta = odelta;   
                    }
                    //odelta = delta;
                    hs.AddLast(h);
                    eps.AddLast(delta);
                    n += 2;
                }
                return Math.Round(fh, count).ToString();
            }
            else return "Ошибка в заданных параметрах!";
        }
    }
}
