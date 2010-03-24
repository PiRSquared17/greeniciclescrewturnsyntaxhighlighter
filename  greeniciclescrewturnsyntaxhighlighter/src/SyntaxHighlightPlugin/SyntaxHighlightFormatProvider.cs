using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrewTurn.Wiki.PluginFramework;
using System.Text.RegularExpressions;
using System.Reflection;

namespace GreenIcicle.Screwturn3SyntaxHighlighter
{

  /// <summary>
  /// Adds syntax highlighting to ScrewTurn wiki v3.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This formatter addes the client side syntax highlighter by 
  /// Alex Gorbatchev to source code blocks within a wiki page.
  /// It supports defining a default language if no language is defined on a 
  /// code block.
  /// </para>
  /// <example>
  /// Example for a code block that defines a programming language for highlighting,
  /// in wiki markup:
  /// <code>
  /// @@ csharp
  /// public const string Greeting="huhu"; 
  /// @@
  /// </code>
  /// </example>
  /// </remarks>
  public class SyntaxHighlightFormatProvider : IFormatterProviderV30
  {
    private string m_ConfigurationString;
    private string m_ClientScriptBaseUrl;

    /// <summary>
    /// Creates a new instance of the <see cref="SyntaxHighlightFormatProvider"/>
    /// </summary>
    public SyntaxHighlightFormatProvider()
    {
      // Set default values
      DefaultLanguage = "text";
      Theme = "Default";
    }

    /// <summary>
    /// If no other URL for the client-side scripts is proided via the configuration, 
    /// this base URL is used.
    /// </summary>
    /// <remarks>
    /// By default, the plugin author's DropBox is used.
    /// </remarks>
    /// <value>http://dl.dropbox.com/u/1900064/GreenIcicle/ScrewTurnSyntaxHighlighter/sh2.1.364/</value>
    public const string DefaultClientScriptBaseUrl = "http://dl.dropbox.com/u/1900064/GreenIcicle/ScrewTurnSyntaxHighlighter/sh2.1.364/";

    /// <summary>
    /// Opening tag of a code block as the default formatter returns it: a simple 
    /// preformatted text element.
    /// </summary>
    protected internal const string CodeBlockTag = "<pre>";

    /// <summary>
    /// String passed into this plugin for holding configuration.
    /// </summary>
    protected internal string ConfigurationString
    {
      get
      {
        return m_ConfigurationString;
      }
      set
      {
        m_ConfigurationString = value;

        // Parse the configuration string. It should be a list of key/val pairs,
        // separated by semicolons.
        if (!string.IsNullOrEmpty( m_ConfigurationString ))
        {
          foreach (string option in m_ConfigurationString.Split( ';' ))
          {
            string[] parts = option.Split( '=' );
            if (parts.Length == 2)
            {
              string key = parts[ 0 ].ToUpperInvariant().Trim();
              string val = parts[ 1 ].Trim();

              if (!string.IsNullOrEmpty( key ) && !string.IsNullOrEmpty( val ))
              {
                switch (key)
                {
                  case "SCRIPTURL":
                    ClientScriptBaseUrl = val;
                    break;

                  case "THEME":
                    Theme = val;
                    break;

                  case "DEFAULTLANG":
                    if (Languages.IsSupported( val ))
                    {
                      DefaultLanguage = val;
                    }
                    break;
                }
              }
            }
          }
        }

      }
    }

    /// <summary>
    /// The URL of a directory that stores the different client-side
    /// JavaScript and CSS files.
    /// </summary>
    /// <remarks>
    /// This URL is defined in the plugin configuration by providing a value for
    /// "ScriptUrl". If this value is not provided, <see cref="DefaultClientScriptBaseUrl"/>
    /// is returned.
    /// </remarks> 
    protected internal string ClientScriptBaseUrl
    {
      get
      {
        string baseUrl = m_ClientScriptBaseUrl ?? DefaultClientScriptBaseUrl;
        if (!baseUrl.EndsWith( "/" ))
        {
          baseUrl = baseUrl + "/";
        }
        return baseUrl;
      }
      set
      {
        m_ClientScriptBaseUrl = value;
      }
    }

    /// <summary>
    /// Programming language used when no language is defined on a code block.
    /// </summary>
    protected internal string DefaultLanguage
    {
      get;
      set;
    }

    /// <summary>
    /// Theme - that is: color scheme - of the formatted code.
    /// </summary>
    protected internal string Theme
    {
      get;
      set;
    }

    /// <summary>
    /// This formatter kicks in at phase 2
    /// </summary>
    public bool PerformPhase1
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// This formatter kicks in at phase 2
    /// </summary>
    public bool PerformPhase2
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    /// This formatter kicks in at phase 2
    /// </summary>
    public bool PerformPhase3
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// The execution priority is not so much of concern; 50 is just in the middle.
    /// </summary>
    public int ExecutionPriority
    {
      get
      {
        return 50;
      }
    }

    /// <summary>
    /// Appends the reference to a CSS style sheet to the formatted text.
    /// </summary>
    /// <param name="textBuilder"><see cref="StringBuilder"/> that creates the formatted text.</param>
    /// <param name="styleSheetName">File name of the style sheet.</param>
    protected internal virtual void AppendStyleSheet(StringBuilder textBuilder, string styleSheetName)
    {
      textBuilder.Append( "<link href='" );
      textBuilder.Append( ClientScriptBaseUrl );
      textBuilder.Append( "styles/" );
      textBuilder.Append( styleSheetName );
      textBuilder.Append( "' rel='stylesheet' type='text/css'/>\n" );
    }

    /// <summary>
    /// Appends the reference to a client-side script to the formatted text.
    /// </summary>
    /// <param name="textBuilder"><see cref="StringBuilder"/> that creates the formatted text.</param>
    /// <param name="scriptName">File name of the script.</param>
    protected internal virtual void AppendClientScript(StringBuilder textBuilder, string scriptName)
    {
      textBuilder.Append( "<script src='" );
      textBuilder.Append( ClientScriptBaseUrl );
      textBuilder.Append( "scripts/" );
      textBuilder.Append( scriptName );
      textBuilder.Append( "' type='text/javascript'></script>\n" );
    }

    /// <summary>
    /// Appends the reference to a programming language specific
    /// script ("brush") to the formatted text
    /// </summary>
    /// <param name="textBuilder"><see cref="StringBuilder"/> that creates the formatted text.</param>
    /// <param name="language">The language.</param>
    protected internal virtual void AppendBrushScript(StringBuilder textBuilder, string language)
    {
      string scriptFile = Languages.GetStylesheetFile( language );
      AppendClientScript( textBuilder, scriptFile );
    }

    /// <summary>
    /// Appends the reference to the theme-specific style sheet.
    /// </summary>
    /// <param name="textBuilder"><see cref="StringBuilder"/> that creates the formatted text.</param>
    protected internal virtual void AppendThemeStylesheet(StringBuilder textBuilder)
    {
      string stylesheet = string.Format( "shTheme{0}.css", Theme );
      AppendStyleSheet( textBuilder, stylesheet );
    }

    /// <summary>
    /// Called by the wiki engine in order to format text.
    /// </summary>
    /// <param name="raw">The unformatted text.</param>
    /// <param name="context">Contextual information for the transformation - 
    /// like the user name, HTTP context, or purpose of the formatter run.</param>
    /// <param name="phase">The formatting phase (1,2,3) - see the ScrewTurn wiki documentation
    /// for formatter plugins for details. This formatter exclusively runs in phase 2.</param>
    /// <returns>Formatted text.</returns>
    /// <remarks>
    /// <para>
    /// This formatter detects blocks of source code. The first word within a source code 
    /// block is considered a hint on in which language the block is - if this first word 
    /// is one of the supported languages. 
    /// </para>
    /// <para>
    /// ScrewTurn formats code blocks with a "pre" tag - this is done in phase 1 of the transformation;
    /// this is why we run in phase 2. THe Syntax Highlighter scripts also use the "pre" tag,
    /// augmented with a CSS class that defines the language ("brush", as it cals it). 
    /// Now all we need to do is:<br/>
    /// * Look for "pre" tags
    /// * Get the first word behind it.
    /// * If this first word is a suported language, use it; otherwise ignore and use the
    /// default language.
    /// * Add the CSS class for the brush.
    /// to the "pre" tag.
    /// * Da capo al fine.
    /// * Inject links to the Syntax Highlighter CSS and script files. Each language
    /// has its own CSS file; in order to make things more efficient we only add files 
    /// that are actually needed because the language was found in a script block.
    /// </para>
    /// </remarks>
    public virtual string Format(string raw, ContextInformation context, FormattingPhase phase)
    {
      // This formatter exclusively runs in phase 2.
      if (phase != FormattingPhase.Phase2)
      {
        return raw;
      }

      // Prepare a list of all programming language code blocks found in the page.
      // This is later used to include only the client-side scripts that are required.
      IList<string> foundLanguages = new List<string>();

      // Buckets for the remainders of unformatted text, and already formatted text.
      string sourceText = raw;
      StringBuilder targetText = new StringBuilder();

      // Find any part of the unformatted text that is enclosed in in a "pre" tags.
      // ScrewTurn formats code blocks into preformatted HTML tags.
      string openingTag = @"<pre>";
      string closingTag = @"</pre>";
      Regex regex = new Regex( openingTag + ".+?" + closingTag, RegexOptions.IgnoreCase | RegexOptions.Singleline );
      Match match = regex.Match( sourceText );
      while (match.Success)
      {
        // Push the text before the found code block into the target text
        // without alteration
        if (match.Index > 0)
        {
          targetText.Append( sourceText.Substring( 0, match.Index ) );
        }

        // Remove the part before the found code block, and the code block, from the remaining 
        // source text
        sourceText = sourceText.Substring( match.Index + match.Length );

        // Get the content of the found code block
        string content = match.Value;

        // The RegEx match still contains the opening and closing tags. Remove them so we get only the 
        // text within the tag.
        int openingTagLen = openingTag.Length;
        int closingTagLen = closingTag.Length;
        int contentLen = content.Length - closingTagLen - openingTagLen;
        content = content.Substring( openingTagLen, contentLen );

        // Get the first word of the code block. If it matched one of the highlighter 
        // languages, use it as a hint on how to format the code block and remove it from the document.
        var wordSeparators = new char[] { ' ', '\n', '\r' };
        string firstWord = content
          .Split( wordSeparators, StringSplitOptions.RemoveEmptyEntries )
          .FirstOrDefault();

        // If a first word could be extracted (the block can as well be empty...),
        // and the language is supported, then...
        string language;
        if (!string.IsNullOrEmpty( firstWord ) && Languages.IsSupported( firstWord ))
        {
          // ... set the language for this block...
          language = firstWord;

          // ... and remove the first word from the code block content.
          int firstWordIndex = content.IndexOf( firstWord );
          content = content.Substring( firstWordIndex + firstWord.Length );
        }
        else
        {
          // If no langauge could be found, use the default language.
          language = DefaultLanguage;
        }

        // Track the languages found on the page so we can include the correct set of script files.
        if (!foundLanguages.Contains( language ))
        {
          foundLanguages.Add( language );
        }

        // Add an opening "pre" tag with a language ("brush") definition...
        targetText.AppendFormat(
          "<pre class='brush: {0}'>\n",
          language.ToLowerInvariant() );
        // ... the content...
        targetText.Append( content );
        // ... and a closing tag.
        targetText.Append( "</pre>" );

        // Get the next code block.
        match = regex.Match( sourceText );
      }

      // Append rest of source text to target.
      targetText.Append( sourceText );

      // If any formatted code blocks have been found, inject the highlighter scripts and style sheets.
      if (foundLanguages.Any())
      {
        targetText.AppendLine( "\n<!-- START GreenIcicle code syntax highlighter -->\n" );
        AppendStyleSheet( targetText, "shCore.css" );
        AppendThemeStylesheet( targetText );
        AppendClientScript( targetText, "shCore.js" );
        foreach (var language in foundLanguages)
        {
          AppendBrushScript( targetText, language );
        }
        // Add script that hooks up the Flash-based clipboard helper, and the activate the 
        // syntax highlighter.
        targetText.Append( "<script language='javascript'>\nSyntaxHighlighter.config.clipboardSwf = '" );
        targetText.Append( ClientScriptBaseUrl );
        targetText.Append( "scripts/clipboard.swf'\nSyntaxHighlighter.all();\n</script>" );

        targetText.AppendLine( "\n<!-- END GreenIcicle code syntax highlighter -->\n" );
      }

      // Return the formatted text.
      return targetText.ToString();
    }



    /// <summary>
    /// Formats the page title - 
    /// the title is not modified by this plugin.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="context"></param>
    /// <returns>The original title.</returns>
    public string PrepareTitle(string title, ContextInformation context)
    {
      return title;
    }

    /// <summary>
    /// Initializes the plugin. This is the very first method called on the 
    /// class.
    /// </summary>
    /// <param name="host">Provides access to the wiki's API</param>
    /// <param name="config">Configuration string for the plugin.</param>
    public void Init(IHostV30 host, string config)
    {
      ConfigurationString = config;
    }

    /// <summary>
    /// Shuts the plugin down. Very last method called on the clas.
    /// </summary>
    public void Shutdown()
    {
      // Nothing to do to shut the formatter down
    }

    /// <summary>
    /// Provides information on the plugin to the wiki's administrative UI.
    /// </summary>
    public virtual ComponentInformation Information
    {
      get
      {
        return new ComponentInformation(
          "GreenIcicle Syntax Highlighter",
          "Christian Heger",
          "1.0.2",
          "http://greenicicleblog.com/ScrewTurnSyntaxHighlighter",
          "http://dl.dropbox.com/u/1900064/GreenIcicle/ScrewTurnSyntaxHighlighter/UpdateInfo.txt"
          );
      }
    }

    /// <summary>
    /// HTML text displayed as help for configuring the plugin
    /// </summary>
    public virtual string ConfigHelpHtml
    {
      get
      {
        return @"
<div>
  <b>Options:</b><br/>
  <ul>
  <li><b>ScriptUrl</b> The syntax highlighter needs to download some CSS, JavaScript, and Flash files. By default, these files are downloaded from my Dropbox (which makes quite a good poor man's CDN). If you like to download them from somewhere else, specify the base URL in the ScriptUrl option. The best-performing way to deliver the files will be to download them onto the wiki's web server and reference them with a relative URL.</li>
	<li><b>Theme</b> The syntax highlighter offers some color-scheme themes. Pick one with this option; or omit it to use the default one.</li>
	<li><b>DefaultLang</b> If no language is specified on a code block, the default language is used. Without specifying this option, no specific language is used and the code appears as plain text; specify the DefaultLang option to use another language as default.</li> 
  </ul>
  <b>Example:</b><br/>
  Load the scripts and style sheets from a local directory, and use C# as standard language.
  ScriptUrl=/sh; DefaultLang=csharp
</div>
      ";
      }
    }
  }
}
