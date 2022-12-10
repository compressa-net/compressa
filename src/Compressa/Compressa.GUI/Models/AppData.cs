using Compressa.GUI.Services;
using FontAwesome;

namespace Compressa.GUI.Models;

public static class AppData
{
    private static Random random = new Random();
    public static string[] Statuses = new string[] { "Ready to Pay", "Cooking", "Ready to Order" };
    public static List<int> Tables = new List<int> { 7, 8, 9, 10, 11, 12, 13, 14 };

    public static List<Source> Sources = new List<Source>
    {
        new Source(){ Name = "Audiobooks", Description = "Audible", ItemCount = "7 audiobooks", Image = FontAwesomeIcons.FileAudio},
        new Source(){ Name = "Podcasts",  Description = "Podcasts", ItemCount = "0 podcasts", Image = FontAwesomeIcons.Podcast},
        new Source(){ Name = "YouTube channels", Description = "YouTube channels", ItemCount = "0 channels", Image = FontAwesomeIcons.Video},
    };

    public static List<Item> Items = new List<Item>
    {
        new Item(){ Title = "AI Superpowers", Author = "Kai-Fu Lee", Image = "aisuperpowers.jpg", Category = ItemCategory.Technology},
        new Item(){ Title = "Flow", Author = "Mihaly Csikszentmihalyi", Image = "flow.jpg", Category = ItemCategory.Relationships},
        new Item(){ Title = "Rework", Author = "Jason Fried, David Heinemeier Hansson", Image = "rework.jpg", Category = ItemCategory.Business},
        new Item(){ Title = "Rich Dad Poor Dad", Author = "Robert T. Kiyosaki", Image = "richdadpoordad.jpg", Category = ItemCategory.Finance},
        new Item(){ Title = "Superintelligence", Author = "Kai-Fu Lee", Image = "superintelligence.jpg", Category = ItemCategory.Technology},
        new Item(){ Title = "The China Study", Author = "Kai-Fu Lee", Image = "thechinastudy.jpg", Category = ItemCategory.Health},
        new Item(){ Title = "The Five Love Languages", Author = "Kai-Fu Lee", Image = "thefivelovelanguages.jpg", Category = ItemCategory.Relationships},
    };

    public static List<Order> Orders { get; set; } = GenerateOrders(null);

    private static List<Order> GenerateOrders(ICompressaClientService compressaClientService)
    {
        if (compressaClientService != null)
        {
            var items = compressaClientService.GetAllMetadataAsync();
        }

        random.Shuffle(Tables);
        List<Order> orders = new List<Order>();
        for (int i = 0; i < Tables.Count; i++)
        {
            orders.Add((new Order()
            {
                Table = Tables[i],
                Status = RandomStatus(),
                Items = GenerateItems()
            }));
        }

        orders.OrderByDescending(x => x.Status);
        return orders;
    }

    private static List<Item> GenerateItems()
    {
        List<Item> items = new List<Item>();
        int numItems = random.Next(1, Items.Count - 1);
        random.Shuffle(Items);
        for (int i = 0; i < numItems; i++)
        {
            items.Add(Items[i]);
        }

        return items;
    }

    private static string RandomStatus()
    {
        var i = random.Next(0, Statuses.Length - 1);
        return Statuses[i];
    }
}

static class RandomExtensions
{
    public static void Shuffle<T>(this Random rng, List<T> array)
    {
        int n = array.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}