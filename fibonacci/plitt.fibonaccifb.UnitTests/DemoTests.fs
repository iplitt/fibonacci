module plitt.fibonaccifb.DemoTests

open NUnit.Framework
open Demo

[<SetUp>]
let Setup () =
    ()

[<Test>]
let extractMessages () =
    Demo.extractMessages
