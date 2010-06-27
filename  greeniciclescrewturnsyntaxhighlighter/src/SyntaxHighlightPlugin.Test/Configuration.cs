using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GreenIcicle.Screwturn3SyntaxHighlighter.Test
{
  [TestFixture( Description = "Tests for setting hte configuration string" )]
  public class Configuration
  {
    SyntaxHighlightFormatProvider m_Tested;

    [SetUp]
    public void Setup()
    {
      m_Tested = new SyntaxHighlightFormatProvider();
    }


    [Test]
    public void CongurationString_Null_DoesNotThrow()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = null;

      // Assert
      // ------------
    }

    [Test]
    public void CongurationString_IncorrectlyFormatted_DoesNotThrow()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "JustSomeString";

      // Assert
      // ------------
    }

    [Test]
    public void InitialState_DefaultLang_IsPlainText()
    {
      // Arrange
      // ------------

      // Act
      // ------------

      // Assert
      // ------------
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( "text" ) );
    }

    [Test]
    public void InitialState_ClientScriptBaseUrl_IsAbsoluteUrl()
    {
      // Arrange
      // ------------

      // Act
      // ------------

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.StringStarting( "http" ) );
    }

    [Test]
    public void InitialState_Theme_IsDefault()
    {
      // Arrange
      // ------------

      // Act
      // ------------

      // Assert
      // ------------
      Assert.That( m_Tested.Theme, Is.EqualTo( "Default" ) );
    }

    #region DefaultLang

    [Test]
    public void DefaultLang_ValidLanguage_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "DefaultLang=csharp";

      // Assert
      // ------------
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( "csharp" ) );
    }

    [Test]
    public void DefaultLang_InvalidLanguage_IsIgnored()
    {
      // Arrange
      // ------------
      string defaultLang = m_Tested.DefaultLanguage;

      // Act
      // ------------
      m_Tested.ConfigurationString = "DefaultLang=unknown";

      // Assert
      // ------------
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( defaultLang ) );
    }

    [Test]
    public void DefaultLang_ValidWithWhitespace_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "  DefaultLang   = \n  csharp  ";

      // Assert
      // ------------
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( "csharp" ) );
    }

    [Test]
    public void DefaultLang_NoValue_IsIgnored()
    {
      // Arrange
      // ------------
      string defaultLang = m_Tested.DefaultLanguage;

      // Act
      // ------------
      m_Tested.ConfigurationString = "DefaultLang=";

      // Assert
      // ------------
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( defaultLang ) );
    }


    [Test]
    public void DefaultLang_NoValueOnlyWhitespace_IsIgnored()
    {
      // Arrange
      // ------------
      string defaultLang = m_Tested.DefaultLanguage;

      // Act
      // ------------
      m_Tested.ConfigurationString = "DefaultLang=  ";

      // Assert
      // ------------
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( defaultLang ) );
    }

    #endregion

    #region CustomLang

    [Test]
    public void CustomLang_CorrectlyFormed_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "CustomLang:magic=shMagic.js";

      // Assert
      // ------------
      Assert.That( m_Tested.Languages.IsSupported( "magic" ), Is.True );
      Assert.That( m_Tested.Languages.GetStylesheetFile( "magic" ), Is.EqualTo( "shMagic.js" ) );
    }

    [Test]
    public void CustomLang_NameDiffersFromScript_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "CustomLang:magic=shSlippers.js";

      // Assert
      // ------------
      Assert.That( m_Tested.Languages.IsSupported( "magic" ), Is.True );
      Assert.That( m_Tested.Languages.GetStylesheetFile( "magic" ), Is.EqualTo( "shSlippers.js" ) );
    }

    [Test]
    public void CustomLang_LanguageIsUndefined_IsIgnored()
    {
      // Arrange
      // ------------
      string defaultLang = m_Tested.DefaultLanguage;

      // Act
      // ------------
      m_Tested.ConfigurationString = "CustomLang:=shMagic.js";

      // Assert
      // ------------
      // No exception has occurred - that's enough
    }

    [Test]
    public void CustomLang_BrushIsUndefined_IsIgnored()
    {
      // Arrange
      // ------------
      string defaultLang = m_Tested.DefaultLanguage;

      // Act
      // ------------
      m_Tested.ConfigurationString = "CustomLang:magic=";

      // Assert
      // ------------
      Assert.That( m_Tested.Languages.IsSupported( "magic" ), Is.False );
    }

    [Test]
    public void CustomLang_BrushIsUndefinedWIthWhitespace_IsIgnored()
    {
      // Arrange
      // ------------
      string defaultLang = m_Tested.DefaultLanguage;

      // Act
      // ------------
      m_Tested.ConfigurationString = "CustomLang:magic=     ";

      // Assert
      // ------------
      Assert.That( m_Tested.Languages.IsSupported( "magic" ), Is.False );
    }

    [Test]
    public void CustomLang_ValidWithWhitespace_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "   CustomLang:   magic     =     shMagic.js   ";

      // Assert
      // ------------
      Assert.That( m_Tested.Languages.IsSupported( "magic" ), Is.True );
      Assert.That( m_Tested.Languages.GetStylesheetFile( "magic" ), Is.EqualTo( "shMagic.js" ) );
    }

    [Test]
    public void CustomLang_LanguageIsAlreadyUsed_OverridesStandardLanguage()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "CustomLang:xml=shMagic.js";

      // Assert
      // ------------
      Assert.That( m_Tested.Languages.IsSupported( "xml" ), Is.True );
      Assert.That( m_Tested.Languages.GetStylesheetFile( "xml" ), Is.EqualTo( "shMagic.js" ) );
    }

    #endregion

    #region Theme
    [Test]
    public void Theme_ValidTheme_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "Theme=Emacs";

      // Assert
      // ------------
      Assert.That( m_Tested.Theme, Is.EqualTo( "Emacs" ) );
    }

    [Test]
    [Ignore( "Currently, themes are not validated" )]
    public void Theme_InvalidTheme_IsIgnored()
    {
      // Arrange
      // ------------
      string theme = m_Tested.Theme;

      // Act
      // ------------
      m_Tested.ConfigurationString = "Theme=unknown";

      // Assert
      // ------------
      Assert.That( m_Tested.Theme, Is.EqualTo( theme ) );
    }

    [Test]
    public void Theme_ValidWithWhitespace_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "  Theme   = \n  Emacs  ";

      // Assert
      // ------------
      Assert.That( m_Tested.Theme, Is.EqualTo( "Emacs" ) );
    }

    [Test]
    public void Theme_NoValue_IsIgnored()
    {
      // Arrange
      // ------------
      string theme = m_Tested.Theme;

      // Act
      // ------------
      m_Tested.ConfigurationString = "Theme=";

      // Assert
      // ------------
      Assert.That( m_Tested.Theme, Is.EqualTo( theme ) );
    }


    [Test]
    public void Theme_NoValueOnlyWhitespace_IsIgnored()
    {
      // Arrange
      // ------------
      string theme = m_Tested.Theme;

      // Act
      // ------------
      m_Tested.ConfigurationString = "Theme=  ";

      // Assert
      // ------------
      Assert.That( m_Tested.Theme, Is.EqualTo( theme ) );
    }
    #endregion

    #region ClientScriptBaseUrl
    [Test]
    public void ScriptUrl_AbsoluteUrl_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "ScriptUrl=http://greenicicleblog.com/";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( "http://greenicicleblog.com/" ) );
    }

    [Test]
    public void ScriptUrl_RelativeUrl_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "ScriptUrl=/sh/";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( "/sh/" ) );
    }

    [Test]
    public void ScriptUrl_MissingTrailingSlash_TrailingSlashIsAdded()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "ScriptUrl=/sh";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( "/sh/" ) );
    }

    [Test]
    public void ScriptUrl_UrlWithWhitespace_IsApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "  ScriptUrl   = \n  /sh/  ";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( "/sh/" ) );
    }

    [Test]
    public void ScriptUrl_NoValue_IsIgnored()
    {
      // Arrange
      // ------------
      string url = m_Tested.ClientScriptBaseUrl;

      // Act
      // ------------
      m_Tested.ConfigurationString = "ScriptUrl=";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( url ) );
    }


    [Test]
    public void ScriptUrl_NoValueOnlyWhitespace_IsIgnored()
    {
      // Arrange
      // ------------
      string url = m_Tested.ClientScriptBaseUrl;

      // Act
      // ------------
      m_Tested.ConfigurationString = "ScriptUrl=  ";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( url ) );
    }
    #endregion

    [Test]
    public void MultipleParams_ValidValues_AreApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "ScriptUrl=/sh/;CustomLang:magic=shMagic.js;Theme=Emacs;DefaultLang=csharp";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( "/sh/" ) );
      Assert.That( m_Tested.Theme, Is.EqualTo( "Emacs" ) );
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( "csharp" ) );
      Assert.That( m_Tested.Languages.GetStylesheetFile( "magic" ), Is.EqualTo( "shMagic.js" ) );
    }

    [Test]
    public void MultipleParams_WithTooManySemicolons_AreApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = ";ScriptUrl=/sh/;;Theme=Emacs;;;DefaultLang=csharp;;";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( "/sh/" ) );
      Assert.That( m_Tested.Theme, Is.EqualTo( "Emacs" ) );
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( "csharp" ) );
    }

    [Test]
    public void MultipleParams_WithWhitespace_AreApplied()
    {
      // Arrange
      // ------------

      // Act
      // ------------
      m_Tested.ConfigurationString = "ScriptUrl=/sh/  ;\n    Theme=Emacs   ;  \n\r \t   DefaultLang=csharp;   ";

      // Assert
      // ------------
      Assert.That( m_Tested.ClientScriptBaseUrl, Is.EqualTo( "/sh/" ) );
      Assert.That( m_Tested.Theme, Is.EqualTo( "Emacs" ) );
      Assert.That( m_Tested.DefaultLanguage, Is.EqualTo( "csharp" ) );
    }
  }
}

