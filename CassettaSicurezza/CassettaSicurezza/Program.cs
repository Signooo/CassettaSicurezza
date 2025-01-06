class CassettaSicurezza
{
    private string cSeriale;
    private string produttore;
    private int cSegreto;
    private Oggetto oggetto;
    private int pin;
    private bool stato;

    public CassettaSicurezza(string cSeriale, string produttore, int cSegreto, int pin)
    {
        this.cSeriale = cSeriale;
        this.produttore = produttore;
        this.cSegreto = cSegreto;
        this.pin = pin;
        this.stato = false; 
    }

    public string CSeriale
    {
        get { return cSeriale; }
    }

    public string Produttore
    {
        get { return produttore; }
    }

    public bool Stato
    {
        get { return stato; }
    }

    public void CambiaPin(int cSegreto1, int nuovoPin)
    {
        if (cSegreto1 == cSegreto)
        {
            pin = nuovoPin;
        }
    }

    public void InserisciOggetto(int pinInserito, Oggetto ogg)
    {
        if (stato == false && pinInserito == pin)
        {
            stato = true;
            oggetto = ogg;
        }
    }

    public void RimuoviOggetto(int pinInserito)
    {
        if (pinInserito == pin)
        {
            stato = true;
            oggetto = null;
        }
    }

}

class CassettaSpeciale : CassettaSicurezza
{
    public CassettaSpeciale(string cSicurezza, string produttore, int cSegreto, int pin) : base(cSicurezza, produttore, cSegreto, pin)
    {
    }

    public int CalcVASS(Oggetto oggetto, int valoreOgg, int valoreAss)
    {
        string tipo = oggetto.getTipologia();
        if ( tipo == "GPreziosi")
        {
            return valoreAss - (valoreAss * 10 / 100);
        }
        else if (tipo == "Documenti")
        {
            return valoreAss - (valoreAss * 20 / 100);
        }
        else if (tipo == "Chiavi")
        {
            return valoreAss - (valoreAss * 30 / 100);
        }
        else
        {
            return -1;
        }
    }
}

class Oggetto
{
    private string id;
    private int valore;
    private int valoreass;

    public string ID
    {
        get { return id; }
    }

    public int Valore
    {
        get { return valore; }
    }

    public int Volume
    {
        get { return valoreass; }
    }

    public Oggetto(string id, int valore, int valoreass)
    {
        this.id = id;
        this.valore = valore;
        this.valoreass = valoreass;
    }
    virtual public int CalcVASS(int valore)
    {
        return valore;
    }
    public string getTipologia()
    {
        return GetType().ToString();
    }
}

class GPreziosi : Oggetto
{
    private string tipo;
    public string Tipo
    {
        get { return tipo; }
    }

    public GPreziosi(string tipo) : base("GPrezioso", 0, 0)
    {
        this.tipo = tipo;
    }

    public override int CalcVASS(int valore)
    {
        return valore * 5;
    }
}

class Documenti : Oggetto
{
    private string tipo;
    public string Tipo
    {
        get { return tipo; }
    }

    public Documenti(string tipo) : base("Documento", 0, 0)
    {
        this.tipo = tipo;
    }

    public override int CalcVASS(int valore)
    {
        if (valore >= 100)
        {
            return valore;
        }
        else
        {
            valore += 50;
            return valore;
        }
    }
}

class Chiavi : Oggetto
{
    private string tipo;
    public string Tipo
    {
        get { return tipo; }
    }

    public Chiavi(string tipo) : base("Chiave", 0, 0)
    {
        this.tipo = tipo;
    }

    public override int CalcVASS(int valore)
    {
        return valore * 1000;
    }
}

class Program
{
    public static void Main()
    {
        CassettaSicurezza C1 = new CassettaSicurezza("EL5671", "Tizio", 1234567, 12345);
        CassettaSicurezza C2 = new CassettaSicurezza("ZT9871", "Caio", 3121237, 54321);
        CassettaSicurezza C3 = new CassettaSicurezza("ABC9821", "Tnn", 7867123, 0);

        GPreziosi G1 = new GPreziosi("Collana");
        Documenti D1 = new Documenti("Testamento");
        Chiavi Ch1 = new Chiavi("Fisica");

        C1.InserisciOggetto(12345, G1);
        C2.InserisciOggetto(54321, D1);
        C3.InserisciOggetto(0, Ch1);

        Console.WriteLine("Cassette di Sicurezza Normali:");
        Console.WriteLine($"ID: {C1.CSeriale}, Tipo: {G1.getTipologia()}, Stato: {C1.Stato}");
        Console.WriteLine($"ID: {C2.CSeriale}, Tipo: {D1.getTipologia()}, Stato: {C2.Stato}");
        Console.WriteLine($"ID: {C3.CSeriale}, Tipo: {Ch1.getTipologia()}, Stato: {C3.Stato}");
        Console.WriteLine();

        CassettaSpeciale CS1 = new CassettaSpeciale("AH1234", "Trinn", 3122372, 1);
        CassettaSpeciale CS2 = new CassettaSpeciale("DC9876", "Io", 6967636, 1000);
        CassettaSpeciale CS3 = new CassettaSpeciale("BR6069", "Tu", 8123776, 100);

        C1.RimuoviOggetto(12345);
        C2.RimuoviOggetto(54321);
        C3.RimuoviOggetto(0);

        CS1.InserisciOggetto(1, G1);
        CS2.InserisciOggetto(1000, D1);
        CS3.InserisciOggetto(100, Ch1);

        Console.WriteLine("Cassette di Sicurezza Speciali:");
        Console.WriteLine($"ID: {CS1.CSeriale}, Tipo: {G1.getTipologia()}, Valore Assicurato: {G1.CalcVASS(G1.Valore)}");
        Console.WriteLine($"ID: {CS2.CSeriale}, Tipo: {D1.getTipologia()}, Valore Assicurato: {D1.CalcVASS(D1.Valore)}");
        Console.WriteLine($"ID: {CS3.CSeriale}, Tipo: {Ch1.getTipologia()}, Valore Assicurato: {Ch1.CalcVASS(Ch1.Valore)}");
    }
}


