/*
    Seja um arquivo com músicas em formato CSV (Comma Separated Values). 

    Implemente as funções abaixo:
    //     [x] Leia-o como uma coleção de músicas
    //     [x] Filtre a coleção por artista (por ex. Coldplay, Metallica, AC/DC)
    //     [x] Filtre a coleção por gênero (por ex. rock)
    //     [x] Filtre a coleção por duração (por ex. maiores que 5 minutos)
    //     [x] Ordene a coleção por artista
    //     [x] Ordene a coleção por artista e em seguida por músicas com duração crescente
    //     [x] Crie uma coleção de artistas e suas músicas
    //     [x] Informe a duração média das músicas da coleção
    //     [x] Informe a duração total das músicas da coleção
    //     [x] Informe qual artista tem mais músicas na coleção
 
*/

using var arquivo = new FileStream("musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);
OperacoesDeVerificacoesDeExistencia(stream);
void OperacoesDeVerificacoesDeExistencia(StreamReader stream)
{
    var musicas = ObterMusicas(stream).ToList();

    var artistas = musicas
        .GroupBy(m => m.Artista)
        .Where(g => g.Any(m => m.Duracao >= 540));
    Console.WriteLine($"\nArtistas com musicas acima de 9 minutos");
    foreach (var artista in artistas)
    {
        Console.WriteLine($"\t - {artista.Key}");
    }
    
    var reggae = musicas
        .GroupBy(m => m.Artista)
        .Where(g => g.Any(m => m.Generos.Contains("Reggae")));
    Console.WriteLine($"\nArtistas com musicas de reggae");
    foreach (var artista in reggae)
    {
        Console.WriteLine($"\t - {artista.Key}");
    }
}

void ArtistaComMaiorQtdeMusicas(StreamReader stream)
{
    var artistaComMaiorQtdeMusicas = ObterMusicas(stream)
    .GroupBy(a => a.Artista)
    .Select(g => new { Artista = g.Key, Musica = g, Total = g.Count() })
    .MaxBy(a => a.Total);
    if (artistaComMaiorQtdeMusicas is not null)
        Console.WriteLine($"O artista com maior quantidade de musicas e {artistaComMaiorQtdeMusicas.Artista} com {artistaComMaiorQtdeMusicas.Total} musicas!");

}
void OperacoesDeObtencaoDeElementos(StreamReader stream)
{
    var musicas = ObterMusicas(stream).ToList();

    var primeiraMusica = musicas.FirstOrDefault();
    Console.WriteLine($"A primeira musica e {primeiraMusica.Titulo}");

    var mairDuracao = musicas.MaxBy(m => m.Duracao);
    if(mairDuracao is not null )
        Console.WriteLine($"A musica com maior duracao e {mairDuracao.Titulo} com {mairDuracao.Duracao} segundos");

}
void OperacoesDeAgrupamento(StreamReader stream)
{
    var artistas = ObterMusicas(stream)
    .GroupBy(a => a.Artista);

    Console.WriteLine($"\nExibindo as musicas de cada artista:");
    foreach (var artista in artistas.Take(5))
    {
        Console.WriteLine($"Artista: {artista.Key} com {artista.Count()} musicas");
        foreach (var musica in artista)
        {
            Console.WriteLine($"\t - {musica.Titulo}");
        }
    }
}

void EstatisticasDeMusicas(StreamReader stream)
{
    var musicas = ObterMusicas(stream).ToList();

    Console.WriteLine($"\nExistem {musicas.Count()} musicas na colecao.");
    Console.WriteLine($"\nExistem {musicas.Count(m => m.Duracao >= 600)} musicas com mais de 10 minutos na colecao");
    Console.WriteLine($"\nA musica com menor duracao da colecao leva {musicas.Min(m => m.Duracao)} segundos");
    Console.WriteLine($"\nA musica com maior duracao da colecao leva {musicas.Max(m => m.Duracao)} segundos");
    Console.WriteLine($"\nA duracao media das musicas da colecao e {musicas.Average(m => m.Duracao)} segundos");
    Console.WriteLine($"\nVoce vai levar {musicas.Sum(m => m.Duracao)/(3600*24)} dias para ouvir toda a colecao");
}

void OperacoesDeProjecao2(StreamReader stream)
{
    var generos = ObterMusicas(stream)
        .SelectMany(m => m.Generos)
        .Distinct()
        .OrderBy(g => g);

    foreach (var genero in generos)
    {
        Console.WriteLine(genero);
    }
}
void OperacoesDeProjecao(StreamReader stream)
{
    var artistas = ObterMusicas(stream)
    .Select(m => m.Artista) // projecao/transformacao
    .Distinct()             // filtragem
    .OrderBy(a => a);

    foreach (var artista in artistas)
    {
        Console.WriteLine(artista);
    }
}
void OperecaoDeFiltroEOrdenacao(StreamReader stream)
{
    var musicasDoColdplay =
    ObterMusicas(stream)
    .Where(musica => musica.Artista == "Coldplay")
    .OrderBy(musica => musica.Titulo)
    .Skip(5);

    ExibirMusicas(musicasDoColdplay);
}

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    Console.WriteLine($"\nExibindo as musicas:");
    var contador = 1;
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} seg");
        contador++;
        if (contador > 10) break;
    }
}
IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine(); // Pula o cabeçalho
    while (linha is not null)
    {
        var partes = linha.Split(';');
        var musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2]),
            Generos = partes[3].Split(",").Select(g => g.Trim())
        };
        yield return musica;
        linha = stream.ReadLine();
    }
}

class Musica
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos
    public IEnumerable<string> Generos { get; set; }
}