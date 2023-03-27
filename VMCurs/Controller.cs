using System;
using System.Collections.Generic;
using System.Text;

namespace VMCurs
{
    class Controller
    {
        private AnalyzerModel amodel;      //синтаксический анализатор
        private MathModel mmodel;          //математика программы
        private bool devMode;              //переключатель режима разработчика
        private string a, b, e;            //границы интегрирования и точность

        public Controller(string chain, string a, string b, string e, bool devmode)
        {

            this.a = a;
            this.b = b;
            this.e = e;
            amodel = new AnalyzerModel(chain);
            devMode = devmode;
        }

        public int GetErrorPosition { get; set; }                      //получение позиции ошибки от синтаксического анализатора
        public string GetErrorMessage { get; set; }                    //получения сообщения ошибки от синтаксического анализатора
        public LinkedList<string> GetFunctionList { get; set; }        //получение списка математических функций
        public string GetResult { get; set; }                          //получение ответа после интегрирования
        public LinkedList<double> GetHs { get; set; }                  //получение точек
        public LinkedList<double> GetEps { get; set; }                 //для графика
        public MathModel.Tree tree;                                    //объект дерева

        public string Analyze()                                        //анализ строки и вычисление интеграла
        {
            if (amodel.AnalyzeChain())
            {
                GetFunctionList = amodel.GetFunctionList;
                mmodel = new MathModel(GetFunctionList, amodel.GetVariable, a, b, e);
                tree = mmodel.GetTree;
                GetResult = mmodel.GetResult;
                GetHs = mmodel.GetHs;
                GetEps = mmodel.GetEps;
                if (devMode == true)
                {
                    devForm dform = new devForm(GetFunctionList, tree.Head);
                    dform.ShowDialog();
                }
                return "Ошибок не обнаружено";
            }
            else
            {
                GetErrorMessage = amodel.GetErrorMessage;
                GetErrorPosition = amodel.GetErrorPosition;
                return "Ошибка: " + GetErrorMessage + ", позиция: " + GetErrorPosition;
            }
        }
    }
}
