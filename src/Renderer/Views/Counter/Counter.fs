[<RequireQualifiedAccess>]
module Counter

open Fable.Import.React
open Elmish.React

type Model =
    { Count : int
      IsAlertVisible : bool }

type Message =
    | Increase
    | Decrease
    | HideAlert
    | ShowAlert

let init () =
    { Count = 42
      IsAlertVisible = true }

let update (msg : Message) (model : Model) =
    match msg with
    | Increase -> { model with Count = model.Count + 1 }
    | Decrease -> { model with Count = model.Count - 1 }
    | HideAlert -> { model with IsAlertVisible = false }
    | ShowAlert -> { model with IsAlertVisible = true }

module R = Fable.Helpers.React
module RP = Fable.Helpers.React.Props
module RS = FsReactstrap
module RSP = FsReactstrap.Props

let increase (dispatch : Message -> unit) (event : MouseEvent) : unit =
    printfn "We do an increase"
    dispatch Increase

let counterView =
    lazyView (fun model -> RS.badge [ RSP.Color(RSP.Dark); RSP.Pill true ] [ R.str (sprintf "%i" model.Count) ])

let view (model : Model) (dispatch : Message -> unit) =
    R.section [] [
        RS.alert
            [ RSP.IsOpen model.IsAlertVisible
              RSP.Color(RSP.Dark)
              RSP.Toggle (fun _ -> dispatch HideAlert) ] [
            R.h4 [ RP.ClassName "alert-heading" ] [
                R.str "Hi, there! "
                RS.badge [ RSP.Color(RSP.Light) ] [ R.str "1337" ]
            ]
            R.hr []
            R.str "This is a warning with "
            R.a [ RP.Href "#"; RP.ClassName "alert-link" ] [ R.str "a link"]
            R.str " to click on!"
        ]
        R.h1 [] [
            R.str "The count is "
            counterView model
        ]
        R.br []
        RS.button [ RP.OnClick (increase dispatch); RSP.Color(RSP.Light) ] [ R.str "Increase" ]
        R.span [] [ R.str " "]
        RS.button [ RP.OnClick (fun _ -> dispatch Decrease); RSP.Color(RSP.Dark) ] [ R.str "Decrease" ]
        R.span [] [ R.str " "]
        RS.button [ RP.OnClick (fun _ -> dispatch ShowAlert) ; RSP.Color(RSP.Secondary) ] [ R.str "Show Alert" ]
    ]