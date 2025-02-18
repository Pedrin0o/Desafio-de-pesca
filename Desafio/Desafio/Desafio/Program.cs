using System;
using System.Collections.Generic;

class Programa
{
    static void Main(string[] args)
    {
        // Peixes no lago
        Console.Write("Digite o número de peixes no lago (máximo 50): ");
        int N = int.Parse(Console.ReadLine());

        // Jogadores
        Console.Write("Digite o número de jogadores: ");
        int numJogadores = int.Parse(Console.ReadLine());

        // Nomes dos jogadores
        List<string> jogadores = new List<string>();
        for (int i = 0; i < numJogadores; i++)
        {
            Console.Write($"Digite o nome do jogador {i + 1}: ");
            jogadores.Add(Console.ReadLine());
        }

        // Tentativas por jogador
        Console.Write("Digite o número de iscas/tentativas do jogador: ");
        int numIscas = int.Parse(Console.ReadLine());

        // Peixes no lago
        Dictionary<int, Peixe> lago = GerarPeixes(N);

        // Quilos pescados por jogador
        Dictionary<string, double> pescados = new Dictionary<string, double>();
        foreach (var jogador in jogadores)
        {
            pescados[jogador] = 0;
        }

        // Loop de rodadas
        for (int tentativa = 0; tentativa < numIscas; tentativa++)
        {
            foreach (var jogador in jogadores)
            {
                Console.WriteLine($"{jogador}, é sua vez!");
                Console.Write("Escolha uma posição no lago (1 a 50): ");
                int posicao = int.Parse(Console.ReadLine());

                // Verifica se tem peixe na posição escolhida
                if (lago.ContainsKey(posicao))
                {
                    Peixe peixe = lago[posicao];
                    Console.WriteLine($"Você pescou um {peixe.Tipo} de {peixe.Peso} kg!");
                    pescados[jogador] += peixe.Peso;
                    lago.Remove(posicao); // Remove o peixe do lago
                }
                else
                {
                    Console.WriteLine("Não há peixes nessa posição.");
                }
            }
        }

        // Determinar o vencedor
        string vencedor = "";
        double maxPeso = 0;
        bool empate = false;

        foreach (var kvp in pescados)
        {
            if (kvp.Value > maxPeso)
            {
                vencedor = kvp.Key;
                maxPeso = kvp.Value;
                empate = false;
            }
            else if (kvp.Value == maxPeso)
            {
                empate = true;
            }
        }

        // Exibir resultado
        if (empate)
        {
            Console.WriteLine("O jogo terminou em empate!");
        }
        else
        {
            Console.WriteLine($"{vencedor} venceu com {maxPeso} kg pescados!");
        }
    }

    // Gera peixes aleatórios
    static Dictionary<int, Peixe> GerarPeixes(int N)
    {
        Dictionary<int, Peixe> peixes = new Dictionary<int, Peixe>();
        Random random = new Random();
        string[] tipos = { "tilápia", "pacu", "tambaqui" };

        for (int i = 1; i <= N; i++)
        {
            string tipo = tipos[random.Next(tipos.Length)];
            double peso = 0;

            switch (tipo)
            {
                case "tilápia":
                    peso = random.NextDouble() * (2 - 1) + 1; // Peso entre 1 e 2 kg
                    break;
                case "pacu":
                    peso = random.NextDouble() * (4 - 2) + 2; // Peso entre 2 e 4 kg
                    break;
                case "tambaqui":
                    peso = random.NextDouble() * (5 - 3) + 3; // Peso entre 3 e 5 kg
                    break;
            }

            peixes[i] = new Peixe(tipo, Math.Round(peso, 2));
        }

        return peixes;
    }
}

// Representa um peixe
class Peixe
{
    public string Tipo { get; }
    public double Peso { get; }

    public Peixe(string tipo, double peso)
    {
        Tipo = tipo;
        Peso = peso;
    }
}