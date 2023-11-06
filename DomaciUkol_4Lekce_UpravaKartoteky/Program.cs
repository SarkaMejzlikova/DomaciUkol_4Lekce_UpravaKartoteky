using DomaciUkol_4Lekce_UpravaKartoteky;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Zadání cesty pro ukládání souboru

string cesta = "";
Console.WriteLine("Dobrý den,\nchcete zadat cestu k souboru ručně? (Odpovězte zadáním čísla) \n1 - ano\n2 - ne");

string answPath = Console.ReadLine();
bool tryAgainPath = true;

while (tryAgainPath)
{
    try
    {
        switch (Int32.Parse(answPath))
        {
            case (int)TrueFalse.yes:
                Console.WriteLine("Zadejte cestu:");
                cesta = Console.ReadLine();
                break;

            case (int)TrueFalse.no:
                cesta = @"C:\text.txt";
                break;
            default:
                Console.WriteLine("Zřejmě jste jako odpověď nezadali 1 nebo 2, zkuste to znovu:");
                answPath = Console.ReadLine();
                break;
        }
        tryAgainPath = false;
    }
    catch (FormatException e)
    {
        Console.WriteLine("Zřejmě jste jako odpověď nezadali 1 nebo 2, zkuste to znovu:");
        answPath = Console.ReadLine();
    }
}

// práce s Listem

List<Osoba> osoby = new List<Osoba>();
bool jeKonec = false;

Console.WriteLine("Vítejte v menu. Pro další akci zvolte číselné označení akce:");

while (!jeKonec)
{
    Console.WriteLine("\n1 - Pridat osobu");
    Console.WriteLine("2 - Smazat osobu");
    Console.WriteLine("3 - Vypsat osoby");
    Console.WriteLine("0 - Konec");

    string answ = Console.ReadLine();
    bool tryAgainFile = true;

    while(tryAgainFile)
    {
        try
        {
            switch (Int32.Parse(answ))
            {
                    case (int)FileOperation.end:
                        jeKonec = true;
                    break;

                    case (int)FileOperation.addPerson:
                        Osoba osoba = new Osoba();
                        Console.Write("Zadej jmeno: ");
                        osoba.Jmeno = (Console.ReadLine()).ToUpper();
                        Console.Write("Zadej prijmeni: ");
                        osoba.Prijmeni = (Console.ReadLine()).ToUpper();
                        Console.Write("Zadej rok narozeni: ");

                        // kontrola roku narozeni
                        string rokNar = Console.ReadLine();
                        int rok;

                        while (!int.TryParse(rokNar, out rok) || rok < 1900)
                        {
                        Console.WriteLine("Zřejmě jste zadali špatný letopočet, zkuste to znovu:");
                        rokNar = Console.ReadLine();
                        }
                        osoba.RokNarozeni = rok;
                                            
                        osoby.Add(osoba);

                        string poleOsoba = "\n" + osoba.Jmeno + " " + osoba.Prijmeni + " " + Convert.ToString(osoba.RokNarozeni);
                        File.AppendAllText(cesta, poleOsoba);

                    break;

                    case (int)FileOperation.deletePerson:
                        Console.WriteLine("Zadejte index mazane polozky:");
                        int index = Convert.ToInt32(Console.ReadLine());
                        osoby.RemoveAt(index);
                    break;

                    case (int)FileOperation.readFile:

                        Console.WriteLine("Od jakého roku narození chcete osoby vypsat?");
                        string answYear = Console.ReadLine();
                        int answYr;

                        while (!int.TryParse(answYear, out answYr) || answYr < 1900)
                        {
                            Console.WriteLine("Zřejmě jste zadali špatný letopočet, zkuste to znovu:");
                            answYear = Console.ReadLine();
                        }

                        string[] vypisSouboru = File.ReadAllLines(cesta);
                        int cnt = 0;
                        foreach (string vs in vypisSouboru)
                        {
                            int vypisRoku = Int32.Parse(vs.Substring(vs.Length - 5));
                            if (vypisRoku >= answYr)
                            {
                                Console.WriteLine(vs);
                                cnt++;
                            }
                        }
                        if (cnt == 0)
                        {
                            Console.WriteLine("V kartotéce se nenachází osoby dle vašeho výběru roku narození.");
                        }      
                    break;

                    default:
                        Console.WriteLine("Zřejmě jste jako odpověď nedazali číslo ve škále od 0 do 3. Zkuste to znovu.");
                        answ = Console.ReadLine();
                    break;
            }
                
            tryAgainFile = false;

        }
        catch (FormatException e) 
        {
            Console.WriteLine("Jako odpověď jste nezadali číslo. Zkuste to znovu.");
            answ = Console.ReadLine();
        }
    }

}


enum TrueFalse
{
    yes = 1,
    no = 2,
}

enum FileOperation
{
    end,
    addPerson,
    deletePerson,
    readFile
}
