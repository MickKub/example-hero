using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SpotHero.Common.Extensions
{
	public static class DataFormatExtensions
	{
		public static string ToXml<T>(this T data) where T: class, new ()
		{
			var xsSubmit = new XmlSerializer(typeof(T));
			var xml = string.Empty;

			using (var sww = new StringWriter())
			{
				using (var writer = XmlWriter.Create(sww))
				{
					xsSubmit.Serialize(writer, data);
					xml = sww.ToString();
				}
			}

			return xml;
		}

		public static string ToJson<T>(this T data) where T : class
		{
			return JsonConvert.SerializeObject(data); ;
		}
	}
}
