using Compressa.GUI.Services;
using Compressa.Models.Metadata;
using FontAwesome;
using System.Text.Json;

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

    public static List<Item> Audiobooks = new List<Item>
    {
        new Item(){ Id = "aisuperpowers", Title = "AI Superpowers", Author = "Kai-Fu Lee", Category = ItemCategory.Technology},
        new Item(){ Id = "flow",Title = "Flow", Author = "Mihaly Csikszentmihalyi", Category = ItemCategory.Relationships},
        new Item(){ Id = "rework",Title = "Rework", Author = "Jason Fried, David Heinemeier Hansson", Category = ItemCategory.Business},
        new Item(){ Id = "richdadpoordad",Title = "Rich Dad Poor Dad", Author = "Robert T. Kiyosaki", Category = ItemCategory.Finance},
        new Item(){ Id = "superintelligence",Title = "Superintelligence", Author = "Kai-Fu Lee", Category = ItemCategory.Technology},
        new Item(){ Id = "thechinastudy",Title = "The China Study", Author = "Kai-Fu Lee", Category = ItemCategory.Health},
        new Item(){ Id = "thefivelovelanguages",Title = "The Five Love Languages", Author = "Kai-Fu Lee", Category = ItemCategory.Relationships},
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
        string metadataCacheFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Metadata\\getallmetadata.json");
        MetadataRoot[] metadata = JsonSerializer.Deserialize<MetadataRoot[]>(File.ReadAllText(metadataCacheFilename));

        List<Item> items = new List<Item>();
        for (int i = 0; i < Audiobooks.Count; i++)
        {
            var audiobookMeta = metadata.FirstOrDefault(x => x.Audiobook.Id == Audiobooks[i].Id);

            if (audiobookMeta != null)
            {
                Audiobooks[i].Meta = audiobookMeta.Audiobook;
                Audiobooks[i].ImageFilename = metadata[i].Audiobook.Id + ".jpg";

                items.Add(Audiobooks[i]);
            }
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