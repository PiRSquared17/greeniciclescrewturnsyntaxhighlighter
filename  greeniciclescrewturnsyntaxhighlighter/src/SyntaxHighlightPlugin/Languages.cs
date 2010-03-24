using System;
using System.Collections.Generic;

namespace GreenIcicle.Screwturn3SyntaxHighlighter
{
  /// <summary>
  /// Provides the supported programming languages.
  /// </summary>
  public static class Languages
  {
    private static Dictionary<string, string> s_Brushes;

    /// <summary>
    /// Returns if a programming language is supported by the formatter.
    /// If there is a brush for it, a language is supported.
    /// </summary>
    /// <param name="language">The programming language name to test.</param>
    /// <returns><c>true</c> if the provided language is supported,
    /// otherwise, <c>false.</c></returns>
    public static bool IsSupported( string language )
    {
      if( string.IsNullOrEmpty( language ) )
      {
        throw new ArgumentNullException( "language");
      }
      InitializeBrushes();
      string key = language.Trim();
      return s_Brushes.ContainsKey( key );
    }

    /// <summary>
    /// Returns the brush CSS file for a programming language.
    /// </summary>
    /// <param name="language">The programming language name.</param>
    /// <returns>The name of a brush CSS file, or <c>null</c>
    /// if the language is not supported.</returns>
    public static string GetStylesheetFile( string language )
    {
      if( string.IsNullOrEmpty( language ) )
      {
        throw new ArgumentNullException( "language");
      }
      InitializeBrushes();

      string stylesheetFile;
      string key = language.Trim();
      s_Brushes.TryGetValue( key, out stylesheetFile );
      return stylesheetFile;
    }

    /// <summary>
    /// Initializes the list of supported language names and associates them
    /// with a brush style sheet.
    /// </summary>
    private static void InitializeBrushes()
    {
      if( s_Brushes != null ) 
      {
        return;
      }
      s_Brushes = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );

      s_Brushes.Add( "as3", "shBrushAS3.js" );
      s_Brushes.Add( "actionscript3", "shBrushAS3.js" );
      s_Brushes.Add( "bash", "shBrushBash.js" );
      s_Brushes.Add( "shell", "shBrushBash.js" );
      s_Brushes.Add( "cf", "shBrushColdFusion.js" );
      s_Brushes.Add( "coldfusion", "shBrushColdFusion.js" );
      s_Brushes.Add( "c-sharp", "shBrushCSharp.js" );
      s_Brushes.Add( "csharp", "shBrushCSharp.js" );
      s_Brushes.Add( "cpp", "shBrushCpp.js" );
      s_Brushes.Add( "c", "shBrushCpp.js" );
      s_Brushes.Add( "css", "shBrushCss.js" );
      s_Brushes.Add( "delphi", "shBrushDelphi.js" );
      s_Brushes.Add( "pas", "shBrushDelphi.js" );
      s_Brushes.Add( "pascal", "shBrushDelphi.js" );
      s_Brushes.Add( "diff", "shBrushDiff.js" );
      s_Brushes.Add( "patch", "shBrushDiff.js" );
      s_Brushes.Add( "erl", "shBrushErlang.js" );
      s_Brushes.Add( "erlang", "shBrushErlang.js" );
      s_Brushes.Add( "groovy", "shBrushGroovy.js" );
      s_Brushes.Add( "js", "shBrushJScript.js" );
      s_Brushes.Add( "jscript", "shBrushJScript.js" );
      s_Brushes.Add( "javascript", "shBrushJScript.js" );
      s_Brushes.Add( "java", "shBrushJava.js" );
      s_Brushes.Add( "jfx", "shBrushJavaFX.js" );
      s_Brushes.Add( "javafx", "shBrushJavaFX.js" );
      s_Brushes.Add( "pl", "shBrushPerl.js" );
      s_Brushes.Add( "perl", "shBrushPerl.js" );
      s_Brushes.Add( "php", "shBrushPhp.js" );
      s_Brushes.Add( "plain", "shBrushPlain.js" );
      s_Brushes.Add( "text", "shBrushPlain.js" );
      s_Brushes.Add( "ps", "shBrushPowerShell.js" );
      s_Brushes.Add( "powershell", "shBrushPowerShell.js" );
      s_Brushes.Add( "py", "shBrushPython.js" );
      s_Brushes.Add( "python", "shBrushPython.js" );
      s_Brushes.Add( "rails", "shBrushRuby.js" );
      s_Brushes.Add( "ror", "shBrushRuby.js" );
      s_Brushes.Add( "ruby", "shBrushRuby.js" );
      s_Brushes.Add( "scala", "shBrushScala.js" );
      s_Brushes.Add( "sql", "shBrushSql.js" );
      s_Brushes.Add( "vb", "shBrushVb.js" );
      s_Brushes.Add( "vbnet", "shBrushVb.js" );
      s_Brushes.Add( "xml", "shBrushXml.js" );
      s_Brushes.Add( "xhtml", "shBrushXml.js" );
      s_Brushes.Add( "html", "shBrushXml.js" );
      s_Brushes.Add( "xslt", "shBrushXml.js" );
      s_Brushes.Add( "xaml", "shBrushXml.js" );

    }
  }
}
