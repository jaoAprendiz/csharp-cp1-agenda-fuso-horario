using System.Globalization;

namespace AgendaFusoHorario
{
    class Compromisso
    {
        public string Descricao { get; set; }
        public DateTimeOffset DataHoraOriginal { get; set; }
        public string TimeZoneOrigem { get; set; }
    }

    class Program
    {
        static List<Compromisso> agenda = new List<Compromisso>();

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

            string timezone = escolhaFuso switch
            {
                "1" => "E. South America Standard Time",
                "2" => "UTC",
                "3" => "Eastern Standard Time",
                _ => "E. South America Standard Time"
            };

            DateTime dataHora = DateTime.ParseExact(
                entradaData,
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture
            );

            TimeZoneInfo tzOrigem = TimeZoneInfo.FindSystemTimeZoneById(timezone);

            DateTimeOffset dataComOffset = new DateTimeOffset(
                dataHora,
                tzOrigem.GetUtcOffset(dataHora)
            );

            agenda.Add(new Compromisso
            {
                Descricao = descricao,
                DataHoraOriginal = dataComOffset,
                TimeZoneOrigem = timezone
            });

            Console.WriteLine("Compromisso adicionado com sucesso!");
            Pausar();
        }

        static void ExibirCompromissosHoje()
        {
            Console.Clear();
            Console.WriteLine("=== COMPROMISSOS DE HOJE (HORÁRIO BRASILEIRO) ===");

            TimeZoneInfo tzBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            DateTime hojeBrasil = TimeZoneInfo.ConvertTime(DateTime.Now, tzBrasil).Date;

            var compromissosHoje = agenda.Where(c =>
                TimeZoneInfo.ConvertTime(c.DataHoraOriginal, tzBrasil).Date == hojeBrasil
            );

            MostrarCompromissos(compromissosHoje, tzBrasil);
        }

        static void ExibirCompromissosPorData()
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR COMPROMISSOS POR DATA ===");

            Console.Write("Digite a data (dd/MM/yyyy): ");
            string entrada = Console.ReadLine();

            DateTime dataBusca = DateTime.ParseExact(
                entrada,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture
            );

            TimeZoneInfo tzBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var compromissosData = agenda.Where(c =>
                TimeZoneInfo.ConvertTime(c.DataHoraOriginal, tzBrasil).Date == dataBusca.Date
            );

            MostrarCompromissos(compromissosData, tzBrasil);
        }

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

        static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}