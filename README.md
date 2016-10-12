# Konves.ChordPro

## Quick Start

Get the NuGet package and then do this:

```CSharp
Document doc;

// Deserialize ChordPro document
using (Stream stream = new FileStream("{file path}", FileMode.Open))
{
    doc = ChordProSerializer.Deserialize(stream);
}

// Do something with the document object model

// Serialize ChordPro document
using (StreamWriter writer = new StreamWriter(new FileStream("{file path}", FileMode.Create)))
{
    ChordProSerializer.Serialize(doc, writer);
}
```

## Custom Directives
Out of the box, this library handles all of the directives defined at
http://www.chordpro.org/. However, there are many tools that define custom
directives that follow a similar `{name: value}` format.  In order to facilitate
custom directives, this library exposes `Directive` and `DirectiveHandler`
abstract classes.  The `Directive` class doesn't define anything and may be
implemented however the developer sees fit.  All of the logic for parsing 
directives lives in the corresponding `DirectiveHandler` implementation.

Let's take a look at the how a few default directives and their respective handlers
are defined.

### Directives without values

The `NewPageDirective` doesn't have a value and thus is written as `{new_page}`
(or in the short form of `{np}`).  The `Directive` implementation is simple:

```CSharp
public sealed class NewPageDirective : Directive
{
}
```

The handler is where the magic happens:

```CSharp
public sealed class NewPageHandler : DirectiveHandler<NewPageDirective>
{
    protected override bool TryCreate(DirectiveComponents components, out Directive directive)
    {
        directive = new NewPageDirective();
        return true;
    }

    public override string LongName { get { return "new_page"; } }
    public override string ShortName { get { return "np"; } }
    public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
    public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
}
```

The type parameter specifies the type of directive that this handler handles.  (Note: although
the `TryCreate` method has an out parameter whose type is an abstract `Directive` class,
the returned value should be the same type as the classes type paremeter.)

The `TryCreate` method defined how the directive is constructed.  In the case of a
valueless directive such as this the logic is trivial.

The `LongName` and `ShortName` properties define the long-form and short-form versions
of the directive.  If `ShortName` is not overridden, it will return the `LongName` by
default.

The `SubKey` and `Value` properties inform the validator on how to handle the presence
of a sub key and/or value.  The options are `Required`, `Optional`, and `NotAllowed`.
(NOTE: a "sub key" is a second string before the "`:`" in a directive.  An example of this
is one form of the `define` directive which can be written: `{define G: chord def here}`
wherein the "sub key" would be the string "`G`".)

Note that all of the four properties are used to validate the directive text before
`TryCreate` is called; therefore, such validation logic does not need to be added.
However, any further validation can be added (eg. requiring the value to be of a 
specific numerical format, etc). A return value of `false` indicates that the custom
validation failed.

## Directives with values

The `ChordSizeDirective` has a value that must be an number and is written as
`{chordsize: 9}` (or in the short form of `{cs: 9}`). The `Directive` implementation
is still fairly simple:

```CSharp
public sealed class ChordSizeDirective : Directive
{
    public ChordSizeDirective(int fontSize)
    {
        FontSize = fontSize;
    }

    public int FontSize { get; set; }
}
```

The handler looks like this:

```CSharp
public sealed class ChordSizeHandler : DirectiveHandler<ChordSizeDirective>
{
    protected override bool TryCreate(DirectiveComponents components, out Directive directive)
    {
        int value;
        if (int.TryParse(components.Value, out value))
        {
            directive = new ChordSizeDirective(value);
            return true;
        }

        directive = null;
        return false;
    }

    protected override string GetValue(Directive directive)
    {
        return (directive as ChordSizeDirective)?.FontSize.ToString();
    }

    public override string LongName { get { return "chordsize"; } }
    public override string ShortName { get { return "cs"; } }
    public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
    public override ComponentPresence Value { get { return ComponentPresence.Required; } }
}
```

Like before we have the long and short names defined in property overrides as well as
the presence of a sub-key (`NotAllowed`) and value (`Required`).  The `TryCreate`
method includes a bit of logic to parse the value as an `integer`.

The other main difference is the `GetValue` override which defines how this handler
converts a `Directive` to a string value.  In all cases, passing in an implementation
of a `Directive` other that the type of the handler's type parameter should return a `null`
value.  In this case, the return value is just the stringified `FontSize`.

## Fairly obvious legal stuff

A license to use and modify this software does not in any way grant the user the same
rights to use and modify any particular piece of music.  Please make sure that you
have the rights to do so.  Observe copyright laws. Don't be evil.

This library is not affiliated with Johan Vromans or any of his official ChordPro
software products.

This page is not legal advice.  If you have any questions about usage of this library
or usage of a piece of music, please seek appropriate councel.  However, if *you*
are a lawyer and want to contribute to this documentation, feel free to submit a
Pull Request :)