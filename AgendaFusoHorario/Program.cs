using System.Globalization;

namespace AgendaFusoHorario
{
    // Classe que representa um compromisso da agenda
    class Compromisso
    {
        public string Descricao { get; set; }
        public DateTimeOffset DataHoraOriginal { get; set; }
        public string TimeZoneOrigem { get; set; }
    }

    class Program
    {
        // Lista principal onde todos os compromissos serão armazenados
        static List<Compromisso> agenda = new List<Compromisso>();

        // Função principal do sistema
        // Exibe o menu e controla o fluxo da aplicação
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("=== AGENDA COM CONVERSOR DE FUSO HORÁRIO ===");
                Console.WriteLine("1 - Adicionar compromisso");
                Console.WriteLine("2 - Ver compromissos de hoje");
                Console.WriteLine("3 - Ver compromissos por data");
                Console.WriteLine("4 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarCompromisso();
                        break;
                    case "2":
                        ExibirCompromissosHoje();
                        break;
                    case "3":
                        ExibirCompromissosPorData();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Pausar();
                        break;
                }
            }
        }

        // Função responsável por cadastrar um novo compromisso
        // Lê descrição, data/hora e timezone escolhido pelo usuário
        static void AdicionarCompromisso()
        {
            Console.Clear();
            Console.WriteLine("=== NOVO COMPROMISSO ===");

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Data e hora (dd/MM/yyyy HH:mm): ");
            string entradaData = Console.ReadLine();

            Console.WriteLine("Fusos disponíveis:");
            Console.WriteLine("1 - Brasil (São Paulo)");
            Console.WriteLine("2 - UTC");
            Console.WriteLine("3 - Nova York");
            Console.Write("Escolha o fuso: ");

            string escolhaFuso = Console.ReadLine();

            // Define o timezone com base na escolha do usuário
            string timezone = escolhaFuso switch
            {
                "1" => "E. South America Standard Time",
                "2" => "UTC",
                "3" => "Eastern Standard Time",
                _ => "E. South America Standard Time"
            };

            // Converte a data digitada para DateTime
            DateTime dataHora = DateTime.ParseExact(
                entradaData,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture
            );

            // Obtém informações do fuso horário escolhido
            TimeZoneInfo tzOrigem = TimeZoneInfo.FindSystemTimeZoneById(timezone);

            // Cria a data com o offset correto
            DateTimeOffset dataComOffset = new DateTimeOffset(
                dataHora,
                tzOrigem.GetUtcOffset(dataHora)
            );

            // Adiciona o compromisso à lista
            agenda.Add(new Compromisso
            {
                Descricao = descricao,
                DataHoraOriginal = dataComOffset,
                TimeZoneOrigem = timezone
            });

            Console.WriteLine("Compromisso adicionado com sucesso!");
            Pausar();
        }

        // Função que exibe todos os compromissos do dia atual
        // Considera o horário brasileiro como referência
        static void ExibirCompromissosHoje()
        {
            Console.Clear();
            Console.WriteLine("=== COMPROMISSOS DE HOJE (HORÁRIO BRASILEIRO) ===");

            // Define timezone do Brasil
            TimeZoneInfo tzBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // Obtém a data de hoje no Brasil
            DateTime hojeBrasil = TimeZoneInfo.ConvertTime(DateTime.Now, tzBrasil).Date;

            // Filtra compromissos da data atual
            var compromissosHoje = agenda.Where(c =>
                TimeZoneInfo.ConvertTime(c.DataHoraOriginal, tzBrasil).Date == hojeBrasil
            );

            MostrarCompromissos(compromissosHoje, tzBrasil);
        }

        // Função que busca compromissos de uma data específica
        // A data informada é comparada com os compromissos convertidos para o horário brasileiro
        static void ExibirCompromissosPorData()
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR COMPROMISSOS POR DATA ===");

            Console.Write("Digite a data (dd/MM/yyyy): ");
            string entrada = Console.ReadLine();

            // Converte a entrada para DateTime
            DateTime dataBusca = DateTime.ParseExact(
                entrada,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture
            );

            // Define timezone brasileiro
            TimeZoneInfo tzBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // Filtra compromissos da data informada
            var compromissosData = agenda.Where(c =>
                TimeZoneInfo.ConvertTime(c.DataHoraOriginal, tzBrasil).Date == dataBusca.Date
            );

            MostrarCompromissos(compromissosData, tzBrasil);
        }

        // Função responsável por exibir na tela a lista de compromissos encontrados
        // Faz a conversão para o timezone desejado antes de mostrar
        static void MostrarCompromissos(IEnumerable<Compromisso> compromissos, TimeZoneInfo tzDestino)
        {
            if (!compromissos.Any())
            {
                Console.WriteLine("Nenhum compromisso encontrado.");
            }
            else
            {
                foreach (var c in compromissos)
                {
                    DateTimeOffset convertido = TimeZoneInfo.ConvertTime(c.DataHoraOriginal, tzDestino);

                    Console.WriteLine("----------------------------------");
                    Console.WriteLine($"Descrição: {c.Descricao}");
                    Console.WriteLine($"Data/Hora Brasil: {convertido:dd/MM/yyyy HH:mm}");
                }
            }

            Pausar();
        }

        // Função utilitária que pausa a execução
        // Aguarda o usuário pressionar uma tecla antes de continuar
        static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}