using System.Collections;

var musica1 = new Musica("Tempo Perdido", "Legião Urbana", 180);
var musica2 = new Musica("Pais e Filhos", "Legião Urbana", 210);
var musica3 = new Musica("Faroeste Caboclo", "Legião Urbana", 360);
var musica4 = new Musica("Geraldo", "Zé Ramalho", 240);
var musica5 = new Musica("Avohai", "Zé Ramalho", 200);

var rockNacional = new Playlist { Nome = "Rock Nacional" };
rockNacional.Add(musica1);
rockNacional.Add(musica2);
rockNacional.Add(musica3);
rockNacional.Add(musica4);
rockNacional.Add(musica5);
ExibirPlaylist(rockNacional);


var legiaoUrbana = new Playlist { Nome = "Mais populares da Legião" };
legiaoUrbana.Add(new Musica("Tempo Perdido", "Legião Urbana", 180));
legiaoUrbana.Add(new Musica("Pais e Filhos", "Legião Urbana", 210));
legiaoUrbana.Add(new Musica("Faroeste Caboclo", "Legião Urbana", 360));
legiaoUrbana.Add(new Musica("Eduardo e Mônica", "Legião Urbana", 240));
legiaoUrbana.Add(new Musica("Será", "Legião Urbana", 200));
ExibirPlaylist(legiaoUrbana);

var player = new PlayerDeMusica();
player.AdicionarNaFila(musica1);
player.AdicionarNaFila(rockNacional);
ExibirFila(player);
ExibirHistorico(player);


var proxima = player.ProximaMusicaDaFila();
if (proxima is not null)
{
    Console.WriteLine($"\nTocando a musica {proxima.Titulo}...");
}
else
{
    Console.WriteLine("\nFila de reproducao vazia!");
}

ExibirFila(player);
ExibirHistorico(player);

var anterior = player.MusicaAnterior();
if (anterior is not null)
{
    Console.WriteLine($"\nTocando a musica {anterior.Titulo}...");
}
else
{
    Console.WriteLine("\nNão há música anterior!");
}
ExibirFila(player);
ExibirHistorico(player);

void ExibirHistorico(PlayerDeMusica player)
{
    Console.WriteLine("\nExibindo o histórico de reprodução:");
    foreach (var musica in player.Historico())
    {
        Console.WriteLine($"\t - {musica.Titulo}");
    }
}

void ExibirFila(PlayerDeMusica player)
{
    Console.WriteLine("\nExibindo a fila de reprodução:");
    foreach (var musica in player.Fila())
    {
        Console.WriteLine($"\t - {musica.Titulo}");
    }
}

void ExibirMaisTocadas(Playlist playlist1, Playlist playlist2)
{
    // Musica (chave/key), Contagem (valor/value)
    Dictionary<Musica, int> ranking = [];
    foreach (var musica in playlist1)
    {
        ranking.Add(musica, 1);
    }
    foreach (var musica in playlist2)
    {
        if (ranking.TryGetValue(musica, out int contagem))
        {
            contagem++;
            ranking[musica] = contagem;
        }
        else
        {
            ranking[musica] = 1;
        }
    }
    List<KeyValuePair<Musica, int>> top = new(ranking);
    top.Sort(new PorContagem());
    Console.WriteLine("\n Top 3 Musicas mais incluidas nas playlist");
    int contador = 1;
    foreach (var par in top)
    {
        Console.WriteLine($"\t - {par.Key.Titulo}");
        contador++;
        if (contador > 3) break;
    }
}

void ExibirPlaylist(Playlist playlist)
{
    Console.WriteLine($"\n Tocando as musicas de {playlist.Nome}");
    foreach (var musica in playlist)
    {
        Console.WriteLine($"\t - {musica.Titulo} ({musica.Artista}) - {musica.Duracao} segundos");
    }
}
void RemoverMusicaPeloTitulo(Playlist playlist, string titulo)
{
    var musicaEncontrada = playlist.ObterPeloTitulo(titulo);
    if (musicaEncontrada is not null)
    {
        playlist.Remove(musicaEncontrada);
        Console.WriteLine($"\n Música '{musicaEncontrada.Titulo}' removida da playlist.");
    }
    else
    {
        Console.WriteLine("\n Música não encontrada.");
    }
    ExibirPlaylist(playlist);
}
void ExibirMusicaAleatoria(Playlist playlist)
{
    var musicaAleatoria = playlist.ObterAleatoria();
    if (musicaAleatoria is not null)
    {
        Console.WriteLine($"\n Música aleatória selecionada: {musicaAleatoria.Titulo}");
    }
    else
    {
        Console.WriteLine("\n A playlist está vazia.");
    }
}

class Musica : IComparable
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; } // Duração em segundos
    public Musica(string titulo, string artista, int duracao)
    {
        Titulo = titulo;
        Artista = artista;
        Duracao = duracao;
    }
    public override string ToString()
    {
        return $"{Titulo} - {Artista} ({Duracao} segundos)";
    }
    public int CompareTo(object? other)
    {
        if (other is null) return -1;
        if (other is Musica outraMusica) return this.Duracao.CompareTo(outraMusica.Duracao);
        return -1;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is Musica outraMusica) return this.Titulo.Equals(outraMusica.Titulo) && this.Artista.Equals(outraMusica.Artista);
        return false;
    }

    public override int GetHashCode()
    {
        return this.Titulo.GetHashCode() ^ this.Artista.GetHashCode();
    }
}

class PorContagem : IComparer<KeyValuePair<Musica, int>>
{
    public int Compare(KeyValuePair<Musica, int> x, KeyValuePair<Musica, int> y)
    {

        return y.Value.CompareTo(x.Value);
    }
}

class PorArtista : IComparer<Musica>
{
    public int Compare(Musica? x, Musica? y)
    {
        if (x is null || y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;
        return x.Artista.CompareTo(y.Artista);
    }
}

class PorTitulo : IComparer<Musica>
{
    public int Compare(Musica? x, Musica? y)
    {
        if (x is null || y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;
        return x.Titulo.CompareTo(y.Titulo);
    }
}

class Playlist : ICollection<Musica>
{
    private HashSet<Musica> set = [];
    private List<Musica> lista = [];
    public string Nome { get; set; }

    public int Count => lista.Count;

    public bool IsReadOnly => false;

    public void Add(Musica musica)
    {
        if (set.Add(musica))
        {
            lista.Add(musica);
        }
    }
    public Musica? ObterPeloTitulo(string titulo)
    {
        foreach (var musica in lista)
        {
            if (musica.Titulo == titulo)
            {
                return musica;
            }
        }
        return null;
    }
    public Musica? ObterAleatoria()
    {
        if (lista.Count == 0) return null;
        var random = new Random();
        var indiceAleatorio = random.Next(0, lista.Count - 1);
        return lista[indiceAleatorio];
    }

    public void OrdenarPorDuracao()
    {
        lista.Sort();
    }

    public void OrdenarPorArtista()
    {
        lista.Sort(new PorArtista());
    }

    public void Clear()
    {
        lista.Clear();
    }

    public bool Contains(Musica item)
    {
        return lista.Contains(item);
    }

    public void CopyTo(Musica[] array, int arrayIndex)
    {
        lista.CopyTo(array, arrayIndex);
    }

    public IEnumerator<Musica> GetEnumerator()
    {
        return lista.GetEnumerator();
    }

    public bool Remove(Musica item)
    {
        return lista.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class PlayerDeMusica
{
    private Queue<Musica> fila = []; // primeiro a entrar, primeiro a sair (FIFO - First In, First Out)
    private Stack<Musica> pilha = []; // último a entrar, primeiro a sair (LIFO - Last In, First Out)
    public void AdicionarNaFila(Musica musica)
    {
        fila.Enqueue(musica);
    }

    public void AdicionarNaFila(Playlist playlist)
    {
        foreach (var musica in playlist)
        {
            AdicionarNaFila(musica);
        }
    }
    public Musica? ProximaMusicaDaFila()
    {
        if (fila.Count == 0) return null;
        var musica = fila.Dequeue();
        pilha.Push(musica);
        return fila.Dequeue();
    }

    public Musica? MusicaAnterior()
    {
        if (pilha.Count == 0) return null;
        return pilha.Pop();
    }
    public IEnumerable<Musica> Fila()
    {
        foreach (var musica in fila)
        {
            yield return musica;
        }
    }
    public IEnumerable<Musica> Historico()
    {
        foreach(var musica in pilha)
        {
            yield return musica;
        }
    }
}