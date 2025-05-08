using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Text;

class Program
{
    static readonly string asciiChars = "@%#*+=-:. ";

    static void Main()
    {
        var path = "58172c59-65c8-48be-9f4c-89d7930de2b8.png";
        using var image = Image.Load<Rgba32>(path);

        int width = 250;
        int height = image.Height * width / image.Width / 2;

        image.Mutate(x => x.Resize(width, height).Grayscale());

        var sb = new StringBuilder();
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                var pixel = image[x, y];
                var brightness = pixel.R / 255.0;
                int charIndex = (int)(brightness * (asciiChars.Length - 1));
                sb.Append(asciiChars[charIndex]);
            }
            sb.AppendLine();
        }

        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine(sb.ToString());
        
        Console.ReadKey();
    }
}
