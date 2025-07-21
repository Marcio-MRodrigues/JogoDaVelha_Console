using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;

// 25. Faça um jogo da velha funcional, exercendo todas as funcionalidades de um jogo comum.

/*
 * Dúvidas:
 * - A separação em classes foi feita de forma apropriada?
 * - As jogadas deveriam ser feitas através de iterações ou descritivamente como foram feitas?
 * - Como instâncias funcionam?
 * 
 *  
 *  Observações:
 * - A string "matrizDoJogo" é passada para outros métodos como uma instância.
 *  É por isso que as alterações feitas na variável "matriz" alteram a matriz original "matrizDoJogo".
 */

// consensar VerificaGanhador.

class Program
{
    // Monta o loop principal do programa, bifurca para as duas modalidades de jogo e permite jogar novamente.
    static void Main(string[] args)
    {
        string respostaUsuario;

        do
        {
            Console.Clear();
            Console.WriteLine("Vamos jogar um jogo da velha.\n");

            do
            {
                Console.Write("Você deseja jogar (1) contra um colega ou (2) contra mim? ");
                respostaUsuario = Console.ReadLine();
            }
            while (!(respostaUsuario == "1" || respostaUsuario == "2"));

            if (respostaUsuario == "1") { Multiplayer(); } else { SinglePlayer(); }

            do
            {
                Console.Write("Deseja tentar novamente (S/N)? ");
                respostaUsuario = Console.ReadLine().ToLower();
            }
            while (!(respostaUsuario == "s" || respostaUsuario == "n"));
        }
        while (respostaUsuario == "s");

        Console.Clear();
        Console.WriteLine("Obrigado por jogar!\n\n\n\n\n");

    }

    // Loop do jogo multiplayer.
    static void Multiplayer()
    {
        string[,] matrizDoJogo = { { "7", "8", "9" }, { "4", "5", "6" }, { "1", "2", "3" } };

        string jogador1;
        string jogador2;

        int rodadas = 0;

        Random random = new Random();

        Console.WriteLine();
        Console.WriteLine("Ok, você escolheu jogar contra um colega.\n");

        Console.Write("Antes de sortear o jogador que jogará primeiro, preciso que\nos jogadores decidam quem jogará como ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("VERMELHO");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" e como ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("AZUL");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(".\n\n");

        PressioneQualquerTecla();
        Console.WriteLine();

        AnimacaoSorteio();
        Console.WriteLine();

        if (random.Next(2) == 0)
        {
            jogador1 = "X";
            jogador2 = "O";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("O jogador AZUL jogará primeiro\n");
            Console.ForegroundColor = ConsoleColor.White;

            PressioneQualquerTecla();
        }
        else
        {
            jogador1 = "O";
            jogador2 = "X";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("O jogador VERMELHO jogará primeiro\n");
            Console.ForegroundColor = ConsoleColor.White;

            PressioneQualquerTecla();
        }
        Console.Clear();

        // Rodadas
        do
        {
            // Rodada Jogador 1
            ImprimeMatriz(matrizDoJogo);

            Jogada(matrizDoJogo, jogador1);
            Console.Clear();
            rodadas++;

            if (VerificaGanhador(matrizDoJogo, jogador1))
            {
                if (jogador1 == "X")
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nJOGADOR AZUL é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nJOGADOR VERMELHO é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
                break;
            }

            if (VerificaVelha(matrizDoJogo, rodadas)) { break; }

            // Rodada Jogador 2
            ImprimeMatriz(matrizDoJogo);

            Jogada(matrizDoJogo, jogador2);
            Console.Clear();
            rodadas++;

            if (VerificaGanhador(matrizDoJogo, jogador2))
            {
                if (jogador2 == "X")
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nJOGADOR AZUL é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nJOGADOR VERMELHO é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
                break;
            }

            if (VerificaVelha(matrizDoJogo, rodadas)) { break; }

        }
        while (true);
    }

    // Loop do jogo sigle player.
    static void SinglePlayer()
    {
        string[,] matrizDoJogo = { { "7", "8", "9" }, { "4", "5", "6" }, { "1", "2", "3" } };

        string jogador1;
        string jogador2;

        int nivelDificuldade;
        int rodadas = 0;

        Random random = new Random();

        Console.Clear();
        Console.Write("Ok, você escolheu jogar contra mim.\n\nEu serei o jogador ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("AZUL");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" e você será o jogador ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("VERMELHO");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(".\n");

        // nivel de dificuldade.
        Console.WriteLine("Você pode jogar em três níveis de dificuldade.\n");
        Console.WriteLine("(1) Fácil");
        Console.WriteLine("(2) Difícil");
        Console.WriteLine();

        nivelDificuldade = EscolhaDificuldade();

        switch (nivelDificuldade)
        {
            case 1:
                Console.WriteLine("\nVocê escolheu o nível fácil.");
                nivelDificuldade = 5;
                break;
            case 2:
                Console.WriteLine("\nVocê escolheu o nível difícil.");
                nivelDificuldade = 1;
                break;
        }

        // sorteia o primeiro a jogar.
        Console.WriteLine("\nO próximo passo é sortear quem jogará primeiro.\n");

        AnimacaoSorteio();

        if (random.Next(2) == 0)
        {
            jogador1 = "X";
            jogador2 = "O";

            Console.Write("\nEu ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[AZUL] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("jogarei primeiro\n");

            PressioneQualquerTecla();
            Console.Clear();
        }
        else
        {
            jogador1 = "O";
            jogador2 = "X";

            Console.Write("\nVocê ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[VERMELHO] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("jogará primeiro\n");

            PressioneQualquerTecla();
            Console.Clear();
        }

        // Rodadas
        do
        {
            // Rodada Jogador 1
            ImprimeMatriz(matrizDoJogo);

            if (jogador1 == "X")
            {
                JogadaComputador(matrizDoJogo, nivelDificuldade);
            }
            else
            {
                Jogada(matrizDoJogo, jogador1);
            }
            Console.Clear();
            Console.Clear();
            rodadas++;

            if (VerificaGanhador(matrizDoJogo, jogador1))
            {
                if (jogador1 == "X")
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nJOGADOR AZUL é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nJOGADOR VERMELHO é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
                break;
            }

            if (VerificaVelha(matrizDoJogo, rodadas)) { break; }

            // Rodada Jogador 2
            ImprimeMatriz(matrizDoJogo);

            if (jogador2 == "X")
            {
                JogadaComputador(matrizDoJogo, nivelDificuldade);
            }
            else
            {
                Jogada(matrizDoJogo, jogador2);
            }
            Console.Clear();
            rodadas++;

            if (VerificaGanhador(matrizDoJogo, jogador2))
            {
                if (jogador2 == "X")
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nJOGADOR AZUL é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    ImprimeMatriz(matrizDoJogo);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nJOGADOR VERMELHO é o vencedor!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
                break;
            }

            if (VerificaVelha(matrizDoJogo, rodadas)) { break; }

        }
        while (true);
    }

    // Imprime a matriz atual do jogo.
    static void ImprimeMatriz(string[,] matriz)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (matriz[i, j] == "X")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (matriz[i, j] == "O")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write($"{matriz[i, j]} ");
            }
            Console.WriteLine();
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    // Executa as jogadas dos usuários.
    static void Jogada(string[,] matriz, string jogador)
    {
        if (jogador == "X")
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nRodada do JOGADOR AZUL.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nRodada do JOGADOR VERMELHO.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        do
        {
            Console.Write("\nDigite no teclado numérico um dos números disponíveis: ");
            string respostaUsuario = Console.ReadLine();

            if (respostaUsuario == "7" && respostaUsuario == matriz[0, 0])
            {
                matriz[0, 0] = jogador;
                break;
            }
            if (respostaUsuario == "8" && respostaUsuario == matriz[0, 1])
            {
                matriz[0, 1] = jogador;
                break;
            }
            if (respostaUsuario == "9" && respostaUsuario == matriz[0, 2])
            {
                matriz[0, 2] = jogador;
                break;
            }
            if (respostaUsuario == "4" && respostaUsuario == matriz[1, 0])
            {
                matriz[1, 0] = jogador;
                break;
            }
            if (respostaUsuario == "5" && respostaUsuario == matriz[1, 1])
            {
                matriz[1, 1] = jogador;
                break;
            }
            if (respostaUsuario == "6" && respostaUsuario == matriz[1, 2])
            {
                matriz[1, 2] = jogador;
                break;
            }
            if (respostaUsuario == "1" && respostaUsuario == matriz[2, 0])
            {
                matriz[2, 0] = jogador;
                break;
            }
            if (respostaUsuario == "2" && respostaUsuario == matriz[2, 1])
            {
                matriz[2, 1] = jogador;
                break;
            }
            if (respostaUsuario == "3" && respostaUsuario == matriz[2, 2])
            {
                matriz[2, 2] = jogador;
                break;
            }
        }
        while (true);

    }

    static void JogadaComputador(string[,] matriz, int distracao)
    {
        Random random = new Random();

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\nAgora é a minha vez, pressione qualquer tecla para continuar.");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.White;

        // o valor de distração é o mesmo valor do nível de dificuldade.
        if (random.Next(distracao) == 0)
        {
            if (JogadaVencedora(matriz)) { return; }
        }

        if (random.Next(distracao) == 0)
        {
            if (OcuparCentro(matriz)) { return; }
        }

        if (OcuparCantos(matriz)) { return; }

        if (OcuparLaterais(matriz)) { return; }
    }

    // Verifica se houve algum ganhador.
    static bool VerificaGanhador(string[,] matriz, string jogador)
    {
        if (matriz[0, 0] == jogador && matriz[0, 1] == jogador && matriz[0, 2] == jogador)
        {
            return true;
        }
        else if (matriz[1, 0] == jogador && matriz[1, 1] == jogador && matriz[1, 2] == jogador)
        {
            return true;
        }
        else if (matriz[2, 0] == jogador && matriz[2, 1] == jogador && matriz[2, 2] == jogador)
        {
            return true;
        }
        else if (matriz[0, 0] == jogador && matriz[1, 0] == jogador && matriz[2, 0] == jogador)
        {
            return true;
        }
        else if (matriz[0, 1] == jogador && matriz[1, 1] == jogador && matriz[2, 1] == jogador)
        {
            return true;
        }
        else if (matriz[0, 2] == jogador && matriz[1, 2] == jogador && matriz[2, 2] == jogador)
        {
            return true;
        }
        else if (matriz[0, 0] == jogador && matriz[1, 1] == jogador && matriz[2, 2] == jogador)
        {
            return true;
        }
        else if (matriz[0, 2] == jogador && matriz[1, 1] == jogador && matriz[2, 0] == jogador)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Verifica se deu velha.
    static bool VerificaVelha(string[,] matriz, int contador)
    {
        if (contador == 9)
        {
            ImprimeMatriz(matriz);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Deu VELHA...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            return true;
        }
        return false;
    }

    // Define o nível de dificuldade.
    static int EscolhaDificuldade()
    {
        string entradaUsuário;

        do
        {
            Console.Write("Em qual nível você gostaria de jogar? ");
            entradaUsuário = Console.ReadLine();
        }
        while (!(entradaUsuário == "1" || entradaUsuário == "2" || entradaUsuário == "3"));

        return int.Parse(entradaUsuário);
    }

    static void AnimacaoSorteio()
    {
        //Thread.Sleep(2000);

        for (int i = 0; i < 30; i++)
        {
            Console.Write("*");
            Thread.Sleep(50);
        }

        Console.WriteLine(" [sorteio realizado]");
        Thread.Sleep(1000);
    }

    static void PressioneQualquerTecla()
    {
        Console.Write("Pressione qualquer tecla para continuar. ");
        Console.ReadKey();
    }

    // JOGADAS AUTOMATIZADAS:

    // Varre oportunidade de vitória do computador, se não houver, varre oportunidade de vitória do usuário.
    static bool JogadaVencedora(string[,] matriz)
    {

        string casaOcupada = "X"; // Ativa a verificação de vitória do computador 

        for (int i = 0; i < 2; i++)
        {
            if (matriz[0, 0] == "7" && matriz[0, 1] == casaOcupada && matriz[0, 2] == casaOcupada) { matriz[0, 0] = "X"; return true; }
            if (matriz[0, 0] == casaOcupada && matriz[0, 1] == "8" && matriz[0, 2] == casaOcupada) { matriz[0, 1] = "X"; return true; }
            if (matriz[0, 0] == casaOcupada && matriz[0, 1] == casaOcupada && matriz[0, 2] == "9") { matriz[0, 2] = "X"; return true; }

            if (matriz[1, 0] == "4" && matriz[1, 1] == casaOcupada && matriz[1, 2] == casaOcupada) { matriz[1, 0] = "X"; return true; }
            if (matriz[1, 0] == casaOcupada && matriz[1, 1] == "5" && matriz[1, 2] == casaOcupada) { matriz[1, 1] = "X"; return true; }
            if (matriz[1, 0] == casaOcupada && matriz[1, 1] == casaOcupada && matriz[1, 2] == "6") { matriz[1, 2] = "X"; return true; }

            if (matriz[2, 0] == "1" && matriz[2, 1] == casaOcupada && matriz[2, 2] == casaOcupada) { matriz[2, 0] = "X"; return true; }
            if (matriz[2, 0] == casaOcupada && matriz[2, 1] == "2" && matriz[2, 2] == casaOcupada) { matriz[2, 1] = "X"; return true; }
            if (matriz[2, 0] == casaOcupada && matriz[2, 1] == casaOcupada && matriz[2, 2] == "3") { matriz[2, 2] = "X"; return true; }


            if (matriz[0, 0] == "7" && matriz[1, 0] == casaOcupada && matriz[2, 0] == casaOcupada) { matriz[0, 0] = "X"; return true; }
            if (matriz[0, 0] == casaOcupada && matriz[1, 0] == "4" && matriz[2, 0] == casaOcupada) { matriz[1, 0] = "X"; return true; }
            if (matriz[0, 0] == casaOcupada && matriz[2, 0] == casaOcupada && matriz[2, 0] == "1") { matriz[2, 0] = "X"; return true; }

            if (matriz[0, 1] == "8" && matriz[1, 1] == casaOcupada && matriz[2, 1] == casaOcupada) { matriz[0, 1] = "X"; return true; }
            if (matriz[0, 1] == casaOcupada && matriz[1, 1] == "5" && matriz[2, 1] == casaOcupada) { matriz[1, 1] = "X"; return true; }
            if (matriz[0, 1] == casaOcupada && matriz[2, 1] == casaOcupada && matriz[2, 1] == "2") { matriz[2, 1] = "X"; return true; }

            if (matriz[0, 2] == "9" && matriz[1, 2] == casaOcupada && matriz[2, 2] == casaOcupada) { matriz[0, 2] = "X"; return true; }
            if (matriz[0, 2] == casaOcupada && matriz[1, 2] == "6" && matriz[2, 2] == casaOcupada) { matriz[1, 2] = "X"; return true; }
            if (matriz[0, 2] == casaOcupada && matriz[2, 2] == casaOcupada && matriz[2, 2] == "3") { matriz[2, 2] = "X"; return true; }


            if (matriz[0, 0] == "7" && matriz[1, 1] == casaOcupada && matriz[2, 2] == casaOcupada) { matriz[0, 0] = "X"; return true; }
            if (matriz[0, 0] == casaOcupada && matriz[1, 1] == "5" && matriz[2, 2] == casaOcupada) { matriz[1, 1] = "X"; return true; }
            if (matriz[0, 0] == casaOcupada && matriz[1, 1] == casaOcupada && matriz[2, 2] == "3") { matriz[2, 2] = "X"; return true; }

            if (matriz[0, 2] == "9" && matriz[1, 1] == casaOcupada && matriz[0, 0] == casaOcupada) { matriz[0, 2] = "X"; return true; }
            if (matriz[0, 2] == casaOcupada && matriz[1, 1] == "5" && matriz[0, 0] == casaOcupada) { matriz[1, 1] = "X"; return true; }
            if (matriz[0, 2] == casaOcupada && matriz[1, 1] == casaOcupada && matriz[0, 0] == "1") { matriz[0, 2] = "X"; return true; }

            casaOcupada = "O"; // Ativa a verificação de vitória do jogador
        }
        return false;
    }

    // Ocupa o centro se estiver livre.
    static bool OcuparCentro(string[,] matriz)
    {
        if (matriz[1, 1] == "5")
        {
            matriz[1, 1] = "X";
            return true;
        }
        return false;
    }

    // Ocupa os cantos que estiverem livres.
    static bool OcuparCantos(string[,] matriz)
    {
        Random random = new Random();
        bool continua = true;

        do
        {
            int sorteio = random.Next(4);

            switch (sorteio)
            {
                case 0:
                    if (matriz[0, 0] == "7")
                    {
                        matriz[0, 0] = "X";
                        continua = false;
                        return true;
                    }
                    break;
                case 1:
                    if (matriz[0, 2] == "9")
                    {
                        matriz[0, 2] = "X";
                        continua = false;
                        return true;
                    }
                    break;
                case 2:
                    if (matriz[2, 0] == "1")
                    {
                        matriz[2, 0] = "X";
                        continua = false;
                        return true;
                    }
                    break;
                case 3:
                    if (matriz[2, 2] == "3")
                    {
                        matriz[2, 2] = "X";
                        continua = false;
                        return true;
                    }
                    break;
            }
            if (matriz[0, 0] != "7" && matriz[0, 2] != "9" && matriz[2, 0] != "1" && matriz[2, 2] != "3")
            {
                continua = false;
            }
        }
        while (continua);
        return false;
    }

    // ocupa as laterais que estiverem livres.
    static bool OcuparLaterais(string[,] matriz)
    {
        Random random = new Random();
        bool continua = true;

        do
        {
            int sorteio = random.Next(4);

            switch (sorteio)
            {
                case 0:
                    if (matriz[0, 1] == "8")
                    {
                        matriz[0, 1] = "X";
                        continua = false;
                        return true;
                    }
                    break;
                case 1:
                    if (matriz[1, 0] == "4")
                    {
                        matriz[1, 0] = "X";
                        continua = false;
                        return true;
                    }
                    break;
                case 2:
                    if (matriz[1, 2] == "6")
                    {
                        matriz[1, 2] = "X";
                        continua = false;
                        return true;
                    }
                    break;
                case 3:
                    if (matriz[2, 1] == "2")
                    {
                        matriz[2, 1] = "X";
                        continua = false;
                        return true;
                    }
                    break;
            }
            if (matriz[0, 1] != "8" && matriz[1, 0] != "4" && matriz[1, 2] != "6" && matriz[2, 1] != "2")
            {
                continua = false;
            }
        }
        while (continua);
        return false;
    }
}