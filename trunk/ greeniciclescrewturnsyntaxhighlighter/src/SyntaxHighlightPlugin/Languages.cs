using System;
using System.Collections.Generic;

namespace GreenIcicle.Screwturn3SyntaxHighlighter
{
  /// <summary>
  /// Provides the supported programming languages.
  /// </summary>
  public class Languages
  {
    private Dictionary<string, string> m_Brushes = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );

    /// <summary>
    /// Constructor. When a Languages class is created, 
    /// a list of predefined languages ("brushes") is created.
    /// </summary>
    public Languages()
    {
      InitializeBrushes();
    }

    /// <summary>
    /// Returns if a programming language is supported by the formatter.
    /// If there is a brush for it, a language is supported.
    /// </summary>
    /// <param name="language">The programming language name to test.</param>
    /// <returns><c>true</c> if the provided language is supported,
    /// otherwise, <c>false.</c></returns>
    public  bool IsSupported( string language )
    {
      if( string.IsNullOrEmpty( language ) )
      {
        throw new ArgumentNullException( "language");
      }
      string key = language.Trim();
      return m_Brushes.ContainsKey( key );
    }

    /// <summary>
    /// Returns the brush CSS file for a programming language.
    /// </summary>
    /// <param name="language">The programming language name.</param>
    /// <returns>The name of a brush CSS file, or <c>null</c>
    /// if the language is not supported.</returns>
    public string GetStylesheetFile( string language )
    {
      if( string.IsNullOrEmpty( language ) )
      {
        throw new ArgumentNullException( "language");
      }

      string stylesheetFile;
      string key = language.Trim();
      m_Brushes.TryGetValue( key, out stylesheetFile );
      return stylesheetFile;
    }

    /// <summary>
    /// Adds a user-defined language brush to the set of supported languages.
    /// </summary>
    /// <param name="language">The name of the language, as given as the first word in a code block</param>
    /// <param name="stylesheetFile">Name of the additional language ("brush") javascript file
    /// within the syntax hightlighter directory.</param>
    public  void AddLanguage( string language, string stylesheetFile )
    {
      if( string.IsNullOrEmpty( language ) )
      {
        throw new ArgumentNullException( "language" );
      }
      if( string.IsNullOrEmpty( stylesheetFile ) )
      {
        throw new ArgumentNullException( "stylesheetFile" );
      }
      m_Brushes[ language.ToLowerInvariant() ] = stylesheetFile;
    }

    /// <summary>
    /// Initializes the list of supported language names and associates them
    /// with a brush style sheet.
    /// </summary>
    private void InitializeBrushes()
    {
      m_Brushes.Add( "as3", "shBrushAS3.js" );
      m_Brushes.Add( "actionscript3", "shBrushAS3.js" );
      m_Brushes.Add( "bash", "shBrushBash.js" );
      m_Brushes.Add( "shell", "shBrushBash.js" );
      m_Brushes.Add( "cf", "shBrushColdFusion.js" );
      m_Brushes.Add( "coldfusion", "shBrushColdFusion.js" );
      m_Brushes.Add( "c-sharp", "shBrushCSharp.js" );
      m_Brushes.Add( "csharp", "shBrushCSharp.js" );
      m_Brushes.Add( "cpp", "shBrushCpp.js" );
      m_Brushes.Add( "c", "shBrushCpp.js" );
      m_Brushes.Add( "css", "shBrushCss.js" );
      m_Brushes.Add( "delphi", "shBrushDelphi.js" );
      m_Brushes.Add( "pas", "shBrushDelphi.js" );
      m_Brushes.Add( "pascal", "shBrushDelphi.js" );
      m_Brushes.Add( "diff", "shBrushDiff.js" );
      m_Brushes.Add( "patch", "shBrushDiff.js" );
      m_Brushes.Add( "erl", "shBrushErlang.js" );
      m_Brushes.Add( "erlang", "shBrushErlang.js" );
      m_Brushes.Add( "groovy", "shBrushGroovy.js" );
      m_Brushes.Add( "js", "shBrushJScript.js" );
      m_Brushes.Add( "jscript", "shBrushJScript.js" );
      m_Brushes.Add( "javascript", "shBrushJScript.js" );
      m_Brushes.Add( "java", "shBrushJava.js" );
      m_Brushes.Add( "jfx", "shBrushJavaFX.js" );
      m_Brushes.Add( "javafx", "shBrushJavaFX.js" );
      m_Brushes.Add( "pl", "shBrushPerl.js" );
      m_Brushes.Add( "perl", "shBrushPerl.js" );
      m_Brushes.Add( "php", "shBrushPhp.js" );
      m_Brushes.Add( "plain", "shBrushPlain.js" );
      m_Brushes.Add( "text", "shBrushPlain.js" );
      m_Brushes.Add( "ps", "shBrushPowerShell.js" );
      m_Brushes.Add( "powershell", "shBrushPowerShell.js" );
      m_Brushes.Add( "py", "shBrushPython.js" );
      m_Brushes.Add( "python", "shBrushPython.js" );
      m_Brushes.Add( "rails", "shBrushRuby.js" );
      m_Brushes.Add( "ror", "shBrushRuby.js" );
      m_Brushes.Add( "ruby", "shBrushRuby.js" );
      m_Brushes.Add( "scala", "shBrushScala.js" );
      m_Brushes.Add( "sql", "shBrushSql.js" );
      m_Brushes.Add( "vb", "shBrushVb.js" );
      m_Brushes.Add( "vbnet", "shBrushVb.js" );
      m_Brushes.Add( "xml", "shBrushXml.js" );
      m_Brushes.Add( "xhtml", "shBrushXml.js" );
      m_Brushes.Add( "html", "shBrushXml.js" );
      m_Brushes.Add( "xslt", "shBrushXml.js" );
      m_Brushes.Add( "xaml", "shBrushXml.js" );

    }
  }
}
