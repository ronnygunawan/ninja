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
class ICollection<int> {
    void Add(Range range);
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
        select j,

        10..12 // spread range
    }
};

// foo.Items: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
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

### 4. Enumerable Range

Range in foreach:

```cs
// 1 to 5
foreach (int i in 1..5) {
}

// 0 to 5
foreach (int i in ..5) {
}

// 0 to length-1
foreach (int i in ..^length) {
}
```

Range in LINQ:

```cs
// simple LINQ
from i in ..^arr.length
select arr[i]

// complex LINQ: you need to explicitly specify 'int' as range variable type
from int i in ..^arr.length
where i % 2 == 0
select arr[i]
```

### 5. Zip, Skip, and Take using Range

```cs
// i is index of item in list
foreach ((string item, int i) in list.Zip(..)) {
}

// take elements at index 3 to 5
var result = list.Take(3..5);

// take all elements except those at index 3 to 5
var result = list.Skip(3..5);
```

### 6. One&lt;T&gt; and Qlosure&lt;T&gt;

Taken from [ronnygunawan/one](https://github.com/ronnygunawan/one) repo. It allows you to use LINQ expression to write JS closure-like or F# pipeline-like expression:

```cs
decimal total = from decimal price in 199m
                let tax = price * 0.1m
                let subtotal = price + tax
                let discount = 10m
                select subtotal - discount;
```