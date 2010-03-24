using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ScrewTurn.Wiki.PluginFramework;

namespace GreenIcicle.Screwturn3SyntaxHighlighter
{
  [TestFixture( Description = "Tests for the formatting functions" )]
  public class Formatting
  {
    SyntaxHighlightFormatProvider m_Tested;

    [SetUp]
    public void Setup()
    {
      m_Tested = new SyntaxHighlightFormatProvider();
    }

    [Test]
    public void Language_DefaultLang_BrushIsCorrectlySet()
    {
      // Arrange
      // ------------
      string raw = @"<pre>Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( @"<pre class='brush: text'>" ) );
    }

    [Test]
    public void Language_DefinedLang_BrushIsCorrectlySet()
    {
      // Arrange
      // ------------
      string raw = @"<pre> Csharp Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( @"<pre class='brush: csharp'>" ) );
    }

  

    [Test]
    public void Language_DefinedLang_LanguageIsRemovedFromCode()
    {
      // Arrange
      // ------------
      string raw = @"<pre> Csharp Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.Not.StringContaining( "Csharp" ) );
    }

    [Test]
    public void Language_IncorrectLang_DefaultLangIsUsed()
    {
      // Arrange
      // ------------
      string raw = @"<pre> Unknown Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( @"<pre class='brush: text'>" ) );
    }

    [Test]
    public void Language_IncorrectLang_LanguageIsNotRemovedFromCode()
    {
      // Arrange
      // ------------
      string raw = @"<pre> Unknown Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "Unknown" ) );
    }

    [Test]
    public void Language_InWeirdCasing_BrushIsCorrectlySet()
    {
      // Arrange
      // ------------
      string raw = @"<pre>CSHARP Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( @"<pre class='brush: csharp'>" ) );
    }

    [Test]
    public void Language_WithWhitespace_BrushIsCorrectlySet()
    {
      // Arrange
      // ------------
      string raw = @"<pre>
                    csharp 
                    
                        Code</pre>";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( @"<pre class='brush: csharp'>" ) );
    }

    [Test]
    public void LoadScipts_CoreJavaScript_IsLoaded()
    {
      // Arrange
      // ------------
      string raw = @"<pre>Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "scripts/shCore.js" ) );
    }

    [Test]
    public void LoadScipts_CoreCss_IsLoaded()
    {
      // Arrange
      // ------------
      string raw = @"<pre>Code</pre>";


      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "styles/shCore.css" ) );
    }

    [Test]
    public void LoadScipts_ThemeCss_IsLoaded()
    {
      // Arrange
      // ------------
      string raw = @"<pre>Code</pre>";
      m_Tested.Theme = "Emacs";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "styles/shThemeEmacs.css" ) );
    }

    [Test]
    public void LoadScipts_BrushJavaScript_IsLoaded()
    {
      // Arrange
      // ------------
      string raw = @"<pre> csharp Code</pre>";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "scripts/shBrushCSharp.js" ) );
    }

    [Test]
    public void LoadScipts_MultipleCodeBlocks_IsLoaded()
    {
      // Arrange
      // ------------
      string raw = @"<pre> csharp Code</pre><pre> xml Code</pre><pre> java Code</pre>";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "scripts/shBrushCSharp.js" ) );
      Assert.That( result, Is.StringContaining( "scripts/shBrushJava.js" ) );
      Assert.That( result, Is.StringContaining( "scripts/shBrushXml.js" ) );
    }

    [Test]
    public void Text_BeforeCode_IsPreserved()
    {
      // Arrange
      // ------------
      string raw = @"Text Before Code<pre>Code</pre>";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "Text Before Code" ) );
    }

    [Test]
    public void Text_AfterCode_IsPreserved()
    {
      // Arrange
      // ------------
      string raw = @"<pre>Code</pre>Text After Code";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "Text After Code" ) );
    }

    [Test]
    public void Text_BetweenCode_IsPreserved()
    {
      // Arrange
      // ------------
      string raw = @"<pre>Code</pre>Text Between Code<pre>Code</pre>";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "Text Between Code" ) );
    }

    [Test]
    public void Text_InCode_IsPreserved()
    {
      // Arrange
      // ------------
      string raw = @"<pre>Some Text In Code</pre>";

      // Act
      // ------------
      string result = m_Tested.Format( raw, null, FormattingPhase.Phase2 );

      // Assert
      // ------------
      Assert.That( result, Is.StringContaining( "Some Text In Code" ) );
    }

    [Test]
    public void Title_AnyText_IsEchoed()
    {
      // Arrange
      // ------------
      string title = @"Any Title";

      // Act
      // ------------
      string result = m_Tested.PrepareTitle( title, null );

      // Assert
      // ------------
      Assert.That( result, Is.EqualTo( "Any Title" ) );
    }
  }
}
