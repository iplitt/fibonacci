namespace Demo

open FSharp.Reflection
open System.Reflection

module Demo =
    
    type Foo =
        static member Bar<'T> (?optionalParam: 'T) =
            printfn "Invoked with parameter %A" optionalParam
            
    let invokeUntyped (value: obj) =
        let t = value.GetType()
        let methodInfo = typeof<Foo>.GetMethod("Bar", BindingFlags.Public ||| BindingFlags.Static).MakeGenericMethod [|t|]
        let optTy = typedefof<_ option>.MakeGenericType [|t|]
        let optCtor = optTy.GetConstructor [|t|]
        let optVal = optCtor.Invoke [|value|]
        methodInfo.Invoke (null, [|optVal|]) :?> unit
    
    type Messages =
        { Xml: string
          RecentXml: string option }

    type MessageResults =
        { XmlResult: Result<string, string>
          RecentXmlResult: Result<string option, string> }

    let getXmlResult = Error "error xml"

    let getRecentXmlResult =
        Ok (Some("error recent xml"))


    let getMessageResults =
        {XmlResult = getXmlResult; RecentXmlResult = getRecentXmlResult}
        
    let extractValueFromNormalResult (result : Result<'T, _>) =
        match result with
        |Ok s -> s
        |Error e -> e
        
    let extractValueFromOptionResult (result : Result<'T option, _>) =
        match result with
        |Ok s -> s
        |Error e -> None
        
    let extractValuesFromResult (results: MessageResults) =
        {Xml = extractValueFromNormalResult(results.XmlResult); RecentXml = extractValueFromOptionResult results.RecentXmlResult}
        
    let extractError (result)(errs) =
        match result with
        |Ok s -> errs
        |Error e -> errs @ [e]
    let extractError2 (result: obj)=
        invokeUntyped result
        List.empty<string>

    let extractErrorsFromResults (results: MessageResults) =
        List.empty<string>
        |> extractError results.XmlResult
        |> extractError results.RecentXmlResult

    let extractErrorsFromResults2 (results: MessageResults) =
        let x = FSharpValue.GetRecordFields results
                |> Array.map extractError2
        List.empty<string>

    let output (messageResults: MessageResults) =
        extractValuesFromResult messageResults, extractErrorsFromResults2 messageResults
        
    let extractMessages =
        let results =
            getMessageResults
            |> output
        match results with
                    | data, [] -> Ok data
                    | _, errs ->  Error errs
            |> printfn "%A"
