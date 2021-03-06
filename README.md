# RG.Ninja

[![NuGet](https://img.shields.io/nuget/v/RG.Ninja.svg)](https://www.nuget.org/packages/RG.Ninja/) [![.NET](https://github.com/ronnygunawan/ninja/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ronnygunawan/ninja/actions/workflows/dotnet.yml)

## List of Available Jutsu:
### 1. Fluent everything

```cs
obj.Setup(o => { ... });
obj.SetupAsync(async o => { ... });
obj.Let(o => { ... return something; });
obj.LetAsync(async o => { ... return something; });
```

Example:

```cs
var button = new Button {
  Text = "Click me"
}
.Setup(btn => Grid.SetRow(btn, 1))
.Setup(btn => btn.Click += ClickMe_Clicked);
```

### 2. Spread operation in collection initializer

You can 'spread' IEnumerable and Range in collection initializer:

```cs
var obj = new Foo { Items = { someEnumerable } };
var obj = new Foo { Items = { 0..n } };
```

Example:

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

        10..13 // spread range
    }
};

// foo.Items: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
```

### 3. Spread operation in dictionary initializer

```cs
var obj = new Foo { Dictionary = { someDictionary } };
var obj = new Foo { Dictionary = { someCollection, item => item.Id } }; // with key selector
var obj = new Foo { Dictionary = { someCollection, item => item.Id, item => item.Text } }; // with key and value selector
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
// 1 to 4
foreach (int i in 1..5) {
}

// 0 to 4
foreach (int i in ..5) {
}

// 0 to length-1
foreach (int i in ..length) {
}
```

Range in LINQ:

```cs
// simple LINQ
from i in ..arr.length
select arr[i]

// complex LINQ: you need to explicitly specify 'int' as range variable type
from int i in ..arr.length
where i % 2 == 0
select arr[i]
```

### 5. Zip, Skip, and Take using Range

```cs
// i is index of item in list
foreach ((string item, int i) in list.Zip(..)) {
}

// take elements at index 3 and 4
var result = list.Take(3..5);

// take all elements except those at index 3 and 4
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
