namespace plitt.fibonaccifb

module FibonacciGenerator =

    type FibonacciGenerator() =
        let mutable map = Map.empty<int, int64>

        member this.generate n : int64 =
            match n with
            | 0
            | 1 -> n
            | _ ->
                match map.ContainsKey n with
                | true -> map[n]
                | false ->
                    let value =
                        this.generate (n - 1) + this.generate (n - 2)

                    map <- map.Add(n, value)
                    value
