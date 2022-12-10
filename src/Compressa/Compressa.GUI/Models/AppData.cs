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
        new Item(){ Title = "Japchae", Price    = 13.99, Quantity = 1, Image = "japchae.png", Category = ItemCategory.Business},
        new Item(){ Title = "Jajangmyeon", Price = 14.99, Quantity = 1, Image = "jajangmyeon.png", Category = ItemCategory.Business},
        new Item(){ Title = "Janchi Guksu", Price = 12.99, Quantity = 1, Image = "janchi_guksu.png", Category = ItemCategory.Business},
        new Item(){ Title = "Budae Jjigae", Price = 14.99, Quantity = 1, Image = "budae_jjigae.png", Category = ItemCategory.Business},
        new Item(){ Title = "Naengmyeon", Price = 12.99, Quantity = 1, Image = "naengmyeon.png", Category = ItemCategory.Business},
        new Item(){ Title = "Soda", Price = 2.50, Quantity = 1, Category = ItemCategory.Relationships, Image = "soda.png"},
        new Item(){ Title = "Iced Tea", Price = 3.50, Quantity = 1, Category = ItemCategory.Relationships, Image = "iced_tea.png"},
        new Item(){ Title = "Hot Tea", Price = 4.00, Quantity = 1, Category = ItemCategory.Relationships, Image = "tea.png"},
        new Item(){ Title = "Coffee", Price = 4.00, Quantity = 1, Category = ItemCategory.Relationships, Image = "coffee.png"},
        new Item(){ Title = "Milk", Price = 5.00, Quantity = 1, Category = ItemCategory.Relationships, Image = "milk.png"},
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