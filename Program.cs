using System;
using System.Collections.Generic;

namespace CalculadoraImpostos
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Empresa> todasEmpresas = new List<Empresa>();

            string menuInicial()
            {

                bool cnpjValido = false;
                string CNPJ;


                do
                {
                    Console.WriteLine("Digite seu CNPJ");
                    CNPJ = Console.ReadLine();
                    cnpjValido = TrataCNPJ.ValidaCNPJ(CNPJ);
                    if (!cnpjValido)
                        Console.WriteLine("CNPJ Inválido. Digite novamente:");
                } while (!cnpjValido);

                string cnpjFormatado = TrataCNPJ.RemoveCaracteres(CNPJ);
                criaNovoCadastro(cnpjFormatado);
                menuOpcoesNotas(cnpjFormatado);
                return cnpjFormatado;
            }


            void criaNovoCadastro(string cnpjFormatado)
            {
                string NomeEmpresa = "";
                bool nomeValido = false;
                bool cadastroExistente = verificaCadastroExistente(cnpjFormatado);
                if (!cadastroExistente)
                {
                    Console.WriteLine("Digite o nome da Empresa");
                    while (!nomeValido)
                    {
                        NomeEmpresa = Console.ReadLine();
                        nomeValido = NomeEmpresa.Length >= 10;
                        if (!nomeValido)
                        {
                            Console.WriteLine("Nome da empresa deve ter pelo menos 10 caracteres.");
                        }
                    }

                    var novaEmpresa = new Empresa(cnpjFormatado, NomeEmpresa);

                    todasEmpresas.Add(novaEmpresa);

                }

            }

            bool verificaCadastroExistente(string cnpjFormatado)
            {
                foreach (var empresa in todasEmpresas)
                {
                    if (empresa.CNPJ == cnpjFormatado)
                    {
                        return true;
                    }
                }
                return false;
            }

            void menuOpcoesNotas(string cnpj)
            {
                bool numeroValido = false;
                int userInputNumber = 0;
                string nomeEmpresa = null;

                foreach (var empresa in todasEmpresas)
                {
                    if (empresa.CNPJ == cnpj)
                    {
                        nomeEmpresa = empresa.Nome;
                    }
                }
                if (nomeEmpresa == null)
                {
                    throw new Exception("Empresa não encontrada.");
                }

                Console.WriteLine($"Escolha ação para a empresa {nomeEmpresa}, CNPJ {cnpj}.\n");

                do
                {
                    Console.WriteLine("1 - Emitir nova nota fiscal");
                    Console.WriteLine("2 - Cadastrar notas anteriores");
                    Console.WriteLine("3 - Consultar notas anteriores");
                    Console.WriteLine("4 - Sair");
                    Console.WriteLine("5 - Encerrar");
                    string opcaoUsuario = Console.ReadLine();
                    if (opcaoUsuario.ToLower().Trim() == "voltar" || opcaoUsuario.ToLower().Trim() == "cancelar")
                    {
                        menuInicial();
                    }
                    numeroValido = (int.TryParse(opcaoUsuario, out userInputNumber) && userInputNumber > 0 && userInputNumber <= 5);
                } while (!numeroValido);
                switch (userInputNumber)
                {
                    case 1:
                        Console.Clear();
                        EmitirOuCadastrarNota(cnpj);
                        Console.WriteLine("\n\n");
                        trataFimPrograma(cnpj);
                        break;
                    case 2:
                    //cadastrar notas anteriores
                    MesCadastroNota:
                        Console.Clear();

                        int mesDigitadoNumber = 0;
                        bool mesValido = false;
                        int anoDigitadoNumber = 0;
                        bool anoValido = false;
                        while (!mesValido)
                        {
                            Console.WriteLine("Digite o mês da emissao");
                            string mesDigitado = Console.ReadLine();
                            if (mesDigitado.ToLower().Trim() == "cancelar" || mesDigitado.ToLower().Trim() == "voltar")
                            {
                                menuOpcoesNotas(cnpj);
                            }
                            mesValido = (int.TryParse(mesDigitado, out mesDigitadoNumber) && mesDigitadoNumber > 0 && mesDigitadoNumber <= 12);
                            if (!mesValido)
                            {
                                Console.Clear();
                                Console.WriteLine("Mês digitado inválido");
                            }
                        }
                        while (!anoValido)
                        {
                            Console.WriteLine("Digite o ano da emissao");
                            string anoDigitado = Console.ReadLine();
                            if (anoDigitado.ToLower().Trim() == "cancelar")
                            {
                                menuOpcoesNotas(cnpj);
                            }
                            if (anoDigitado.ToLower().Trim() == "voltar")
                            {
                                goto MesCadastroNota;
                            }
                            anoValido = (int.TryParse(anoDigitado, out anoDigitadoNumber) && anoDigitadoNumber > 1999 && anoDigitadoNumber <= DateTime.Now.Year);
                            if (!anoValido)
                            {
                                Console.Clear();
                                Console.WriteLine("Ano digitado inválido");
                            }
                        }

                        EmitirOuCadastrarNota(cnpj, mesDigitadoNumber, anoDigitadoNumber);
                        trataFimPrograma(cnpj);
                        break;
                    case 3:
                        Console.Clear();
                        menuConsulta(cnpj);
                        //consulta notas anteriores
                        break;
                    case 4:
                        Console.Clear();
                        menuInicial();
                        //consulta notas anteriores
                        break;
                    case 5:
                        Environment.Exit(0);
                        //consulta notas anteriores
                        break;
                }
            }

            void EmitirOuCadastrarNota(string cnpjEmpresa, int mesNota = 0, int anoNota = 0)
            {
                Console.WriteLine("Digite o valor da nota fiscal:");

                decimal valorNota = 0;
                bool isValidNumber = false;
                while (!isValidNumber)
                {
                    string opcaoUsuario = Console.ReadLine();
                    if (opcaoUsuario.ToLower().Trim() == "voltar" || opcaoUsuario.ToLower().Trim() == "cancelar")
                    {
                        menuOpcoesNotas(cnpjEmpresa);
                    }
                    isValidNumber = decimal.TryParse(opcaoUsuario, out valorNota);
                    if (!isValidNumber)
                    {
                        Console.WriteLine("Digite somente números com casas decimais. Utilize ponto para casas decimais.");
                    }
                }

            EntrarCnpjCliente:
                Console.WriteLine("Digite o CNPJ do Cliente:");
                bool cnpjValido = false;
                string cnpjCliente = "";

                while (!cnpjValido)
                {
                    cnpjCliente = Console.ReadLine();
                    if (cnpjCliente.ToLower().Trim() == "cancelar")
                    {
                        menuOpcoesNotas(cnpjEmpresa);
                    }
                    if (cnpjCliente.ToLower().Trim() == "voltar")
                    {
                        EmitirOuCadastrarNota(cnpjEmpresa);
                    }
                    cnpjValido = TrataCNPJ.ValidaCNPJ(cnpjCliente);
                    if (!cnpjValido)
                    {
                        Console.WriteLine("CNPJ Inválido!\nDigite o CNPJ do Cliente:");
                    }
                }

                string cnpjClienteFormatado = TrataCNPJ.RemoveCaracteres(cnpjCliente);

            DigiteNomeCliente:
                Console.WriteLine("Digite o nome do Cliente:");
                string nomeCliente = Console.ReadLine();
                if (nomeCliente.ToLower().Trim() == "cancelar")
                {
                    menuOpcoesNotas(cnpjEmpresa);
                }
                if (nomeCliente.ToLower().Trim() == "voltar")
                {
                    goto EntrarCnpjCliente;
                }

                Console.WriteLine("Deseja emitir a nota? Sim/Nao/sair/encerrar");
                string resposta = (Console.ReadLine()).ToLower().Trim();

                if (resposta == "sim")
                {
                    Console.Clear();
                    foreach (var empresa in todasEmpresas)
                    {
                        if (empresa.CNPJ == cnpjEmpresa)
                        {
                            if (mesNota == 0)
                            {
                                decimal[] impostos = empresa.EmitirNota(valorNota, cnpjClienteFormatado, nomeCliente, mesNota, anoNota);
                                Console.WriteLine($"IRPJ: R${impostos[0].ToString("N2")}");
                                Console.WriteLine($"CSLL: R${impostos[1].ToString("N2")}");
                                Console.WriteLine($"COFINS: R${impostos[2].ToString("N2")}");
                                Console.WriteLine($"PIS: R${impostos[3].ToString("N2")}");
                                Console.WriteLine($"INSS: R${impostos[4].ToString("N2")}");
                                Console.WriteLine($"ISS: R${impostos[5].ToString("N2")}");
                                Console.WriteLine($"VALOR TOTAL: R${impostos[6].ToString("N2")}");
                            }
                            else
                            {
                                empresa.CadastrarNotasAnteriores(valorNota, cnpjClienteFormatado, nomeCliente, mesNota, anoNota);
                            }
                            Console.WriteLine("\n\n");
                            menuOpcoesNotas(cnpjEmpresa);
                        }
                    }
                }
                else if (resposta == "sair")
                {
                    Console.Clear();
                    menuInicial();
                }
                else if (resposta == "encerrar")
                {
                    Environment.Exit(0);
                }
                else if (resposta == "voltar")
                {
                    goto DigiteNomeCliente;
                }
                else
                {
                    menuOpcoesNotas(cnpjEmpresa);
                }

            }

            void menuConsulta(string cnpj)
            {
                int escolhaTipoConsulta = 0;
                bool escolhaValida = false;
                while (!escolhaValida)
                {
                    Console.WriteLine("1 - Consultar por Cliente");
                    Console.WriteLine("2 - Consultar por Mes");
                    string escolhaDigitada = Console.ReadLine();
                    if (escolhaDigitada.ToLower().Trim() == "voltar" || escolhaDigitada.ToLower().Trim() == "cancelar")
                    {
                        menuOpcoesNotas(cnpj);
                    }
                    escolhaValida = (int.TryParse(escolhaDigitada, out escolhaTipoConsulta) && escolhaTipoConsulta > 0 && escolhaTipoConsulta <= 2);
                    if (!escolhaValida)
                    {
                        Console.WriteLine("Digite a opção 1 ou 2.");
                    }
                }

                if (escolhaTipoConsulta == 1)
                {
                    bool cnpjValido = false;
                    string cnpjConsultaCliente = "";
                    while (!cnpjValido)
                    {
                        Console.WriteLine("Digite o CNPJ do Cliente");
                        cnpjConsultaCliente = Console.ReadLine();
                        if (cnpjConsultaCliente.ToLower().Trim() == "cancelar")
                        {
                            menuOpcoesNotas(cnpj);
                        }
                        if (cnpjConsultaCliente.ToLower().Trim() == "voltar")
                        {
                            menuConsulta(cnpj);
                        }
                        cnpjValido = TrataCNPJ.ValidaCNPJ(cnpjConsultaCliente);
                        if (!cnpjValido)
                            Console.WriteLine("CNPJ Inválido. Digite novamente:");
                    }
                    string cnpjClienteFormatado = TrataCNPJ.RemoveCaracteres(cnpjConsultaCliente);

                    List<string[]> notas = new List<string[]>();
                    foreach (var empresa in todasEmpresas)
                    {
                        if (empresa.CNPJ == cnpj)
                        {
                            List<string[]> notas2 = empresa.ConsultarNotasPorCliente(cnpjClienteFormatado);
                            notas.AddRange(notas2);
                        }
                    }
                    if (notas.Count > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Mês de Emissao\t\tValor");
                        foreach (var nota in notas)
                        {
                            Console.WriteLine($"{nota[0]}\t\t\t{nota[1]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cliente não encontrado");
                    }
                    Console.WriteLine("\n\n");
                    trataFimPrograma(cnpj);
                }
                if (escolhaTipoConsulta == 2)
                {
                //COnsultar por mês
                MesConsulta:
                    int mesDigitadoNumber = 0;
                    bool mesValido = false;
                    int anoDigitadoNumber = 0;
                    bool anoValido = false;
                    while (!mesValido)
                    {
                        Console.WriteLine("Digite o mês da pesquisa");
                        string mesDigitado = Console.ReadLine();
                        if (mesDigitado.ToLower().Trim() == "cancelar")
                        {
                            menuOpcoesNotas(cnpj);
                        }
                        if (mesDigitado.ToLower().Trim() == "voltar")
                        {
                            menuConsulta(cnpj);
                        }
                        mesValido = (int.TryParse(mesDigitado, out mesDigitadoNumber) && mesDigitadoNumber > 0 && mesDigitadoNumber <= 12);
                        if (!mesValido)
                        {
                            Console.Clear();
                            Console.WriteLine("Mês digitado inválido");
                        }
                    }
                    while (!anoValido)
                    {
                        Console.WriteLine("Digite o ano da pesquisa");
                        string anoDigitado = Console.ReadLine();
                        if (anoDigitado.ToLower().Trim() == "cancelar")
                        {
                            menuConsulta(cnpj);
                        }
                        if (anoDigitado.ToLower().Trim() == "voltar")
                        {
                            goto MesConsulta;
                        }
                        anoValido = (int.TryParse(anoDigitado, out anoDigitadoNumber) && anoDigitadoNumber > 1999 && anoDigitadoNumber <= DateTime.Now.Year);
                        if (!anoValido)
                        {
                            Console.Clear();
                            Console.WriteLine("Ano digitado inválido");
                        }
                    }

                    List<string[]> notas = new List<string[]>();
                    foreach (var empresa in todasEmpresas)
                    {
                        if (empresa.CNPJ == cnpj)
                        {
                            List<string[]> notas2 = empresa.ConsultarNotasPorMes(mesDigitadoNumber, anoDigitadoNumber);
                            notas.AddRange(notas2);
                        }
                    }
                    if (notas.Count > 0)
                    {
                        Console.WriteLine("Cliente\t\t\t\tMês de Emissao\t\tValor");
                        foreach (var nota in notas)
                        {
                            Console.WriteLine($"{nota[0]}\t\t\t{nota[1]}\t\t\t{nota[2]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não foram encontradas notas para o mês pesquisado.");
                    }

                    Console.WriteLine("\n\n");
                    trataFimPrograma(cnpj);
                }
            }

            void trataFimPrograma(string cnpjEmTratamento)
            {
                bool escolhaValida = false;
                int escolhaUsuario = 0;

                while (!escolhaValida)
                {
                    Console.WriteLine("\n\n");
                    Console.WriteLine("O que deseja fazer?");
                    Console.WriteLine("1 - Voltar ao menu inicial (digitar no CPNJ)");
                    Console.WriteLine("2 - Fazer nova busca ou cadastro de NF");
                    Console.WriteLine("3 - Encerrar o programa");
                    string escolhaDigitada = Console.ReadLine();
                    escolhaValida = (int.TryParse(escolhaDigitada, out escolhaUsuario) && escolhaUsuario > 0 && escolhaUsuario <= 3);
                    if (!escolhaValida)
                    {
                        Console.WriteLine("Digite uma opção entre 1 e 3.");
                    }


                }

                if (escolhaUsuario == 1) { menuInicial(); }
                if (escolhaUsuario == 2) { menuOpcoesNotas(cnpjEmTratamento); }
                if (escolhaUsuario == 3) { Environment.Exit(0); }
            }

            string cnpjEmTratamento = menuInicial();



        }
    }
}