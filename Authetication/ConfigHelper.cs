using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using DAL.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Authetication
{

  public class ConfigHelper
  {

    public static Config GetConfig()
    {

      PropertyInfo[] properties = typeof(Config).GetProperties();
      Config config = new Config();

      if (ConfigurationManager.AppSettings.Count > 0)
      {

        Parallel.ForEach(properties, prop =>
        {
          IEnumerable<CustomAttributeData> propAttributes = prop.CustomAttributes;

          CustomAttributeData matchedAttibute = propAttributes.FirstOrDefault(x => x.AttributeType == typeof(DisplayNameAttribute));

          string value = prop.Name;
          if (matchedAttibute != null)
          {
            DisplayNameAttribute attribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(prop, matchedAttibute.AttributeType);
            if (!string.IsNullOrEmpty(attribute.DisplayName))
              value = attribute.DisplayName;
          }

          string result = ConfigurationManager.AppSettings.Get(value);
          if (!string.IsNullOrEmpty(result))
            result = Regex.Unescape(result);

          prop.SetValue(config, result);
        });

      }

      return config;

    }

  }
}
