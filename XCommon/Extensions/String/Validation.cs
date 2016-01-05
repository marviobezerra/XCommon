using XCommon.Util;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XCommon.Extensions.String
{
    public static class Validation
    {
        #region Valida Email
        public static bool EmailValido(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            Regex regex = new Regex(LibraryRegex.Email);

            return regex.IsMatch(email);
        }
        #endregion

        #region Valida Telefone
        public static bool TelefoneValido(this string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
                return false;

            Regex regex = new Regex(LibraryRegex.Phone);

            return regex.IsMatch(telefone);
        }
        #endregion

        #region Valida URL
        public static bool UrlValida(this string url)
        {
            Regex regex = new Regex(LibraryRegex.URL);

            return regex.IsMatch(url);
        }
        #endregion

        #region Valida CNPJ
        public static bool CPNJValido(this string cnpj)
        {
            cnpj = cnpj.Remove(".", "-", "/");
            List<string> _cnpj = new List<string>();

            _cnpj.Add("00000000000000");
            _cnpj.Add("11111111111111");
            _cnpj.Add("22222222222222");
            _cnpj.Add("33333333333333");
            _cnpj.Add("44444444444444");
            _cnpj.Add("55555555555555");
            _cnpj.Add("66666666666666");
            _cnpj.Add("77777777777777");
            _cnpj.Add("88888888888888");
            _cnpj.Add("99999999999999");

            if (cnpj.Length != 14)
                return false;

            if (_cnpj.Contains(cnpj))
                return true;

            int[] Multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int Soma = 0;
            int RestoDivisao = 0;
            string Digito = "";
            string NovoCNPJ = "";

            NovoCNPJ = cnpj.Substring(0, 12);


            for (int i = 0; i < 12; i++)
                Soma += int.Parse(NovoCNPJ[i].ToString()) * Multiplicador1[i];

            RestoDivisao = (Soma % 11);
            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            Digito = RestoDivisao.ToString();
            NovoCNPJ = NovoCNPJ + Digito;
            Soma = 0;

            for (int pos = 0; pos < 13; pos++)
                Soma = Soma + Convert.ToInt32(NovoCNPJ[pos].ToString()) * Multiplicador2[pos];
            RestoDivisao = (Soma % 11);

            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            NovoCNPJ += RestoDivisao;

            return NovoCNPJ == cnpj;
        }
        #endregion

        #region Valida CPF
        public static bool CPFValido(this string cpf)
        {
            const string _cpf = "000.000.000-00";
            cpf = cpf.Remove(".", "-", "/");

            if (cpf.Length != 11)
                return false;

            if (cpf == _cpf)
                return true;

            int[] Multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string NovoCPF = cpf.Substring(0, 9);
            string Digito = "";
            int Soma = 0;
            int RestoDivisao = 0;

            for (int i = 0; i < 9; i++)
                Soma += int.Parse(NovoCPF[i].ToString()) * Multiplicador1[i];

            RestoDivisao = Soma % 11;
            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            Digito = RestoDivisao.ToString();

            NovoCPF = NovoCPF + Digito;

            Soma = 0;
            for (int i = 0; i < 10; i++)
                Soma += int.Parse(NovoCPF[i].ToString()) * Multiplicador2[i];

            RestoDivisao = Soma % 11;
            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            Digito = Digito + RestoDivisao;

            return cpf.EndsWith(Digito);
        }
        #endregion

        #region Codigo de barras
        public static bool CodigoBarraValido(this string codigoBarra)
        {
            const string checkSum = "131313131313";
            bool valido = (codigoBarra.Length == 13);

            if (valido)
            {
                int digito = int.Parse(codigoBarra[codigoBarra.Length - 1].ToString());
                string ean = codigoBarra.Substring(0, codigoBarra.Length - 1);

                int sum = 0;
                for (int i = 0; i <= ean.Length - 1; i++)
                {
                    sum += int.Parse(ean[i].ToString()) * int.Parse(checkSum[i].ToString());
                }

                int calculo = 10 - (sum % 10);

                valido = (digito == calculo);
            }
            return valido;
        }
        #endregion
    }
}
