module Test

open Feliz
open Feliz.Bulma
open System

open FsSpreadsheet

type FsCell with
    
    /// <summary>
    /// Gets the cell's value converted to the T type.
    /// <para>FsSpreadsheet will try to convert the current value to type 'T.</para>
    /// <para>An exception will be thrown if the current value cannot be converted to the T type.</para>
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    /// <exception cref="ArgumentException"></exception>
    member inline self.GetValueAs<'T>() =
        match (typeof<'T>) with
        | t when t = typeof<string>           -> self.Value |> box
        | t when t = typeof<bool>             -> bool.Parse (self.Value) |> box
        | t when t = typeof<float>            -> Double.Parse (self.Value) |> box
        | t when t = typeof<int>              -> Int32.Parse (self.Value) |> box
        
        | t when t = typeof<int16>            -> Int16.Parse (self.Value) |> box
        | t when t = typeof<int64>            -> Int64.Parse (self.Value) |> box
        
        | t when t = typeof<uint>             -> UInt32.Parse (self.Value) |> box
        | t when t = typeof<uint16>           -> UInt16.Parse (self.Value) |> box
        | t when t = typeof<uint64>           -> UInt64.Parse (self.Value) |> box

        | t when t = typeof<single>           -> Single.Parse (self.Value) |> box
        | t when t = typeof<decimal>          -> Decimal.Parse (self.Value) |> box
        | t when t = typeof<Guid>             -> Guid.Parse (self.Value) |> box
        | t when t = typeof<char>             -> Char.Parse (self.Value) |> box
        | t when t = typeof<DateTime>         -> DateTime.Parse (self.Value) |> box
        | t -> failwith $"FsCell with value {self.Value} cannot be parsed to {typeof<double>.Name}."
        
        :?> 'T

let mycell = FsCell(true)
let book = new FsWorkbook()

open FsSpreadsheet.DSL

let dslTest = cell {
    1
}

let Main() =
    Bulma.box [
        prop.children [
            Bulma.button.a [
                prop.text "Click me!"
                //prop.onClick(fun _ -> Browser.Dom.console.log(mycell.GetValueAs<bool>()))
                prop.onClick(fun _ -> Browser.Dom.console.log(dslTest))
                //prop.onClick(fun _ -> Browser.Dom.console.log(""))
            ]
        ]
    ]