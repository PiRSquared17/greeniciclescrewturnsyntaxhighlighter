# Adding formatted code to wiki pages #

The markup for code in ScrewTurn Wiki is:
```
Text before the code
@@
// Your source code goes here
@@
Text after the code
```

In order to support wikis that already contain code following that syntax (and especially for one large project with 500+ pages…) this syntax is supported. However, it does not indicate which language the code is in, so that the formatter defaults to plain text; it still provides the line numbers and fancy background.

To indicate the language, add the name of the programming language as the first word of the code block. The name of the language can be any of the [syntaxes supported by the SyntaxHighlighter](http://alexgorbatchev.com/wiki/SyntaxHighlighter:Brushes). A single page in the wiki can contain code blocks in several programming languages.

```
@@ csharp
// Comment
public string Greeting = "huhu";
@@

Text between code blocks

@@ xml
<!-- Comment -->
<node>content</node>
@@
```