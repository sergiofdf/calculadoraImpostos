using System;

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
            Ano = anoNota != 0 ? anoNota : DateTime.Now.Year;
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
            else if (ValorBruto12Meses > 180000 && ValorBruto12Meses <= 360000)
            {
                decimal aliq = 0.112m;
                decimal valorDeduzir = 9360m;
                decimal irpj = 0.04m;
                decimal csll = 0.035m;
                decimal cofins = 0.1405m;
                decimal pis = 0.0305m;
                decimal inss = 0.434m;
                decimal iss = 0.320m;

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
            else if (ValorBruto12Meses > 360000 && ValorBruto12Meses <= 720000)
            {
                decimal aliq = 0.135m;
                decimal valorDeduzir = 17640m;
                decimal irpj = 0.04m;
                decimal csll = 0.035m;
                decimal cofins = 0.1364m;
                decimal pis = 0.0296m;
                decimal inss = 0.434m;
                decimal iss = 0.325m;

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
            else if (ValorBruto12Meses > 720000 && ValorBruto12Meses <= 1800000)
            {
                decimal aliq = 0.16m;
                decimal valorDeduzir = 35640m;
                decimal irpj = 0.04m;
                decimal csll = 0.035m;
                decimal cofins = 0.1364m;
                decimal pis = 0.0296m;
                decimal inss = 0.434m;
                decimal iss = 0.325m;

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
            else if (ValorBruto12Meses > 1800000 && ValorBruto12Meses <= 3600000)
            {
                decimal aliq = 0.21m;
                decimal valorDeduzir = 125640m;
                decimal irpj = 0.04m;
                decimal csll = 0.035m;
                decimal cofins = 0.1282m;
                decimal pis = 0.0278m;
                decimal inss = 0.434m;
                decimal iss = 0.335m;

                decimal valorBase = ValorBruto12Meses * aliq;

                decimal valorDeduzido = valorBase - valorDeduzir;
                decimal aliqEfetiva = valorDeduzido / ValorBruto12Meses;

                if (aliqEfetiva > 0.1492537m)
                {
                    irpj = 0.0602m;
                    csll = 0.0526m;
                    cofins = 0.1928m;
                    pis = 0.0418m;
                    inss = 0.6526m;
                    iss = 0.05m;
                }


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

            else if (ValorBruto12Meses > 3600000 && ValorBruto12Meses <= 4800000)
            {
                decimal aliq = 0.33m;
                decimal valorDeduzir = 648000m;
                decimal irpj = 0.35m;
                decimal csll = 0.15m;
                decimal cofins = 0.1603m;
                decimal pis = 0.0347m;
                decimal inss = 0.3050m;
                decimal iss = decimal.Zero;

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

            else
            {
                throw new Exception("Empresa não se enquadra como Microempresa ou Empresa de Pequeno Porte.");
            }

        }
    }


}
