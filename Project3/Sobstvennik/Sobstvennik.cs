namespace Project3.Sobstvennik;

public class Sobstvennik
{
    public Sobstvennik(int sobsvennikId, string fio, string phone)
    {
        SobsvennikID = sobsvennikId;
        FIO = fio;
        Phone = phone;
    }

    public int SobsvennikID { get; set; }
    public string FIO { get; set; }
    public string Phone { get; set; }
    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
}