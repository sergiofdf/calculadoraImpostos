namespace CalculadoraImpostos
{
    public class Empresa
    {
        public string CNPJ { get; }
        public string Nome { get; }
        public decimal Faturamento12Meses
        {
            get
            {
                decimal total = 0;
                foreach (var nf in listaNotasFiscais)
                {
                    if ((nf.Mes < DateTime.Now.Month && nf.Ano == DateTime.Now.Year)
                        || (nf.Mes >= DateTime.Now.Month && nf.Ano == DateTime.Now.Year - 1))
                    {
                        total += nf.ValorNota;
                    }
                }
                return total;
            }
        }

        public List<NotaFiscal> listaNotasFiscais = new List<NotaFiscal>();

        public Empresa(string cnpj, string nome)
        {
            CNPJ = cnpj;
            Nome = nome;
        }


        public decimal[] EmitirNota(decimal valorNota, string cnpjCliente, string nomeCliente, int mesNota, int anoNota)
        {
            var novaNota = new NotaFiscal(valorNota, cnpjCliente, nomeCliente, Faturamento12Meses, mesNota, anoNota);
            listaNotasFiscais.Add(novaNota);

            return novaNota.Impostos;
        }

        public void CadastrarNotasAnteriores(decimal valorNota, string cnpjCliente, string nomeCliente, int mesNota, int anoNota)
        {
            if ((mesNota > DateTime.Now.Month && anoNota >= DateTime.Now.Year) || anoNota > DateTime.Now.Year)
            {
                Console.WriteLine("Não é possível cadastrar notas com data futura.");
                return;
            }
            var novaNota = new NotaFiscal(valorNota, cnpjCliente, nomeCliente, Faturamento12Meses, mesNota, anoNota);
            listaNotasFiscais.Add(novaNota);
        }

        public List<string[]> ConsultarNotasPorCliente(string cnpjClienteFormatado)
        {
            List<string[]> notasPorCliente = new List<string[]>();
            foreach (var nota in listaNotasFiscais)
            {
                if (nota.CnpjCliente == cnpjClienteFormatado)
                {
                    int mesNota = nota.Mes;
                    int anoNota = nota.Ano;
                    string mesEmissao = (mesNota + "/" + anoNota);
                    decimal valorNota = nota.ValorNota;
                    string valorEmissao = ($"R$ {valorNota.ToString("N2")}");
                    string[] dadosNota = { mesEmissao, valorEmissao };
                    notasPorCliente.Add(dadosNota);

                }
            }
            return notasPorCliente;
        }

        public List<string[]> ConsultarNotasPorMes(int mesDigitado, int anoDigitado)
        {
            List<string[]> notasPorMes = new List<string[]>();
            foreach (var nota in listaNotasFiscais)
            {
                if (nota.Mes == mesDigitado && nota.Ano == anoDigitado)
                {
                    string clienteNota = nota.NomeCliente;
                    int mesNota = nota.Mes;
                    int anoNota = nota.Ano;
                    string mesEmissao = (mesNota + "/" + anoNota);
                    decimal valorNota = nota.ValorNota;
                    string valorEmissao = ($"R$ {valorNota.ToString("N2")}");
                    string[] dadosNota = { clienteNota, mesEmissao, valorEmissao };
                    notasPorMes.Add(dadosNota);

                }
            }
            return notasPorMes;
        }
    }
}
