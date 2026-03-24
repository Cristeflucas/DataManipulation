using System.Collections;

var diasDaSemana = new DiasDaSemana();

var carrinho = new List<Produto>()
{
    new Produto("Coca-cola", 10.0),
    new Produto("Pizza", 20.0),
    new Produto("Hamburguer", 15.0)
};

var pares = NumerosParesComYield();
var contador = 0;
foreach (var par in pares)
{
    Console.WriteLine(par);
    if (contador > 200) break;
}

IEnumerable<int> NumerosParesSemYield(int limite)
{
    var lista = new List<int>();
    for (int i = 0; i < limite; i++)
    {
        Console.WriteLine($"Processando elemento {i}...");
        lista.Add(i * 2);
    }
    return lista;
}
IEnumerable<int> NumerosParesComYield()
{
    var i = 0;
    while (true)
    {
        Console.WriteLine($"Processando elemento {i}...");
        yield return i * 2;
        if (i > 100)
        {
            Console.WriteLine("Saindo do enumerator!");
            yield break; // Para evitar um loop infinito
        }
        i++;
    }
}
class Produto
{
    public string Nome { get; set; }
    public double Preco { get; set; }

    public Produto(string nome, double preco)
    {
        Nome = nome;
        Preco = preco;
    }
}


class DiasDaSemana : IEnumerable<string>
{
    private string[] dias = new string[] { "Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado" };
    public IEnumerator<string> GetEnumerator()
    {
        foreach (var dia in dias)
        {
            yield return dia;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

