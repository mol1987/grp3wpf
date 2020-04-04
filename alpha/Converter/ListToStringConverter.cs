using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace alpha
{ 
    public class ListToStringConverter : IValueConverter
    {
        /// <summary>
        /// List to string conversion for wpf
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Screw Error checks, i dont have time for this

                #region ...................

                //// Check if type is valid
                //if (typeof(Library.TypeLib.Ingredient) != targetType)
                //{
                //    throw new Exception("Wrong input type for conversion");
                //}

                //// Check if type is valid
                //if (typeof(Library.TypeLib.Ingredient) != targetType)
                //{
                //    throw new Exception("Wrong input type for conversion");
                //}
                // cast it
                //ingredient = (Library.TypeLib.Ingredient)value; 
                //var ingredient = new Library.TypeLib.Ingredient();
                #endregion

                var ingredientList = (List<Library.TypeLib.Ingredient>)value;
                string res = "";

                // Join string
                ingredientList.ForEach(a => res += String.Format(", {0}", a.Name));

                return res;
            }
            catch
            {
                return "[Error] invalid list->string converting. Contact system administrator";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

