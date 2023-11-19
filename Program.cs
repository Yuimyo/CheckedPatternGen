using SixLabors.ImageSharp;

namespace CheckedPatternGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            drawCheckedPatternImage(new CheckedPatternConfig()
            {
                Height = 100,
                Width = 100,
                SaveFilePath = "D:\\pat.png",
                Overwriting = true,
                Color1 = Color.White,
                Color2 = Color.Black,
            });
        }

        public class CheckedPatternConfig
        {
            public int Height = 100;
            public int Width = 100;
            public int HeightUnit = 10;
            public int WidthUnit = 10;
            public string SaveFilePath = string.Empty;
            public bool Overwriting = false;
            public Color Color1 = Color.White;
            public Color Color2 = Color.Black;

            public bool Validate()
            {
                if (Height <= 0)
                    throw new ArgumentException();
                if (Width <= 0)
                    throw new ArgumentException();
                if (HeightUnit <= 0)
                    throw new ArgumentException();
                if (WidthUnit <= 0)
                    throw new ArgumentException();
                if (File.Exists(SaveFilePath) && !Overwriting)
                    throw new ArgumentException();

                return true;
            }
        }

        static void drawCheckedPatternImage(CheckedPatternConfig config)
        {
            config.Validate();

            var tempDir = Path.GetDirectoryName(config.SaveFilePath);
            if (tempDir != null)
            {
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }
            }

            int h = 0, w = 0;
            using (var image = new Image<Rgba32>(config.Width, config.Height))
            {
                for (int i = 0; i < (config.Width + config.WidthUnit - 1) / config.WidthUnit; i++)
                {
                    for (int j = 0; j < (config.Height + config.HeightUnit - 1) / config.HeightUnit; j++)
                    {
                        for (int dx = 0; dx < config.WidthUnit; dx++)
                        {
                            for (int dy = 0; dy < config.HeightUnit; dy++)
                            {
                                int x = i * config.WidthUnit + dx;
                                int y = j * config.HeightUnit + dy;

                                if (x >= config.Width || y >= config.Height) continue;

                                if ((i + j) % 2 == 0)
                                    image[x, y] = config.Color1;
                                else
                                    image[x, y] = config.Color2;
                            }
                        }
                    }
                }

                image.Save(config.SaveFilePath);
            }
        }
    }
}