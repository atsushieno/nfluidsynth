using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

public class Generator
{
	public static void Main ()
	{
		var doc = XDocument.Load ("props.xml");
		var props = new Dictionary<string,Dictionary<string,string>> ();
		var cur = new Dictionary<string,string> ();
		foreach (var tr in doc.Root.Elements ("table").SelectMany (t => t.Elements ("tr"))) {
			var tds = tr.Elements ("td").ToArray ();
			var name = tds.First ().Value;
			if (name.Length > 0) {
				cur = new Dictionary<string, string> ();
				props [name] = cur;
			}
			cur [tds [1].Value] = tds [2].Value;
		}
		//foreach (var prop in props)
		//	Console.WriteLine ("{0} - {1}", prop.Key,
		//		string.Join ("\n", prop.Value.Select (p => p.Key + "=" + p.Value).ToArray ()));
		
		
		string template = @"using System;

namespace NFluidsynth
{
	public static class ConfigurationKeys
	{
{LIST}
	}
}
";
		
		using (var tw = File.CreateText ("ConfigurationKeys.cs"))
			tw.WriteLine (template.Replace ("{LIST}", string.Join ("\n", props.Keys.Select (
					k => "\t\tpublic const string " + string.Concat (
						k.Split (new char [] {'.', '-'})
							.Select (e => char.ToUpper (e [0]) + e.Substring (1))) + " = \"" + k + "\";"))));
	}
}
