[<RequireQualifiedAccess>]
module Routing

open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
module RP = Fable.Helpers.React.Props

type Route =
    | Counter
    | Alerts

let route : Parser<Route -> Route, Route> =
    oneOf
        [ map Counter (s "counter")
          map Alerts (s "alerts") ]

let toHash route =
    match route with
    | Counter -> "#counter"
    | Alerts -> "#alerts"

let href route =
    RP.Href (toHash route)

/// Alias for Elmish.Browser.Navigation.modifyUrl
let modifyUrl route =
    route |> toHash |> Navigation.modifyUrl

/// Alias for Elmish.Browser.Navigation.newUrl
let newUrl route =
    route |> toHash |> Navigation.newUrl

/// Alias for Browser.window.location.href
let modifyLocation route =
    Fable.Import.Browser.window.location.href <- toHash route