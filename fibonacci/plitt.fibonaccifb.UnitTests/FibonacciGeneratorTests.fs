module plitt.fibonaccifb.FibonacciGeneratorTests

open NUnit.Framework
open FibonacciGenerator

[<SetUp>]
let Setup () =
    ()

[<TestCase(0,0f)>]
[<TestCase(1,1f)>]
[<TestCase(49, 7778742049f)>]
[<TestCase(50, 12586269025f)>]
let generate_producesExpectedResults (n, expected) =
    let fib = FibonacciGenerator()
    Assert.That(fib.generate n, Is.EqualTo(expected))
