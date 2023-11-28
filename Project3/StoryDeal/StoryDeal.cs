namespace Project3.StoryDeal;

public class StoryDeal
{
    public StoryDeal(int dealId, string o, string sobstvennik, string pokupatel, string doverennoeLico)
    {
        DealID = dealId;
        Object = o;
        Sobstvennik = sobstvennik;
        Pokupatel = pokupatel;
        DoverennoeLico = doverennoeLico;
    }

    public int DealID { get; set; }
    public string Object { get; set; }
    public string Sobstvennik { get; set; }
    public string Pokupatel { get; set; }
    public string DoverennoeLico { get; set; }
}
