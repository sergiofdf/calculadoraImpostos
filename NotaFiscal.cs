using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraImpostos
{
    public class NotaFiscal
    {
        public decimal ValorNota { get; }
        public int Mes { get; }
        public int Ano { get; }
        public string CnpjCliente { get; }
        public string NomeCliente { get; }
        public decimal ValorBruto12Meses { get; } 
        public decimal[] Impostos { get; }
        

        public NotaFiscal(decimal valor, string cnpjCliente, string nomeCliente, decimal valor12meses, int mesNota, int anoNota)
        {
            ValorNota = valor;
            Mes = mesNota != 0 ? mesNota : DateTime.Now.Month;
            Ano = anoNota != 0 ? anoNota : DateTime.Now.Month;
            CnpjCliente = TrataCNPJ.RemoveCaracteres(cnpjCliente);
            NomeCliente = nomeCliente;
            ValorBruto12Meses = valor12meses;
            Impostos = calculaImpostosNota();
        }

        public decimal[] calculaImpostosNota()
        {
            if (ValorBruto12Meses <= 180000)
            {
                decimal aliq = 0.06m;
                decimal irpj = 0.04m;
                decimal csll = 0.035m;
                decimal cofins = 0.1282m;
                decimal pis = 0.0278m;
                decimal inss = 0.434m;
                decimal iss = 0.335m;

                decimal totalImpostoNota = aliq * ValorNota;
                decimal totalIrpj = irpj * totalImpostoNota;
                decimal totalCsll = csll * totalImpostoNota;
                decimal totalCofins = cofins * totalImpostoNota;
                decimal totalPis = pis * totalImpostoNota;
                decimal totalInss = inss * totalImpostoNota;
                decimal totalIss = iss * totalImpostoNota;

                decimal[] impostos = { totalIrpj, totalCsll, totalCofins, totalPis, totalInss, totalIss, totalImpostoNota };

                return impostos;

            }
            else
            {
                decimal aliq = 0.112m;
                decimal valorDeduzir = 9360m;
                decimal irpj = 0.04m;
                decimal csll = 0.035m;
                decimal cofins = 0.1405m;
                decimal pis = 0.0305m;
                decimal inss = 0.434m;
                decimal iss = 0.335m;

                decimal valorBase = ValorBruto12Meses * aliq;

                decimal valorDeduzido = valorBase - valorDeduzir;
                decimal aliqEfetiva = valorDeduzido / ValorBruto12Meses;
                decimal totalImpostoNota = aliqEfetiva * ValorNota;
                decimal totalIrpj = irpj * totalImpostoNota;
                decimal totalCsll = csll * totalImpostoNota;
                decimal totalCofins = cofins * totalImpostoNota;
                decimal totalPis = pis * totalImpostoNota;
                decimal totalInss = inss * totalImpostoNota;
                decimal totalIss = iss * totalImpostoNota;

                decimal[] impostos = { totalIrpj, totalCsll, totalCofins, totalPis, totalInss, totalIss, totalImpostoNota };

                return impostos;

            }
        
        }
    }

   
}
