using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;

namespace alpha
{
    /// <summary>
    /// Takes a physical path-string for an image and converts it into image data
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PathToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string name = (string)value;
                // Output image
                string image_name = "Default.png";
                // This is run again and again for each image, not great. todo; redo with some kind of static array if time
                string path = Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;
                // image path
                string subpath = @"\Assets\Icons\";

                path = Directory.GetParent(path).FullName;

                DirectoryInfo directory = new DirectoryInfo(path);

                var c = System.IO.Path.Join(directory.FullName, subpath);
                var d = Path.Join(c, name + ".png");

                if (File.Exists(d))
                {
                    image_name = name + ".png";
                }


                return new BitmapImage(new Uri($"pack://application:,,,/Assets/Icons/{image_name}"));
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
