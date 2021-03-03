# RG.Ninja

[![NuGet](https://img.shields.io/nuget/v/RG.Ninja.svg)](https://www.nuget.org/packages/RG.Ninja/) [![.NET](https://github.com/ronnygunawan/ninja/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ronnygunawan/ninja/actions/workflows/dotnet.yml)

## List of Available Jutsu:
### 1. Fluent anything

Extension methods:

```cs
class T {
    T Setup<T>(Action<T> setupAction);
    Task<T> SetupAsync<T>(Func<T, Task> asyncSetupAction);
    TResult Let<T, TResult>(Func<T, TResult> selector);
    Task<TResult> LetAsync(Func<T, Task<TResult>> asyncSelector);
}
```

Usage:

```cs
var button = new Button {
  Text = "Click me"
}
.Setup(btn => Grid.SetRow(btn, 1))
.Setup(btn => btn.Click += ClickMe_Clicked);
```

### 2. Spread operation in collection initializer

Extension methods:

```cs
class ICollection<T> {
    void Add(IEnumerable<T> items);
}
```

Usage:

```cs
record Foo {
    public List<int> Items { get; } = new();
}

var items = new[] { 1, 2, 3 };

var foo = new Foo {
    Items = {
        items, // spread array

        4, 5, 6, // add values normally

        from i in items // spread IEnumerable
        let j = 10 - i
        orderby j ascending
        select j
    }
};

// foo.Items: 1, 2, 3, 4, 5, 6, 7, 8, 9
```

### 3. Spread operation in dictionary initializer

Extension methods:

```cs
class IDictionary<TKey, TValue> {
    void Add(IEnumerable<KeyValuePair<TKey, TValue>> items);
    void Add(IEnumerable<TValue> values, Func<TValue, TKey> keySelector);
    void Add<TSource>(IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector);
}
```

Usage:

```cs
record Foo {
    public Dictionary<int, string> Items { get; } = new();
}

var items = new Dictionary<int, string> {
    { 1, "Satu" },
    { 2, "Dua" }
};

var values = new[] { "Tiga", "Empat" };

var foo = new Foo {
    Items = {
        { items }, // spread dictionary

        { values, v => v.Length - 1 }, // spread array using keySelector

        { 5, "Lima" } // add entry normally
    }
};

// foo.Items:
// [1] = "Satu"
// [2] = "Dua"
// [3] = "Tiga"
// [4] = "Empat"
// [5] = "Lima"
```