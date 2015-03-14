In the admin UI of ScrewTurn Wiki, you can provide a configuration string for plugins. This one supports three configuration options:

## ScriptUrl ##
> The syntax highlighter needs to download some CSS, JavaScript, and Flash files. By default, these files are downloaded from my Dropbox (which makes quite a good poor man’s CDN). If you like to download them from somewhere else, specify the base URL in the ScriptUrl option. The best-performing way to deliver the files will be to download them onto the wiki’s web server and reference them with a relative URL.

## Theme ##
The syntax highlighter offers some color-scheme themes. Pick one with this option; or omit it to use the default one.

## DefaultLang ##
> If no language is specified on a code block, the default language is used. Without specifying this option, no specific language is used and the code appears as plain text; specify the DefaultLang option to use another language as default.

In this example the script files are loaded from a directory on the wiki server with the machine-absolute URL “/syntaxHighlighter/”, the white-on-black Emacs color theme is used, and the default language is C#.
```
ScriptUrl=/syntaxHighlighter/;
Theme=Emacs;
DefaultLang=csharp

```

## CustomLang ##
> The syntax highlighter allows adding new javascript "brush" files in orde to support additional languages that are not included in the standard script library. **If you have not written or imported custom brush definitions, or if you don't know what is meant by that, you do not need to set this option.**

By default, the ScrewTurn plugin does not know about these custom languages. To use additional brush definitions, add a custom language to the configuration string, and add the name of the javascript file that implements it.

In this example, an additional language called "magic" is added. It uses a brush file named "shBrushMagic.js".
```
CustomLang:magic=shBrushMagic.js
```

Note that the custom brush file has to reside in the same directory as the standard brush definition files.

You can specify multiple custom languages, and you can assign different synonyms for the same brush:
```
CustomLang:magic=shBrushMagic.js;
CustomLang:mushroom=shBrushMagic.js;
CustomLang:slippers=shBrushSlippers.js
```
