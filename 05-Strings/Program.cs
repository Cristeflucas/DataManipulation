using System.Globalization;

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musica = ObterMusicas(stream)
    .Where(m => m.Titulo.StartsWith('T'))
    .OrderBy(m => m.Lancamento)
    .FirstOrDefault();
if (musica is not null)
{
    Console.WriteLine($"Titulo da Musica: {musica.Titulo}");
    musica.Titulo = musica.Titulo.Replace("The ", ""); // Strings são imutáveis, então isso não altera o titulo da musica
    Console.WriteLine($"Titulo da Musica: {musica.Titulo}");
}




//ExibirMusicas(musicasComT);




void ValidandoSenha()
{
    var senha = "Ml#1234567";

    var totalCaracteres = senha.Length;
    var totalLetraMaiuscula = senha.Count(c => char.IsUpper(c));
    var totalLetraMinuscula = senha.Count(c => char.IsLower(c));
    var totalNumeros = senha.Count(c => char.IsDigit(c));
    var totalSimbulos = senha.Count(c => !char.IsLetterOrDigit(c));

    if (totalCaracteres < 8 || totalLetraMaiuscula == 0 || totalLetraMinuscula == 0 || totalNumeros == 0 || totalSimbulos == 0)
    {
        Console.WriteLine("Senha Fraca!");
    }
    else
    {
        Console.WriteLine("Senha Forte!");
    }
    void ExibirMusicas(IEnumerable<Musica> musicas)
    {
        var titulo = "\nMusicas do arquivo";
        Console.WriteLine(titulo);
        foreach (var musica in musicas)
        {
            var linha = $"\t- {musica.Titulo} ({musica.Artista}) - {musica.Duracao}s [{musica.Lancamento}]";
            Console.WriteLine(linha);
        }
    }
}

IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine();
    while (linha is not null)
    {
        var partes = linha.Split(';');
        var musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2]),
            Generos = partes[3].Split(',', StringSplitOptions.TrimEntries),
            Lancamento = DateTime.ParseExact(partes[4], "dd/MM/yyyy", CultureInfo.InvariantCulture)
        };
        yield return musica;
        linha = stream.ReadLine();
    }
}

class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }
    public IEnumerable<string> Generos { get; set; }
    public DateTime Lancamento { get; set; }
}