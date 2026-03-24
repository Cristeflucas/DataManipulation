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

var musicas = ObterMusicas(stream);
ExibirMusicas(musicas);

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    Console.WriteLine($"\nExibindo as musicas:");
    var contador = 1;
    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo}");
        contador++;
        if (contador > 10) break;
    }
}

IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine(); // Pula o cabeçalho
    while(linha is not null)
    {
        var partes = linha.Split(';');
        var musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2])
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
}